// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechSynthesisVoice 类
/// </summary>
public class WebSpeechSynthesisVoice
{
    /// <summary>
    /// 获得/设置 是否为默认
    /// </summary>
    public bool Default { get; set; }

    /// <summary>
    /// 获得/设置 indicating the language of the voice.
    /// </summary>
    public string? Lang { get; set; }

    /// <summary>
    /// 获得/设置 human-readable name that represents the voice.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 获得/设置 indicating whether the voice is supplied by a local speech synthesizer service
    /// </summary>
    public bool LocalService { get; set; }

    /// <summary>
    /// 获得/设置 the type of URI and location of the speech synthesis service for this voice.
    /// </summary>
    public string? VoiceURI { get; set; }
}
