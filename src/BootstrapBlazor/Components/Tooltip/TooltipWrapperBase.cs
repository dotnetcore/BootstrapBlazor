// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">TooltipWrapperBase 基类</para>
///  <para lang="en">TooltipWrapperBase 基类</para>
/// </summary>
public abstract class TooltipWrapperBase : BootstrapModuleComponentBase
{
    /// <summary>
    ///  <para lang="zh">Tooltip 弹窗位置字符串</para>
    ///  <para lang="en">Tooltip 弹窗位置字符串</para>
    /// </summary>
    protected virtual string? PlacementString => (!string.IsNullOrEmpty(TooltipText) && TooltipPlacement != Placement.Auto) ? TooltipPlacement.ToDescriptionString() : null;

    /// <summary>
    ///  <para lang="zh">Tooltip Trigger 字符串</para>
    ///  <para lang="en">Tooltip Trigger 字符串</para>
    /// </summary>
    protected virtual string? TriggerString => TooltipTrigger == "hover focus" ? null : TooltipTrigger;

    /// <summary>
    ///  <para lang="zh">the 实例 of Tooltip component</para>
    ///  <para lang="en">the instance of Tooltip component</para>
    /// </summary>
    [CascadingParameter]
    protected Tooltip? Tooltip { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 TooltipText 显示文字 默认为 null</para>
    ///  <para lang="en">Gets or sets TooltipText display文字 Default is为 null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Tooltip 显示位置 默认为 Top</para>
    ///  <para lang="en">Gets or sets Tooltip display位置 Default is为 Top</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Placement TooltipPlacement { get; set; } = Placement.Top;

    /// <summary>
    ///  <para lang="zh">获得/设置 Tooltip 触发方式 默认为 hover focus</para>
    ///  <para lang="en">Gets or sets Tooltip 触发方式 Default is为 hover focus</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TooltipTrigger { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipTrigger ??= "hover focus";
    }
}
