// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Scroll 组件</para>
/// <para lang="en">Scroll Component</para>
/// </summary>
public partial class Scroll
{
    private string? ClassString => CssBuilder.Default("scroll")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height))
        .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width))
        .AddClass($"--bb-scroll-width: {ActualScrollWidth}px;")
        .AddClass($"--bb-scroll-hover-width: {ActualScrollHoverWidth}px;")
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    private int ActualScrollWidth => ScrollWidth ?? Options.CurrentValue.ScrollOptions.ScrollWidth;

    private int ActualScrollHoverWidth => ScrollHoverWidth ?? Options.CurrentValue.ScrollOptions.ScrollHoverWidth;

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件高度</para>
    /// <para lang="en">Gets or sets Height</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件宽度</para>
    /// <para lang="en">Gets or sets Width</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动条宽度 默认 null 未设置使用 <see cref="ScrollOptions"/> 配置类中的 <see cref="ScrollOptions.ScrollWidth"/></para>
    /// <para lang="en">Gets or sets Scroll Width. Default null. Use <see cref="ScrollOptions.ScrollWidth"/> in <see cref="ScrollOptions"/> config class if not set</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? ScrollWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动条 hover 状态下宽度 默认 null 未设置使用 <see cref="ScrollOptions"/> 配置类中的 <see cref="ScrollOptions.ScrollHoverWidth"/></para>
    /// <para lang="en">Gets or sets Scroll Hover Width. Default null. Use <see cref="ScrollOptions.ScrollHoverWidth"/> in <see cref="ScrollOptions"/> config class if not set</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? ScrollHoverWidth { get; set; }

    [Inject]
    [NotNull]
    private IOptionsMonitor<BootstrapBlazorOptions>? Options { get; set; }

    /// <summary>
    /// <para lang="zh">滚动到底部</para>
    /// <para lang="en">Scroll To Bottom</para>
    /// </summary>
    public Task ScrollToBottom() => InvokeVoidAsync("scrollToBottom", Id);
}
