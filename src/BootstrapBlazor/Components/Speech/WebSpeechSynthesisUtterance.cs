// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechSynthesisUtterance 类
/// </summary>
public class WebSpeechSynthesisUtterance
{
    /// <summary>
    /// gets and sets the text that will be synthesized when the utterance is spoken.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Text { get; set; }

    /// <summary>
    /// gets and sets A string representing a BCP 47 language tag
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Lang { get; set; }

    /// <summary>
    /// gets and sets the pitch at which the utterance will be spoken at.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Pitch { get; set; }

    /// <summary>
    /// gets and sets the speed at which the utterance will be spoken at.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Rate { get; set; }

    /// <summary>
    /// gets and sets the voice that will be used to speak the utterance.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public WebSpeechSynthesisVoice? Voice { get; set; }

    /// <summary>
    /// gets and sets the volume that the utterance will be spoken at.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? Volume { get; set; }
}
