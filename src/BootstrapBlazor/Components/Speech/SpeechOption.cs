// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SpeechOption 配置类
/// </summary>
public class SpeechOption : ProviderOption
{
    /// <summary>
    /// 获得/设置 语音识别 Provider 实例
    /// </summary>
    public ISpeechProvider? Provider { get; set; }
}
