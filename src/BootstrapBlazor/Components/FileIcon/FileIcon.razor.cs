// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Icon 图标组件
/// </summary>
public partial class FileIcon
{
    private string? ClassString => CssBuilder.Default("file-icon")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? IconClassString => CssBuilder.Default("badge")
        .AddClass($"bg-{IconColor.ToDescriptionString()}", IconColor != Color.None)
        .Build();

    /// <summary>
    /// 获得/设置 文件类型扩展名 
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? Extension { get; set; }

    /// <summary>
    /// 获得/设置 背景图模板 默认 null 使用内部内置的空文件 svg 图
    /// </summary>
    [Parameter]
    public RenderFragment? BackgroundTemplate { get; set; }

    /// <summary>
    /// 获得/设置 图标类型背景色 默认 Color.Primary
    /// </summary>
    [Parameter]
    public Color IconColor { get; set; } = Color.Primary;
}
