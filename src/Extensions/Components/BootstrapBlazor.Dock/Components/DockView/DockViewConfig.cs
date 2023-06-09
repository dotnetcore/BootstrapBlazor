// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

class DockViewConfig
{
    /// <summary>
    /// 获得/设置 DockView 名称 要求页面内唯一
    /// </summary>
    public string Name { get; set; } = "default";

    /// <summary>
    /// 获得/设置 是否启用本地布局保持 默认 true
    /// </summary>
    public bool EnableLocalStorage { get; set; } = true;

    /// <summary>
    /// 获得/设置 配置信息版本号 默认 null
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// 获得/设置 Golden-Layout 配置项集合 默认 空集合
    /// </summary>
    [JsonPropertyName("content")]
    public List<DockContent> Contents { get; set; } = new();
}
