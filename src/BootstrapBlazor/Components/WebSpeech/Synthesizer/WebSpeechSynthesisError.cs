// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechSynthesisError 类
/// </summary>
public class WebSpeechSynthesisError
{
    /// <summary>
    /// Returns the index position of the character in the SpeechSynthesisUtterance.text that was being spoken when the event was triggered.
    /// </summary>
    public int CharIndex { get; set; }

    /// <summary>
    /// Returns the elapsed time in seconds after the SpeechSynthesisUtterance.text started being spoken that the event was triggered at.
    /// </summary>
    public float ElapsedTime { get; set; }

    /// <summary>
    /// Returns an error code indicating what has gone wrong with a speech synthesis attempt.
    /// </summary>
    public string? Error { get; set; }
}
