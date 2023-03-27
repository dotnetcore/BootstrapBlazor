// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 图片预览组件
/// </summary>
[JSModuleAutoLoader("image-previewer")]
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
    /// 获得/设置 原生 z-index 属性 默认 2050
    /// </summary>
    [Parameter]
    public int ZIndex { get; set; } = 2050;

    /// <summary>
    /// 获得/设置 预览大图链接集合 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public List<string>? PreviewList { get; set; }

    /// <summary>
    /// 获得/设置 上一张图片 Icon 图标
    /// </summary>
    [Parameter]
    public string? PreviousIcon { get; set; }

    /// <summary>
    /// 获得/设置 下一张图片 Icon 图标
    /// </summary>
    [Parameter]
    public string? NextIcon { get; set; }

    /// <summary>
    /// 获得/设置 缩小 Icon 图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 获得/设置 方法 Icon 图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    /// <summary>
    /// 获得/设置 向左旋转 Icon 图标
    /// </summary>
    [Parameter]
    public string? RotateLeftIcon { get; set; }

    /// <summary>
    /// 获得/设置 向右旋转 Icon 图标
    /// </summary>
    [Parameter]
    public string? RotateRightIcon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? GetFirstImageUrl() => PreviewList.First();

    private bool ShowButtons => PreviewList.Count > 1;

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
    /// <returns></returns>
    protected override Task ModuleInitAsync() => InvokeInitAsync(Id, PreviewList);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task ModuleExecuteAsync() => InvokeExecuteAsync(Id, PreviewList);
}
