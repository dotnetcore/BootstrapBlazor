// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Icon 组件</para>
/// <para lang="en">Icon Component</para>
/// </summary>
public partial class BootstrapBlazorIcon
{
    /// <summary>
    /// <para lang="zh">获得/设置 图标名称</para>
    /// <para lang="en">Gets or sets Icon Name</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">如果是字库图标应该是样式名称如 fa-solid fa-home 如果是 svg sprites 应该为 Id</para>
    /// <para lang="en">If it is a font icon, it should be a style name such as fa-solid fa-home. If it is svg sprites, it should be the Id</para>
    /// </remarks>
    [Parameter]
    [NotNull]
    public string? Name { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为 svg sprites 默认 false</para>
    /// <para lang="en">Gets or sets Whether is svg sprites Default false</para>
    /// </summary>
    [Parameter]
    public bool IsSvgSprites { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Svg Sprites 路径</para>
    /// <para lang="en">Gets or sets Svg Sprites Path</para>
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? Href => $"{Url}#{Name}";

    private string? ClassString => CssBuilder.Default("bb-icon")
        .AddClass($"bb-icon-{Name}", !string.IsNullOrEmpty(Name) && IsSvgSprites)
        .Build();
}
