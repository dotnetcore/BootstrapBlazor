// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Progress 组件</para>
/// <para lang="en">Progress Component</para>
/// </summary>
public partial class Progress
{
    /// <summary>
    /// <para lang="zh">获得/设置 控件高度，默认为 null 未设置</para>
    /// <para lang="en">Gets or sets the control height. Default is null</para>
    /// </summary>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 颜色，默认为 Color.Primary</para>
    /// <para lang="en">Gets or sets the color. Default is Color.Primary</para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示进度条值，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show progress value. Default is false</para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsShowValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示为条纹，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show striped. Default is false</para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsStriped { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否动画，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show animated. Default is false</para>
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsAnimated { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件进度值</para>
    /// <para lang="en">Gets or sets the progress value</para>
    /// </summary>
    [Parameter]
    public double Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 进度值修约小数位数，默认为 0（即保留为整数）</para>
    /// <para lang="en">Gets or sets the rounding decimal places. Default is 0 (Keep as integer)</para>
    /// </summary>
    [Parameter]
    public int Round { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保留小数点模式，默认为 AwayFromZero</para>
    /// <para lang="en">Gets or sets the rounding mode. Default is AwayFromZero</para>
    /// </summary>
    [Parameter]
    public MidpointRounding MidpointRounding { get; set; } = MidpointRounding.AwayFromZero;

    /// <summary>
    /// <para lang="zh">获得/设置 进度标签文本</para>
    /// <para lang="en">Gets or sets the progress label text</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    private string? ClassString => CssBuilder.Default("progress")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ClassName => CssBuilder.Default("progress-bar")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("progress-bar-striped", IsStriped)
        .AddClass("progress-bar-animated", IsAnimated)
        .Build();

    private string? StyleName => CssBuilder.Default()
        .AddClass($"width: {InternalValue.ToString(CultureInfo.InvariantCulture)}%;")
        .Build();

    private string? ProgressStyle => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height.HasValue)
        .Build();

    private double InternalValue => Round == 0 ? Value : Math.Round(Value, Round, MidpointRounding);

    private string? ValueLabelString => IsShowValue ? string.IsNullOrEmpty(Text) ? $"{InternalValue}%" : Text : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Value = Math.Min(100, Math.Max(0, Value));
    }
}
