// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">图片预览组件</para>
/// <para lang="en">Image Previewer Component</para>
/// </summary>
public partial class ImagePreviewer
{
    private string? MinusIconString => CssBuilder.Default("minus-icon")
        .AddClass(MinusIcon)
        .Build();

    private string? PlusIconString => CssBuilder.Default("plus-icon")
        .AddClass(PlusIcon)
        .Build();

    private string? RotateLeftIconString => CssBuilder.Default("rotate-left")
        .AddClass(RotateLeftIcon)
        .Build();

    private string? RotateRightIconString => CssBuilder.Default("rotate-right")
        .AddClass(RotateRightIcon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 原生 z-index 属性 默认 2050</para>
    /// <para lang="en">Get/Set z-index property Default 2050</para>
    /// </summary>
    [Parameter]
    public int ZIndex { get; set; } = 2050;

    /// <summary>
    /// <para lang="zh">获得/设置 预览大图链接集合 默认 null</para>
    /// <para lang="en">Get/Set Preview Image List Default null</para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<string>? PreviewList { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上一张图片 Icon 图标</para>
    /// <para lang="en">Get/Set Previous Image Icon</para>
    /// </summary>
    [Parameter]
    public string? PreviousIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下一张图片 Icon 图标</para>
    /// <para lang="en">Get/Set Next Image Icon</para>
    /// </summary>
    [Parameter]
    public string? NextIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 缩小 Icon 图标</para>
    /// <para lang="en">Get/Set Zoom Out Icon</para>
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 放大 Icon 图标</para>
    /// <para lang="en">Get/Set Zoom In Icon</para>
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 向左旋转 Icon 图标</para>
    /// <para lang="en">Get/Set Rotate Left Icon</para>
    /// </summary>
    [Parameter]
    public string? RotateLeftIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 向右旋转 Icon 图标</para>
    /// <para lang="en">Get/Set Rotate Right Icon</para>
    /// </summary>
    [Parameter]
    public string? RotateRightIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 预览缩放速度 默认 null 未设置取 0.015 值</para>
    /// <para lang="en">Get/Set Zoom Speed Default null 0.015 if not set</para>
    /// </summary>
    [Parameter]
    public double? ZoomSpeed { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? GetFirstImageUrl() => PreviewList.First();

    private bool ShowButtons => PreviewList.Count > 1;

    private string? ClassString => CssBuilder.Default("bb-previewer collapse active")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-viewer-zindex: {ZIndex};", ZIndex > 0)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">显示图片</para>
    /// <para lang="en">Show Image</para>
    /// </summary>
    /// <param name="index"></param>
    public Task Show(int index = 0) => InvokeVoidAsync("show", Id, index);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        PreviousIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImagePreviewPreviousIcon);
        NextIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImagePreviewNextIcon);
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImagePreviewMinusIcon);
        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImagePreviewPlusIcon);
        RotateLeftIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImagePreviewRotateLeftIcon);
        RotateRightIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImagePreviewRotateRightIcon);
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
            await InvokeVoidAsync("update", Id, PreviewList);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, PreviewList, new { ZoomSpeed });
}
