// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 语音识别配置类
/// </summary>
public class RecognizerOption
{
    /// <summary>
    /// 获得/设置 语音识别指令名称 默认 null
    /// </summary>
    public string? MethodName { get; set; }

    /// <summary>
    /// 获得/设置 回调方法 默认 null
    /// </summary>
    public Func<string, Task>? Callback { get; set; }

    /// <summary>
    /// 获得/设置 识别语音文化 默认 zh-CN
    /// </summary>
    public string SpeechRecognitionLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// 获得/设置 结果文化 默认 zh-CN
    /// </summary>
    public string TargetLanguage { get; set; } = "zh-CN";
}
