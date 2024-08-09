// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DockViewIcon 组件
/// </summary>
public partial class AntDesignIcon
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
    /// 获得 图标分类 默认为 Outlined    
    /// </summary>
    [Parameter]
    public AntDesignIconCategory Category { get; set; } = AntDesignIconCategory.Outlined;

    /// <summary>
    /// 获得 样式字符串
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-ant-icon")
        .AddClass($"bb-ant-icon-{Name}", !string.IsNullOrEmpty(Name))
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Href ??= $"./_content/BootstrapBlazor.AntDesignIcon/{Category.ToDescriptionString()}.svg#{Name}";
    }
}
