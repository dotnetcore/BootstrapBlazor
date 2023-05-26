// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// 获得 Trigger 集合
    /// </summary>
    internal List<ContextMenuTrigger> Triggers { get; } = new();

    /// <summary>
    /// 获得/设置 上下文菜单组件集合
    /// </summary>
    internal ContextMenu? ContextMenu { get; set; }

    /// <summary>
    /// Trigger 调用
    /// </summary>
    /// <param name="contextItem"></param>
    /// <returns></returns>
    internal async Task OnContextMenu(object? contextItem)
    {
        // 弹出关联菜单
        if (ContextMenu != null)
        {
            await ContextMenu.Show(contextItem);
        }
    }
}
