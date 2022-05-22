// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// Topology 组件类
/// </summary>
public partial class Topology : IDisposable
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
    /// 获得/设置 获取推送数据回调委托方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<CancellationToken, Task<IEnumerable<TopologyItem>>>? OnQueryAsync { get; set; }

    /// <summary>
    /// 获得/设置 开始推送数据前回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnBeforePushData { get; set; }

    [NotNull]
    private JSModule? Module { get; set; }

    private string? StyleString => CssBuilder.Default("width: 100%; height: 100%;")
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private CancellationTokenSource? CancelToken { get; set; }

    private bool isInited { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!isInited)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                isInited = true;
                Module = await JSRuntime.LoadModule<Topology>("./_content/BootstrapBlazor.Topology/js/bootstrap.blazor.topology.min.js", this, false);
                await Module.InvokeVoidAsync("init", Id, Content, nameof(PushData));
            }
        }
    }

    /// <summary>
    /// 开始推送数据方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task PushData()
    {
        if (!disposing)
        {
            if (OnBeforePushData != null)
            {
                await OnBeforePushData();
            }
            Interval = Math.Max(100, Interval);
            CancelToken = new CancellationTokenSource();
            while (CancelToken != null && !CancelToken.IsCancellationRequested)
            {
                try
                {
                    var data = await OnQueryAsync(CancelToken.Token);
                    await Module.InvokeVoidAsync("push_data", CancelToken.Token, Id, data);

                    await Task.Delay(Interval, CancelToken.Token);
                }
                catch (TaskCanceledException)
                {

                }
            }
        }
    }

    private bool disposing = false;

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
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

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        disposing = true;
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
