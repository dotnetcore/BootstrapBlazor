// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Image 组件</para>
/// <para lang="en">Image Component</para>
/// </summary>
public partial class ImageViewer
{
    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get Component Style</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-img")
        .AddClass("is-preview", ShowPreviewList)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ImageClassString => CssBuilder.Default()
        .AddClass($"obj-fit-{FitMode.ToDescriptionString()}")
        .AddClass("d-none", ShouldHandleError && !IsLoaded)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 图片 Url 默认 null 必填</para>
    /// <para lang="en">Gets or sets Image Url Default null Required</para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图片是否异步加载</para>
    /// <para lang="en">Gets or sets whether the image is loaded asynchronously</para>
    /// </summary>
    [Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 原生 alt 属性 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Native alt Attribute Default null</para>
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示占位符 适用于大图片加载 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show placeholder. Suitable for large image loading. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowPlaceHolder { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载失败时是否显示错误占位符 默认 false</para>
    /// <para lang="en">Gets or sets Whether to show error placeholder when loading fails. Default false</para>
    /// </summary>
    [Parameter]
    public bool HandleError { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 占位模板 未设置 <see cref="Url"/> 或者 正在加载时显示 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Placeholder Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? PlaceHolderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 错误模板 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Error Template. Default null</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ErrorTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 原生 object-fit 属性 默认 fill 未设置</para>
    /// <para lang="en">Gets or sets Native object-fit Attribute. Default fill</para>
    /// </summary>
    [Parameter]
    public ObjectFitMode FitMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 原生 z-index 属性 默认 2050</para>
    /// <para lang="en">Gets or sets Native z-index Attribute. Default 2050</para>
    /// </summary>
    [Parameter]
    public int ZIndex { get; set; } = 2050;

    /// <summary>
    /// <para lang="zh">获得/设置 预览大图链接集合 默认 null</para>
    /// <para lang="en">Gets or sets Preview Image List Default null</para>
    /// </summary>
    [Parameter]
    public List<string>? PreviewList { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 预览大图当前链接集合点开的索引 默认为 0</para>
    /// <para lang="en">Gets or sets Index of the currently opened link in the preview image list Default 0</para>
    /// </summary>
    [Parameter]
    public int PreviewIndex { get; set; } = 0;

    /// <summary>
    /// <para lang="zh">获得/设置 图片加载失败时回调方法</para>
    /// <para lang="en">Gets or sets Callback method when image loading fails</para>
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnErrorAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图片加载成功时回调方法</para>
    /// <para lang="en">Gets or sets Callback method when image loading succeeds</para>
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnLoadAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图片文件图标</para>
    /// <para lang="en">Gets or sets Image File Icon</para>
    /// </summary>
    [Parameter]
    public string? FileIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否交叉监听 默认 false</para>
    /// <para lang="en">Gets or sets Whether Intersection Observer. Default false</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">不可见时不加载图片，当图片即将可见时才开始加载图片</para>
    /// <para lang="en">Images are not loaded when not visible, and start loading when they are about to become visible.</para>
    /// </remarks>
    [Parameter]
    public bool IsIntersectionObserver { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 预览缩放速度 默认 null 未设置取 0.015 值</para>
    /// <para lang="en">Gets or sets Zoom Speed Default null 0.015 if not set</para>
    /// </summary>
    [Parameter]
    public double? ZoomSpeed { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool ShowImage => !string.IsNullOrEmpty(Url);

    private bool IsLoaded { get; set; }

    private bool IsError { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        IsError = false;
        FileIcon ??= IconTheme.GetIconByKey(ComponentIcons.ImageViewerFileIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id, PreviewList, PreviewIndex);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Url, PreviewList, PreviewIndex, Async = IsAsync, PreviewerId, Intersection = IsIntersectionObserver });

    private RenderFragment RenderChildContent() => builder =>
    {
        if (!IsError)
        {
            builder.OpenElement(0, "img");
            builder.AddAttribute(1, "class", ImageClassString);
            if (!IsAsync && !IsIntersectionObserver)
            {
                builder.AddAttribute(2, "src", Url);
            }
            if (!string.IsNullOrEmpty(Alt))
            {
                builder.AddAttribute(3, "alt", Alt);
            }
            if (ShowPlaceHolder || ShouldHandleError)
            {
                builder.AddAttribute(4, "onload", EventCallback.Factory.Create(this, async () =>
                {
                    IsLoaded = true;
                    if (OnLoadAsync != null)
                    {
                        await OnLoadAsync(Url);
                    }
                }));
            }
            if (ShouldHandleError)
            {
                builder.AddAttribute(5, "onerror", EventCallback.Factory.Create(this, async () =>
                {
                    IsError = true;
                    if (OnErrorAsync != null)
                    {
                        await OnErrorAsync(Url);
                    }
                }));
            }
            builder.CloseElement();

            if (ShouldRenderPlaceHolder)
            {
                builder.AddContent(6, PlaceHolderTemplate ?? RenderPlaceHolder());
            }
        }
        else
        {
            builder.AddContent(7, ErrorTemplate ?? RenderErrorTemplate());
        }
    };

    private bool ShouldRenderPlaceHolder => (ShowPlaceHolder || PlaceHolderTemplate != null) && !IsLoaded;

    private bool ShouldHandleError => HandleError || ErrorTemplate != null;

    private bool ShowPreviewList => PreviewList != null && PreviewList.Count > 0;

    private string? PreviewerId => $"prev_{Id}";
}
