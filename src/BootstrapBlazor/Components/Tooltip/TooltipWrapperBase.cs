// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TooltipWrapperBase 基类</para>
/// <para lang="en">TooltipWrapperBase Base Class</para>
/// </summary>
public abstract class TooltipWrapperBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// <para lang="zh">Tooltip 弹窗位置字符串</para>
    /// <para lang="en">Tooltip Popup Position String</para>
    /// </summary>
    protected virtual string? PlacementString => (!string.IsNullOrEmpty(TooltipText) && TooltipPlacement != Placement.Auto) ? TooltipPlacement.ToDescriptionString() : null;

    /// <summary>
    /// <para lang="zh">Tooltip 触发方式字符串</para>
    /// <para lang="en">Tooltip Trigger String</para>
    /// </summary>
    protected virtual string? TriggerString => TooltipTrigger == "hover focus" ? null : TooltipTrigger;

    /// <summary>
    /// <para lang="zh">获得 Tooltip 组件实例</para>
    /// <para lang="en">Gets the Tooltip component instance</para>
    /// </summary>
    [CascadingParameter]
    protected Tooltip? Tooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Tooltip 显示文字，默认为 null</para>
    /// <para lang="en">Gets or sets the Tooltip display text. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Tooltip 显示位置，默认为 Top</para>
    /// <para lang="en">Gets or sets the Tooltip display position. Default is Top.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Placement TooltipPlacement { get; set; } = Placement.Top;

    /// <summary>
    /// <para lang="zh">获得/设置 Tooltip 触发方式，默认为 hover focus</para>
    /// <para lang="en">Gets or sets the Tooltip trigger method. Default is hover focus.</para>
    /// <para><version>10.2.2</version></para>
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
