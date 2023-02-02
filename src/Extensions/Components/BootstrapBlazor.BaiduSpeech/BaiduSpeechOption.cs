// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BaiduSpeechOptions 配置类
/// </summary>
public class BaiduSpeechOption
{
    /// <summary>
    /// 获得/设置 Baidu AppId
    /// </summary>
    [NotNull]
    public string? AppId { get; set; }

    /// <summary>
    /// 获得/设置 Baidu ApiKey
    /// </summary>
    [NotNull]
    public string? ApiKey { get; set; }

    /// <summary>
    /// 获得/设置 Baidu 密钥
    /// </summary>
    [NotNull]
    public string? Secret { get; set; }

    /// <summary>
    /// 获得/设置 语速 值范围 0 - 9 默认值 5
    /// </summary>
    public int Speed { get; set; } = 5;
}
