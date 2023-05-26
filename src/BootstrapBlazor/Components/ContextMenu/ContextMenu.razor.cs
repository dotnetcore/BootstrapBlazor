// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
        .AddClass("show", IsShow)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool IsShow { get; set; }

    private object? ContextItem { get; set; }

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
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal Task Show(object? contextItem)
    {
        IsShow = true;
        ContextItem = contextItem;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
