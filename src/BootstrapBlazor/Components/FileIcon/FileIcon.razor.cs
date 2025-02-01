// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Icon 图标组件
/// </summary>
public partial class FileIcon
{
    private string? ClassString => CssBuilder.Default("file-icon")
        .AddClass($"file-icon-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? IconClassString => CssBuilder.Default("badge")
        .AddClass($"bg-{IconColor.ToDescriptionString()}", IconColor != Color.None)
        .Build();

    /// <summary>
    /// 获得/设置 文件类型扩展名 
    /// </summary>
    [Parameter]
    [EditorRequired]
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

    /// <summary>
    /// 获得/设置 图标大小 默认 Color.None
    /// </summary>
    [Parameter]
    public Size Size { get; set; }
}
