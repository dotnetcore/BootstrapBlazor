// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 图标映射配置类
/// </summary>
public class IconMapperOptions
{
    /// <summary>
    /// 获得/设置 集合
    /// </summary>
    public Dictionary<ComponentIcons, string> Items { get; set; } = new();
}
