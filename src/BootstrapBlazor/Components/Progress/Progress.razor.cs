// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Progress 组件
/// </summary>
public partial class Progress
{
    /// <summary>
    /// 获得/设置 控件高度 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? Height { get; set; }

    /// <summary>
    /// 获得/设置 颜色 默认为 Color.Primary
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示进度条值 默认 false
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsShowValue { get; set; }

    /// <summary>
    /// 获得/设置 是否显示为条纹 默认 false
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsStriped { get; set; }

    /// <summary>
    /// 获得/设置 是否动画 默认 false
    /// </summary>
    /// <value></value>
    [Parameter]
    public bool IsAnimated { get; set; }

    /// <summary>
    /// 获得/设置 组件进度值
    /// </summary>
    [Parameter]
    public double Value { get; set; }

    /// <summary>
    /// 获得/设置 进度值修约小数位数, 默认 0 (即保留为整数)
    /// </summary>
    [Parameter]
    public int Round { get; set; }

    /// <summary>
    /// 获得/设置 保留小数点模式 默认为 AwayFromZero
    /// </summary>
    [Parameter]
    public MidpointRounding MidpointRounding { get; set; } = MidpointRounding.AwayFromZero;

    /// <summary>
    /// 获得/设置 进度标签文本
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    private string? ClassString => CssBuilder.Default("progress")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("progress-bar")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("progress-bar-striped", IsStriped)
        .AddClass("progress-bar-animated", IsAnimated)
        .Build();

    /// <summary>
    /// 获得 Style 集合
    /// </summary>
    private string? StyleName => CssBuilder.Default()
        .AddClass($"width: {InternalValue}%;")
        .Build();

    /// <summary>
    /// 获得 ProgressStyle 集合
    /// </summary>
    private string? ProgressStyle => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height.HasValue)
        .Build();

    private double InternalValue => Round == 0 ? Value : Math.Round(Value, Round, MidpointRounding);

    /// <summary>
    /// 获得 当前值百分比标签文字
    /// </summary>
    private string? ValueLabelString => IsShowValue ? string.IsNullOrEmpty(Text) ? $"{InternalValue}%" : Text : null;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Value = Math.Min(100, Math.Max(0, Value));
    }
}
