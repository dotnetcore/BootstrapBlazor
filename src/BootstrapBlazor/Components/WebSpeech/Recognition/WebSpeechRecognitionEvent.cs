// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechRecognitionEvent 类
/// </summary>
public class WebSpeechRecognitionEvent
{
    /// <summary>
    /// 获得/设置 识别文本内容
    /// </summary>
    public string? Transcript { get; set; }

    /// <summary>
    /// 获得/设置 是否已经结束
    /// </summary>
    public bool IsFinal { get; set; }
}
