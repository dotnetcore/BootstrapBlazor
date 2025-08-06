// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using System;

namespace BootstrapBlazor.Components;

/// <summary>
/// LinkButton 组件
/// </summary>
public sealed class LinkButton : ButtonBase
{
    /// <summary>
    /// 获得/设置 Url 默认为 #
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 A 标签 target 参数 默认 null
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 显示图片地址 默认为 null
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The css class of img element default value null
    /// </summary>
    [Parameter]
    public string? ImageCss { get; set; }

    /// <summary>
    /// 获得/设置 是否为垂直布局 默认 false
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    private bool Prevent => (Url?.StartsWith('#') ?? true) || IsDisabled;

    private string TagName => IsDisabled ? "button" : "a";

    private string? UrlString => IsDisabled ? null : Url;

    private string? ClassString => CssBuilder.Default("btn link-button")
        .AddClass("btn-vertical", IsVertical)
        .AddClass($"btn-outline-{Color.ToDescriptionString()}", IsOutline)
        .AddClass($"link-{Color.ToDescriptionString()}", Color != Color.None && !IsOutline && !IsDisabled)
        .AddClass($"btn-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClass("btn-block", IsBlock)
        .AddClass("btn-round", ButtonStyle == ButtonStyle.Round)
        .AddClass("btn-circle", ButtonStyle == ButtonStyle.Circle)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private bool TriggerClick => !IsDisabled || (string.IsNullOrEmpty(Url));

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, TagName);
        builder.AddAttribute(1, "class", ClassString);
        builder.AddAttribute(2, "href", UrlString);
        builder.AddAttribute(3, "target", Target);
        builder.AddAttribute(4, "disabled", Disabled);
        builder.AddAttribute(5, "aria-disabled", DisabledString);
        builder.AddAttribute(6, "tabindex", Tab);
        builder.AddAttribute(7, "id", Id);
        builder.AddAttribute(8, "role", "button");

        if (TriggerClick)
        {
            builder.AddAttribute(9, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickButton));
            builder.AddEventPreventDefaultAttribute(10, "onclick", Prevent);
            builder.AddEventStopPropagationAttribute(11, "onclick", StopPropagation);
        }

        if (!string.IsNullOrEmpty(Icon))
        {
            builder.AddContent(12, new MarkupString($"<i class=\"{Icon}\"></i>"));
        }

        if (!string.IsNullOrEmpty(ImageUrl))
        {
            builder.AddContent(13, new MarkupString($"<img alt=\"img\" class=\"{ImageCss}\" src=\"{ImageUrl}\" />"));
        }

        if (!string.IsNullOrEmpty(Text))
        {
            builder.AddContent(14, new MarkupString($"<span>{Text}</span>"));
        }

        builder.AddContent(15, ChildContent);
        builder.CloseElement();
    }

    private async Task OnClickButton()
    {
        if (OnClickWithoutRender != null)
        {
            await OnClickWithoutRender();
        }
        if (OnClick.HasDelegate)
        {
            await OnClick.InvokeAsync();
        }
    }
}
