// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// ContextMenu 组件
/// </summary>
public partial class ContextMenu
{
    [CascadingParameter]
    [NotNull]
    private ContextMenuZone? ContextMenuZone { get; set; }

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
        return Task.CompletedTask;
    }
}
