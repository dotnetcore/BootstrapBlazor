// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
