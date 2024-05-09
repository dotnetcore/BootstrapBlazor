// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Options;

/// <summary>
/// MeiliSearch 配置项
/// </summary>
public class MeiliSearchOptions
{
    /// <summary>
    /// 获得/设置 主机地址
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    /// 获得/设置 配置 Key 值
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 获得/设置 索引名称
    /// </summary>
    public string? Index { get; set; }
}
