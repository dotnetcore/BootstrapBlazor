// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

/// <summary>
/// LinkButton 组件
/// </summary>
public class LinkButton : ButtonBase
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
        builder.AddAttribute(10, "class", ClassString);
        builder.AddAttribute(20, "href", UrlString);
        builder.AddAttribute(30, "target", Target);
        builder.AddAttribute(40, "disabled", Disabled);
        builder.AddAttribute(50, "aria-disabled", DisabledString);
        builder.AddAttribute(60, "tabindex", Tab);
        builder.AddAttribute(70, "id", Id);
        builder.AddAttribute(80, "role", "button");
        builder.AddMultipleAttributes(90, AdditionalAttributes);

        if (TriggerClick)
        {
            builder.AddAttribute(100, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, OnClickButton));
            builder.AddEventPreventDefaultAttribute(10, "onclick", Prevent);
            builder.AddEventStopPropagationAttribute(11, "onclick", StopPropagation);
        }

        if (!string.IsNullOrEmpty(Icon))
        {
            builder.AddContent(110, new MarkupString($"<i class=\"{Icon}\"></i>"));
        }

        if (!string.IsNullOrEmpty(ImageUrl))
        {
            builder.AddContent(120, AddImage());
        }

        if (!string.IsNullOrEmpty(Text))
        {
            builder.AddContent(130, new MarkupString($"<span>{Text}</span>"));
        }

        builder.AddContent(140, ChildContent);
        builder.CloseElement();
    }

    private RenderFragment AddImage() => builder =>
    {
        builder.OpenElement(0, "img");
        builder.AddAttribute(10, "alt", "img");
        if (!string.IsNullOrEmpty(ImageCss))
        {
            builder.AddAttribute(20, "class", ImageCss);
        }
        builder.AddAttribute(30, "src", ImageUrl);
        builder.CloseElement();
    };

    private async Task OnClickButton()
    {
        if (IsAsync)
        {
            IsAsyncLoading = true;
            IsDisabled = true;
        }

        await HandlerClick();

        // 恢复按钮
        if (IsAsync)
        {
            IsDisabled = IsKeepDisabled;
            IsAsyncLoading = false;
        }
    }
}
