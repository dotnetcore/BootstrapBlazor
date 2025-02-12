// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Frame 组件封装 Html iframe 元素
/// </summary>
public partial class IFrame
{
    /// <summary>
    /// 获得/设置 Frame 加载网页路径
    /// </summary>
    [Parameter]
    public string? Src { get; set; }

    /// <summary>
    /// 获得/设置 需要传递的数据
    /// </summary>
    [Parameter]
    public object? Data { get; set; }

    /// <summary>
    /// 获得/设置 Frame 加载页面传递过来的数据
    /// </summary>
    [Parameter]
    public Func<object?, Task>? OnPostDataAsync { get; set; }

    /// <summary>
    /// 获得/设置 页面加载完毕后回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnReadyAsync { get; set; }

    private string? ClassString => CssBuilder.Default("bb-frame")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private object? _lastData;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (_lastData != Data)
        {
            _lastData = Data;
            await PushData(Data);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(TriggerPostData));

    /// <summary>
    /// 推送数据方法
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public Task PushData(object? data) => InvokeVoidAsync("execute", Id, data);

    /// <summary>
    /// 由 JavaScript 调用
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerPostData(object? data)
    {
        if (OnPostDataAsync != null)
        {
            await OnPostDataAsync(data);
        }
    }

    /// <summary>
    /// 由 JavaScript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerLoaded()
    {
        if (OnReadyAsync != null)
        {
            await OnReadyAsync();
        }
    }
}
