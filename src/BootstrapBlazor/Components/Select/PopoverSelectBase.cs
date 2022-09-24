// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TValue"></typeparam>
public class PopoverSelectBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得/设置 弹窗位置 默认为 Bottom
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Bottom;

    /// <summary>
    /// 获得/设置 是否使用 Popover 渲染下拉框 默认 false
    /// </summary>
    [Parameter]
    public bool IsPopover { get; set; }

    /// <summary>
    /// 
    /// </summary>
    protected string? ToggleString => IsPopover ? null : "dropdown";

    /// <summary>
    /// 
    /// </summary>
    protected string? DropdownMenuClassString => IsPopover ? "dropdown-menu" : "dropdown-menu shadow";

    /// <summary>
    /// 
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();
}
