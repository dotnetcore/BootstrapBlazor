// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// Recognition result returned by the Baidu Speech sample API.
/// </summary>
public sealed class SampleSpeechRecognitionResult
{
    /// <summary>
    /// Gets or sets the provider error code.
    /// </summary>
    public int ErrorCode { get; set; }

    /// <summary>
    /// Gets or sets the provider error message.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets the recognized text.
    /// </summary>
    public string? Text { get; set; }
}
