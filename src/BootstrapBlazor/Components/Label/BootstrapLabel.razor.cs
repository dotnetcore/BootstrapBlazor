// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapLabel 组件
/// </summary>
public partial class BootstrapLabel
{
    /// <summary>
    /// 获得/设置 组件值 显示文本 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Value { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Tooltip 多用于标签文字过长导致裁减时使用 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 标签宽度 默认 null 未设置使用全局设置 <code>--bb-row-label-width</code> 值
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
        .AddStyle($"--bb-row-label-width", $"{LabelWidth}px", LabelWidth.HasValue)
        .Build();

    /// <summary>
    /// <inheritdoc/>
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
