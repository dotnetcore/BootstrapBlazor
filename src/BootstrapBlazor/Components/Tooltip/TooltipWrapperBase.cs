// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TooltipWrapperBase 基类
/// </summary>
public abstract class TooltipWrapperBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// Tooltip 弹窗位置字符串
    /// </summary>
    protected virtual string? PlacementString => (!string.IsNullOrEmpty(TooltipText) && TooltipPlacement != Placement.Auto) ? TooltipPlacement.ToDescriptionString() : null;

    /// <summary>
    /// Tooltip Trigger 字符串
    /// </summary>
    protected virtual string? TriggerString => TooltipTrigger == "hover focus" ? null : TooltipTrigger;

    /// <summary>
    /// the instance of Tooltip component
    /// </summary>
    [CascadingParameter]
    protected Tooltip? Tooltip { get; set; }

    /// <summary>
    /// 获得/设置 TooltipText 显示文字 默认为 null
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    /// 获得/设置 Tooltip 显示位置 默认为 Top
    /// </summary>
    [Parameter]
    public Placement TooltipPlacement { get; set; } = Placement.Top;

    /// <summary>
    /// 获得/设置 Tooltip 触发方式 默认为 hover focus
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TooltipTrigger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipTrigger ??= "hover focus";
    }
}
