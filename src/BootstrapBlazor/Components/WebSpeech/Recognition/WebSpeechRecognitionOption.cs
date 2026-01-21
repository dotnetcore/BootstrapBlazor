// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebSpeechRecognitionOption 配置类</para>
/// <para lang="en">WebSpeechRecognitionOption 配置类</para>
/// </summary>
public class WebSpeechRecognitionOption
{
    /// <summary>
    /// <para lang="zh">sets the maximum number of SpeechRecognitionAlternatives provided per SpeechRecognitionResult.</para>
    /// <para lang="en">sets the maximum number of SpeechRecognitionAlternatives provided per SpeechRecognitionResult.</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? MaxAlternatives { get; set; }

    /// <summary>
    /// <para lang="zh">是否 continuous results are returned for each recognition, or only a single result.</para>
    /// <para lang="en">whether continuous results are returned for each recognition, or only a single result.</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Continuous { get; set; }

    /// <summary>
    /// <para lang="zh">是否 interim results should be returned (true) or not (false). Interim results are results that are not yet final (e.g. the SpeechRecognitionResult.isFinal 属性 is false).</para>
    /// <para lang="en">whether interim results should be returned (true) or not (false). Interim results are results that are not yet final (e.g. the SpeechRecognitionResult.isFinal property is false).</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? InterimResults { get; set; }

    /// <summary>
    /// <para lang="zh">sets the language of the current SpeechRecognition. If not specified, this defaults to the HTML lang attribute value, or the user agent's language setting if that isn't set either.</para>
    /// <para lang="en">sets the language of the current SpeechRecognition. If not specified, this defaults to the HTML lang attribute value, or the user agent's language setting if that isn't set either.</para>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lang { get; set; }
}
