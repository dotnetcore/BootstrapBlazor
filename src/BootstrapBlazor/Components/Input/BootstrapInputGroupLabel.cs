// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapInputGroupLabel 组件</para>
/// <para lang="en">BootstrapInputGroupLabel Component</para>
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
    /// <para lang="zh">获得/设置 标签宽度 默认 null 未设置自动适应</para>
    /// <para lang="en">Get/Set Label Width. Default null (Auto Fit)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签对其方式 默认 null 未设置 star 对齐</para>
    /// <para lang="en">Get/Set Label Alignment. Default null (Start Alignment)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示必填项标识 默认 false</para>
    /// <para lang="en">Get/Set Whether to show required mark. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowRequiredMark { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the child 内容. 默认为 null.
    ///</para>
    /// <para lang="en">Gets or sets the child content. Default is null.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 InputGroup 或 TableToolbar 内的标签 默认 null 未设置</para>
    /// <para lang="en">Get/Set Whether it is a label inside InputGroup or TableToolbar. Default null (Not set)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? IsGroupLabel { get; set; }

    [CascadingParameter]
    private IToolbarComponent? ToolbarComponent { get; set; }

    private string? Required => ShowRequiredMark ? "true" : null;

    private bool IsInputGroupLabel => IsGroupLabel ?? InputGroup != null || ToolbarComponent != null;

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
