// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// ElementIcon 组件
/// </summary>
public partial class ElementIcon
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
    private string? ClassString => CssBuilder.Default("bb-element-icon")
        .AddClass($"bb-element-icon-{Name}", !string.IsNullOrEmpty(Name))
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Href ??= $"./_content/BootstrapBlazor.ElementIcon/element.svg#{Name}";
    }
}
