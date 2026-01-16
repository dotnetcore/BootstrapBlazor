// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebSpeechSynthesisVoice 类
///</para>
/// <para lang="en">WebSpeechSynthesisVoice 类
///</para>
/// </summary>
public class WebSpeechSynthesisVoice
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否为默认
    ///</para>
    /// <para lang="en">Gets or sets whether为Default is
    ///</para>
    /// </summary>
    public bool Default { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 indicating the language of the voice.
    ///</para>
    /// <para lang="en">Gets or sets indicating the language of the voice.
    ///</para>
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 human-readable name that represents the voice.
    ///</para>
    /// <para lang="en">Gets or sets human-readable name that represents the voice.
    ///</para>
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 indicating whether the voice is supplied by a local speech synthesizer service
    ///</para>
    /// <para lang="en">Gets or sets indicating whether the voice is supplied by a local speech synthesizer service
    ///</para>
    /// </summary>
    public bool LocalService { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the type of URI and location of the speech synthesis service for this voice.
    ///</para>
    /// <para lang="en">Gets or sets the type of URI and location of the speech synthesis service for this voice.
    ///</para>
    /// </summary>
    public string? VoiceURI { get; set; }
}
