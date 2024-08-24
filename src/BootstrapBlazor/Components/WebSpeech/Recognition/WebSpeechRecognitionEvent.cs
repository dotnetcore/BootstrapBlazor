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
    /// the lowest index value result in the SpeechRecognitionResultList "array" that has actually changed.
    /// </summary>
    public int ResultIndex { get; set; }

    /// <summary>
    /// the speech recognition results for the current session.
    /// </summary>
    public string? Results { get; set; }
}
