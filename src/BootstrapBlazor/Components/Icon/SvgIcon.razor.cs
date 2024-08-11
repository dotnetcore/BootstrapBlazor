// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SvgIcon 组件
/// </summary>
public partial class SvgIcon
{
    /// <summary>
    /// 获得/设置 图标名称
    /// </summary>
    [Parameter, NotNull]
    [EditorRequired]
    public string? Name { get; set; }

    /// <summary>
    /// 获得 图标地址
    /// </summary>
    [Parameter, NotNull]
    public string? Href { get; set; }

    /// <summary>
    /// 获得 样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-svg-icon")
        .AddClass($"bb-svg-icon-{Name}", !string.IsNullOrEmpty(Name))
        .Build();

    private string? _hrefString;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Href ??= "./_content/BootstrapBlazor.IconPark/icon-park.svg";
        _hrefString = $"{Href}#{Name}";
    }
}
