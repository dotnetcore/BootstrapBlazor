// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Progress 组件
/// </summary>
public partial class Progress
{
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
        .AddClass($"width: {Value}%;")
        .Build();

    /// <summary>
    /// 获得 ProgressStyle 集合
    /// </summary>
    private string? ProgressStyle => CssBuilder.Default()
        .AddClass($"height: {Height}px;", Height.HasValue)
        .Build();

    /// <summary>
    /// 获得 当前值
    /// </summary>
    private string ValueString => Value.ToString();

    /// <summary>
    /// 获得 当前值百分比标签文字
    /// </summary>
    private string? ValueLabelString => IsShowValue ? $"{Value}%" : null;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Value = Math.Min(100, Math.Max(0, Value));
    }
}
