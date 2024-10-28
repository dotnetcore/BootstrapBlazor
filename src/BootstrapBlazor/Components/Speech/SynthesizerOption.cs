// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音合成配置类
/// </summary>
public class SynthesizerOption
{
    /// <summary>
    /// 获得/设置 语音识别指令名称 默认 null
    /// </summary>
    public string? MethodName { get; set; }

    /// <summary>
    /// 获得/设置 语音合成文本内容 默认 null
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 回调方法 默认 null
    /// </summary>
    public Func<SynthesizerStatus, Task>? Callback { get; set; }

    /// <summary>
    /// 获得/设置 识别语音文化 默认 zh-CN
    /// </summary>
    public string SpeechSynthesisLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// 获得/设置 结果文化 默认 zh-CN
    /// </summary>
    public string SpeechSynthesisVoiceName { get; set; } = "zh-CN-XiaoxiaoNeural";
}
