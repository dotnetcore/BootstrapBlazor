// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapLabel 组件</para>
/// <para lang="en">BootstrapLabel component</para>
/// </summary>
public partial class BootstrapLabel
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件值 显示文本 默认 null</para>
    /// <para lang="en">Gets or sets component值 display文本 Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Tooltip 多用于标签文字过长导致裁减时使用 默认 false 不显示</para>
    /// <para lang="en">Gets or sets whetherdisplay Tooltip 多用于标签文字过长导致裁减时使用 Default is false 不display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值</para>
    /// <para lang="en">Gets or sets 标签width Default is null 未Sets使用全局Sets <code>--bb-row-label-width</code> 值</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    [CascadingParameter]
    private BootstrapLabelSetting? Setting { get; set; }

    private bool _showTooltip;

    private string? ClassString => CssBuilder.Default("form-label")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-row-label-width: {LabelWidth}px;", LabelWidth.HasValue)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (ShowLabelTooltip.HasValue)
        {
            _showTooltip = ShowLabelTooltip.Value;
        }
        Value ??= "";

        // 获得级联参数的 LabelWidth
        LabelWidth ??= Setting?.LabelWidth;
    }
}
