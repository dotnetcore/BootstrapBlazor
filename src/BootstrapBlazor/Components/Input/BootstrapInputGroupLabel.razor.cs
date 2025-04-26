// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInputGroupLabel 组件
/// </summary>
public partial class BootstrapInputGroupLabel
{
    private string? ClassString => CssBuilder.Default()
        .AddClass("input-group-text", IsInputGroupLabel)
        .AddClass("form-label", !IsInputGroupLabel)
        .AddClass("justify-content-center", Alignment == Alignment.Center)
        .AddClass("justify-content-end", Alignment == Alignment.Right)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-input-group-label-width: {Width}px;", Width.HasValue)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

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

    /// <summary>
    /// Gets or sets the child content. Default is null.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? Required => ShowRequiredMark ? "true" : null;

    private bool IsInputGroupLabel => InputGroup != null;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsInputGroupLabel)
        {
            DisplayText ??= FieldIdentifier?.GetDisplayName();
        }
    }
}
