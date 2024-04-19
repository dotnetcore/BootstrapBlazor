// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DockView 组件配置类
/// </summary>
public class DockViewOptions
{
    /// <summary>
    /// 获得/设置 组件本地化版本信息
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// 获得/设置 是否开启本地存储 默认 null 未设置
    /// </summary>
    public bool? EnableLocalStorage { get; set; }

    /// <summary>
    /// 获得/设置 本地存储前缀 默认 bb-dock
    /// </summary>
    public string? LocalStoragePrefix { get; set; }
}
