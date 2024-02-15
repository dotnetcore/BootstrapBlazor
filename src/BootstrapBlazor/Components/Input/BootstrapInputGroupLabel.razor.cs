// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInputGroupLabel 组件
/// </summary>
public partial class BootstrapInputGroupLabel
{
    private string? ClassString => CssBuilder.Default()
        .AddClass("input-group-text", IsInnerLabel)
        .AddClass("form-label", !IsInnerLabel)
        .AddClass("justify-content-center", Alignment == Alignment.Center)
        .AddClass("justify-content-end", Alignment == Alignment.Right)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
    .AddClass($"--bb-input-group-label-width: {Width}px;", Width.HasValue)
    .AddClassFromAttributes(AdditionalAttributes)
    .Build();

    private bool IsInnerLabel { get; set; }

    /// <summary>
    /// 获得/设置 标签宽度 默认 null 未设置自动适应
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// 获得/设置 标签对其方式 默认 null 未设置 star 对齐
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; }

    /// <summary>
    /// 获得/设置 是否显示必填项标识 默认 false
    /// </summary>
    [Parameter]
    public bool ShowRequiredMark { get; set; }

    private string? Required => ShowRequiredMark ? "true" : null;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        IsInnerLabel = InputGroup != null;
    }
}
