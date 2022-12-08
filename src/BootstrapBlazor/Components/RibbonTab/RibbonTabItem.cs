// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// RibbonTabItem 实体类
/// </summary>
public class RibbonTabItem : MenuItem
{
    /// <summary>
    /// 获得/设置 图片路径
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// 获得/设置 分组名称
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// 获得/设置 按钮标识
    /// </summary>
    public string? Command { get; set; }

    /// <summary>
    /// 获得/设置 动态组件实例
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }

    /// <summary>
    /// 获得/设置 是否为默认按钮 默认 false
    /// </summary>
    public bool IsDefault { get; set; }
}
