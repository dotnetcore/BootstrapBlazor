// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenu 组件
/// </summary>
public partial class ContextMenu
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    [NotNull]
    private ContextMenuZone? ContextMenuZone { get; set; }

    private string? ClassString => CssBuilder.Default("bb-cm")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private object? ContextItem { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 参数 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示阴影 默认 true
    /// </summary>
    [Parameter]
    public bool ShowShadow { get; set; } = true;

    private string ZoneId => ContextMenuZone.Id;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ContextMenuZone.ContextMenu = this;
    }

    /// <summary>
    /// 弹出 ContextMenu
    /// </summary>
    /// <param name="triggerId">相关联 Tigger Id</param>
    /// <param name="args"></param>
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal async Task Show(string triggerId, MouseEventArgs args, object? contextItem)
    {
        ContextItem = contextItem;
        await InvokeVoidAsync("show", Id, triggerId, args);
    }
}
