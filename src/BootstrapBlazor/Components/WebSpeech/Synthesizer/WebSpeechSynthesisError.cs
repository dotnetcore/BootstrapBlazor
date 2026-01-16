// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">WebSpeechSynthesisError 类</para>
///  <para lang="en">WebSpeechSynthesisError 类</para>
/// </summary>
public class WebSpeechSynthesisError
{
    /// <summary>
    ///  <para lang="zh">Returns the 索引 position of the character in the SpeechSynthesisUtterance.text that was being spoken when the event was triggered.</para>
    ///  <para lang="en">Returns the index position of the character in the SpeechSynthesisUtterance.text that was being spoken when the event was triggered.</para>
    /// </summary>
    public int CharIndex { get; set; }

    /// <summary>
    ///  <para lang="zh">Returns the elapsed time in seconds after the SpeechSynthesisUtterance.text started being spoken that the event was triggered at.</para>
    ///  <para lang="en">Returns the elapsed time in seconds after the SpeechSynthesisUtterance.text started being spoken that the event was triggered at.</para>
    /// </summary>
    public float ElapsedTime { get; set; }

    /// <summary>
    ///  <para lang="zh">Returns an error code indicating what has gone wrong with a speech synthesis attempt.</para>
    ///  <para lang="en">Returns an error code indicating what has gone wrong with a speech synthesis attempt.</para>
    /// </summary>
    public string? Error { get; set; }
}
