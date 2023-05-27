// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenuZone 组件
/// </summary>
public partial class ContextMenuZone : BootstrapModuleComponentBase
{
    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 上下文菜单组件集合
    /// </summary>
    internal ContextMenu? ContextMenu { get; set; }

    private string? ClassString => CssBuilder.Default("bb-cm-zone")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// Trigger 调用
    /// </summary>
    /// <param name="args"></param>
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal async Task OnContextMenu(MouseEventArgs args, object? contextItem)
    {
        // 弹出关联菜单
        if (ContextMenu != null)
        {
            await ContextMenu.Show(args, contextItem);
        }
    }
}
