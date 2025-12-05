// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInputGroupLabel 组件
/// </summary>
public sealed class BootstrapInputGroupLabel : DisplayBase<string>
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

    private string TagName => IsInputGroupLabel ? "div" : "label";

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

    /// <summary>
    /// 获得/设置 是否为 InputGroup 或 TableToolbar 内的标签 默认 null 未设置
    /// </summary>
    [Parameter]
    public bool? IsGroupLabel { get; set; }

    private string? Required => ShowRequiredMark ? "true" : null;

    private bool IsInputGroupLabel => IsGroupLabel ?? InputGroup != null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (IsInputGroupLabel)
        {
            DisplayText ??= FieldIdentifier?.GetDisplayName();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, TagName);
        builder.AddMultipleAttributes(10, AdditionalAttributes);
        builder.AddAttribute(20, "class", ClassString);
        builder.AddAttribute(30, "style", StyleString);
        builder.AddAttribute(40, "required", Required);
        if (ChildContent != null)
        {
            builder.AddContent(50, ChildContent);
        }
        else
        {
            builder.AddContent(60, DisplayText);
        }
        builder.CloseElement();
    }
}
