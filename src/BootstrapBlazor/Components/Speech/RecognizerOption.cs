// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
    public Func<RecognizerStatus, string?, Task>? Callback { get; set; }

    /// <summary>
    /// 获得/设置 识别语音文化 默认 zh-CN
    /// </summary>
    public string SpeechRecognitionLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// 获得/设置 结果文化 默认 zh-CN
    /// </summary>
    public string TargetLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// 获得/设置 自动识别时间 默认 5000 设置 0 时禁用需要手动关闭
    /// </summary>
    public int AutoRecoginzerElapsedMilliseconds { get; set; } = 5000;
}
