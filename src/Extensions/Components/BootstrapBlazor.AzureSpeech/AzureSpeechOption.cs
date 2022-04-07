// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// AzureSpeechOption 配置类
/// </summary>
public class AzureSpeechOption
{
    /// <summary>
    /// 获得/设置 订阅 Key 由 Azure 提供
    /// </summary>
    [NotNull]
    public string? SubscriptionKey { get; set; }

    /// <summary>
    /// 获得/设置 Location/Region 描述 由 Azure 提供
    /// </summary>
    [NotNull]
    public string? Region { get; set; }

    /// <summary>
    /// 获得/设置 AuthorizationToken 网址 由 Azure 提供
    /// </summary>
    [NotNull]
    public string? AuthorizationTokenUrl { get; set; }

    /// <summary>
    /// 获得/设置 超时时长 默认 0 未设置
    /// </summary>
    public int Timeout { get; set; }
}
