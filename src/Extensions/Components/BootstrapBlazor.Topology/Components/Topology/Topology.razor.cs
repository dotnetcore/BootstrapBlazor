// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Topology 组件类
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor.Topology/js/bootstrap.blazor.topology.min.js", ModuleName = "BlazorTopology", JSObjectReference = true, Relative = false)]
public partial class Topology
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task ModuleInitAsync() => InvokeInitAsync(Id, Content, nameof(PushData));

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
                        await PushData(data, CancelToken.Token);
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
    /// <param name="token"></param>
    /// <returns></returns>
    public async ValueTask PushData(IEnumerable<TopologyItem> items, CancellationToken token = default)
    {
        if (!_disposing)
        {
            await InvokeVoidAsync("execute", token, Id, items);
        }
    }

    /// <summary>
    /// 重置视图 缩放比例 默认 1 即 100%
    /// </summary>
    /// <param name="rate"></param>
    /// <returns></returns>
    public Task Scale(int rate = 1) => InvokeExecuteAsync(Id, "scale", rate);
    private bool _disposing;
    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        _disposing = true;
        if (CancelToken != null && disposing)
        {
            CancelToken.Cancel();
            CancelToken.Dispose();
            CancelToken = null;
        }
        await base.DisposeAsync(disposing);
    }
}
