// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Icon 组件
/// </summary>
public partial class BootstrapBlazorIcon
{
    /// <summary>
    /// 获得/设置 图标名称
    /// </summary>
    /// <remarks>如果是字库图标应该是样式名称如 fa-solid fa-home 如果是 svg sprites 应该为 Id</remarks>
    [Parameter]
    [NotNull]
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 是否为 svg sprites 默认 false
    /// </summary>
    [Parameter]
    public bool IsSvgSprites { get; set; }

    /// <summary>
    /// 获得/设置 Svg Sprites 路径
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? Href => $"{Url}#{Name}";

    private string? ClassString => CssBuilder.Default("bb-icon")
        .AddClass($"bb-icon-{Name}", !string.IsNullOrEmpty(Name) && IsSvgSprites)
        .Build();
}
