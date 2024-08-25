// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechRecognitionError 类
/// </summary>
public class WebSpeechRecognitionError
{
    /// <summary>
    /// A string naming the type of error. no-speech aborted audio-capture network not-allowed service-not-allowed bad-grammar language-not-supported.
    /// </summary>
    public string? Error { get; set; }

    /// <summary>
    /// A string containing more details about the error that was raised. Note that the spec does not define the exact wording of these messages — this is up to the implementors to decide upon.
    /// </summary>
    public string? Message { get; set; }
}
