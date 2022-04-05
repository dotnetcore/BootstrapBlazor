// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 图片预览组件
/// </summary>
public partial class ImagePreview
{
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

    private string? GetFirstImageUrl() => PreviewList.First();

    private bool ShowButtons => PreviewList.Count > 1;
}
