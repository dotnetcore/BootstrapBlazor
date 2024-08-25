// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechRecognitionOption 配置类
/// </summary>
public class WebSpeechRecognitionOption
{
    /// <summary>
    /// sets the maximum number of SpeechRecognitionAlternatives provided per SpeechRecognitionResult.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? MaxAlternatives { get; set; }

    /// <summary>
    /// whether continuous results are returned for each recognition, or only a single result.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Continuous { get; set; }

    /// <summary>
    /// whether interim results should be returned (true) or not (false). Interim results are results that are not yet final (e.g. the SpeechRecognitionResult.isFinal property is false).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? InterimResults { get; set; }

    /// <summary>
    /// sets the language of the current SpeechRecognition. If not specified, this defaults to the HTML lang attribute value, or the user agent's language setting if that isn't set either.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lang { get; set; }
}
