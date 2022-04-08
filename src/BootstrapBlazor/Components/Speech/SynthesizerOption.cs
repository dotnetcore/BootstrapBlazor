// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
