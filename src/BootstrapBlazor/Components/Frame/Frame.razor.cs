// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Frame 组件封装 Html iframe 元素
/// </summary>
public partial class Frame
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

            await InvokeVoidAsync("execute", Id, Data);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(CallbackAsync));

    /// <summary>
    /// 由 JavaScript 调用
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task CallbackAsync(object? data)
    {
        if (OnPostDataAsync != null)
        {
            await OnPostDataAsync(data);
        }
    }
}
