// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Topology 组件类
/// </summary>
public partial class Topology : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 JSON 文件内容
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 推送数据间隔时间 最小值 100 默认 2000 毫秒
    /// </summary>
    [Parameter]
    public int Interval { get; set; } = 2000;

    /// <summary>
    /// 获得/设置 获取推送数据回调委托方法 默认 null 赋值后开启轮训模式
    /// </summary>
    [Parameter]
    public Func<CancellationToken, Task<IEnumerable<TopologyItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// 获得/设置 开始推送数据前回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnBeforePushData { get; set; }

    /// <summary>
    /// 获得/设置 是否支持树莓派触屏浏览器
    /// </summary>
    /// <remarks></remarks>
    [Parameter]
    public bool IsSupportTouch { get; set; }

    /// <summary>
    /// 获得/设置 是否自适应屏幕显示
    /// </summary>
    /// <remarks></remarks>
    [Parameter]
    public bool IsFitView { get; set; }

    /// <summary>
    /// 获得/设置 是否居中显示可视区域
    /// </summary>
    /// <remarks></remarks>
    [Parameter]
    public bool IsCenterView { get; set; }

    private string? StyleString => CssBuilder.Default("width: 100%; height: 100%;")
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private CancellationTokenSource? CancelToken { get; set; }

    private string? IsSupportTouchString => IsSupportTouch ? "true" : null;

    private string? IsFitViewString => IsFitView ? "true" : null;

    private string? IsCenterViewString => IsCenterView ? "true" : null;

    [NotNull]
    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private DotNetObjectReference<Topology>? Interop { get; set; }

    /// <summary>
    /// 获得/设置 EChart DOM 元素实例
    /// </summary>
    private ElementReference Element { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            // import JavaScript
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Topology/Components/Topology/Topology.razor.js");
            Interop = DotNetObjectReference.Create(this);
            await Module.InvokeVoidAsync("init", Element, Interop, Content, nameof(PushData));
        }
    }

    /// <summary>
    /// 开始推送数据方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task PushData()
    {
        if (!_disposing)
        {
            if (OnBeforePushData != null)
            {
                await OnBeforePushData();
            }

            // 判断工作模式
            if (OnQueryAsync != null)
            {
                // 轮训模式
                Interval = Math.Max(100, Interval);
                CancelToken = new CancellationTokenSource();
                while (CancelToken != null && !CancelToken.IsCancellationRequested)
                {
                    try
                    {
                        var data = await OnQueryAsync(CancelToken.Token);
                        await PushData(data);
                        await Task.Delay(Interval, CancelToken.Token);
                    }
                    catch (TaskCanceledException)
                    {

                    }
                }
            }
        }
    }

    /// <summary>
    /// 推送数据到客户端
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public async ValueTask PushData(IEnumerable<TopologyItem> items)
    {
        if (!_disposing)
        {
            await Module.InvokeVoidAsync("update", Element, items);
        }
    }

    /// <summary>
    /// 重置视图 缩放比例 默认 1 即 100%
    /// </summary>
    /// <param name="rate"></param>
    /// <returns></returns>
    public ValueTask Scale(int rate = 1) => Module.InvokeVoidAsync("scale", Element, rate);

    /// <summary>
    /// 重置视图 自适应大小并且居中显示
    /// </summary>
    /// <returns></returns>
    public ValueTask Reset() => Module.InvokeVoidAsync("reset", Element);

    /// <summary>
    /// 重置可视化引擎大小
    /// </summary>
    /// <returns></returns>
    public ValueTask Resize(int? width = null, int? height = null) => Module.InvokeVoidAsync("resize", Element, width, height);

    #region Dispose
    private bool _disposing;

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        _disposing = true;
        if (disposing)
        {
            if (CancelToken != null)
            {
                CancelToken.Cancel();
                CancelToken.Dispose();
                CancelToken = null;
            }

            Interop?.Dispose();

            if (Module != null)
            {
                await Module.InvokeVoidAsync("dispose", Element);
                await Module.DisposeAsync();
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
