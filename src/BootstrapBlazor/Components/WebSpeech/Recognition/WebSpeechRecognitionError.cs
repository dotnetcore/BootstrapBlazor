// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebSpeechRecognitionError 类
///</para>
/// <para lang="en">WebSpeechRecognitionError 类
///</para>
/// </summary>
public class WebSpeechRecognitionError
{
    /// <summary>
    /// <para lang="zh">A string naming the 类型 of error. no-speech aborted audio-capture network not-allowed service-not-allowed bad-grammar language-not-supported.
    ///</para>
    /// <para lang="en">A string naming the type of error. no-speech aborted audio-capture network not-allowed service-not-allowed bad-grammar language-not-supported.
    ///</para>
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// <para lang="zh">A string containing more details about the error that was raised. Note that the spec does not define the exact wording of these messages — this is up to the implementors to decide upon.
    ///</para>
    /// <para lang="en">A string containing more details about the error that was raised. Note that the spec does not define the exact wording of these messages — this is up to the implementors to decide upon.
    ///</para>
    /// </summary>
    public string? Message { get; set; }
}
