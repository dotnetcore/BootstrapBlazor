// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// MessageItem 组件
/// </summary>
public sealed partial class MessageItem
{
    private ElementReference MessageItemElement { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    protected override string? ClassName => CssBuilder.Default("alert")
        .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("is-bar", ShowBar)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 Toast Body 子组件
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 是否自动隐藏
    /// </summary>
    [Parameter]
    public bool IsAutoHide { get; set; } = true;

    /// <summary>
    /// 获得/设置 自动隐藏时间间隔
    /// </summary>
    [Parameter]
    public int Delay { get; set; } = 4000;

    /// <summary>
    /// 获得/设置 Message 实例
    /// </summary>
    /// <value></value>
    [CascadingParameter]
    public Message? Message { get; set; }

    private JSInterop<Message>? _interop;

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender && Message != null)
        {
            _interop = new JSInterop<Message>(JSRuntime);
            await _interop.InvokeVoidAsync(Message, MessageItemElement, "bb_message", nameof(Message.Clear));
        }
    }

    private async Task OnClick()
    {
        if (OnDismiss != null) await OnDismiss();
    }
}
