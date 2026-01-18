// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Watermark 组件</para>
/// <para lang="en">Watermark Component</para>
/// </summary>
public partial class Watermark
{
    /// <summary>
    /// <para lang="zh">获得/设置 组件内容</para>
    /// <para lang="en">Gets or sets componentcontent</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 水印文本 默认 BootstrapBlazor</para>
    /// <para lang="en">Gets or sets 水印文本 Default is BootstrapBlazor</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 字体大小 默认 null 未设置 默认使用 16px 字体大小 单位 px</para>
    /// <para lang="en">Gets or sets 字体大小 Default is null 未Sets Default is使用 16px 字体大小 单位 px</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? FontSize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 颜色 默认 null 未设置</para>
    /// <para lang="en">Gets or sets color Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 水印的旋转角度 默认 null 45°</para>
    /// <para lang="en">Gets or sets 水印的旋转角度 Default is null 45°</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? Rotate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 水印元素的 z-index 值 默认 null</para>
    /// <para lang="en">Gets or sets 水印元素的 z-index 值 Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? ZIndex { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 水印之间的间距 值 默认 null</para>
    /// <para lang="en">Gets or sets 水印之间的间距 值 Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int? Gap { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为整页面水印 默认 false</para>
    /// <para lang="en">Gets or sets whether为整页面水印 Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsPage { get; set; }

    private string? ClassString => CssBuilder.Default("bb-watermark")
        .AddClass("is-page", IsPage)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= "BootstrapBlazor";

        if (IsPage && ChildContent is not null)
        {
            throw new InvalidOperationException($"{nameof(IsPage)} is true, {nameof(ChildContent)} cannot be set.");
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id, GetOptions());
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, GetOptions());

    private object GetOptions() => new
    {
        Text,
        FontSize,
        Color,
        Rotate,
        Gap,
        ZIndex,
        IsPage
    };
}
