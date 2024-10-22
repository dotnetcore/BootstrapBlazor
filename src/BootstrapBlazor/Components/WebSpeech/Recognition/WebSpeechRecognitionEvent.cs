// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
