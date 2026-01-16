// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">语音合成配置类</para>
/// <para lang="en">Speech Synthesis Option Class</para>
/// </summary>
public class SynthesizerOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 语音识别指令名称 默认 null</para>
    /// <para lang="en">Get/Set Speech Recognition Command Name. Default null</para>
    /// </summary>
    public string? MethodName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 语音合成文本内容 默认 null</para>
    /// <para lang="en">Get/Set Speech Synthesis Text Content. Default null</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 回调方法 默认 null</para>
    /// <para lang="en">Get/Set Callback Method. Default null</para>
    /// </summary>
    public Func<SynthesizerStatus, Task>? Callback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 识别语音文化 默认 zh-CN</para>
    /// <para lang="en">Get/Set Speech Synthesis Culture. Default zh-CN</para>
    /// </summary>
    public string SpeechSynthesisLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// <para lang="zh">获得/设置 结果文化 默认 zh-CN</para>
    /// <para lang="en">Get/Set Result Culture. Default zh-CN</para>
    /// </summary>
    public string SpeechSynthesisVoiceName { get; set; } = "zh-CN-XiaoxiaoNeural";
}
