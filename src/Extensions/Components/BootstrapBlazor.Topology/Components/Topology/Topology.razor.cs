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
    [Obsolete("已过期，脚本已支持")]
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

    private string? StyleString => CssBuilder.Default()
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private CancellationTokenSource? CancelToken { get; set; }

    private string? ClassString => CssBuilder.Default("bb-topology")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoker = Interop, Data = Content, Callback = nameof(PushData), IsFitView, IsCenterView });

    /// <summary>
    /// 开始推送数据方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task PushData()
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
                    if (CancelToken != null)
                    {
                        await Task.Delay(Interval, CancelToken.Token);
                    }
                }
                catch (TaskCanceledException)
                {

                }
            }
        }
    }

    /// <summary>
    /// 推送数据到客户端
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public Task PushData(IEnumerable<TopologyItem> items) => InvokeVoidAsync("update", Id, items);

    /// <summary>
    /// 重置视图 缩放比例 默认 1 即 100%
    /// </summary>
    /// <param name="rate"></param>
    /// <param name="isCenterView"></param>
    /// <param name="isFitView"></param>
    /// <returns></returns>
    public Task Scale(int rate = 1, bool isCenterView = false, bool isFitView = false) => InvokeVoidAsync("scale", Id, rate, new { isCenterView, isFitView });

    /// <summary>
    /// 重置视图 自适应大小并且居中显示
    /// </summary>
    /// <param name="isCenterView"></param>
    /// <param name="isFitView"></param>
    /// <returns></returns>
    public Task Reset(bool isCenterView = true, bool isFitView = true) => InvokeVoidAsync("reset", Id, new { isCenterView, isFitView });

    /// <summary>
    /// 重置可视化引擎大小
    /// </summary>
    /// <returns></returns>
    public Task Resize(int? width = null, int? height = null) => InvokeVoidAsync("resize", Id, width, height);

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            if (CancelToken != null)
            {
                CancelToken.Cancel();
                CancelToken.Dispose();
                CancelToken = null;
            }
        }
    }
}
