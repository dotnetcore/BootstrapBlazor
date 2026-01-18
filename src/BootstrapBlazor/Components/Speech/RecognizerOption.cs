// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">语音识别配置类</para>
/// <para lang="en">Speech Recognition Option Class</para>
/// </summary>
public class RecognizerOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 语音识别指令名称 默认 null</para>
    /// <para lang="en">Gets or sets Speech Recognition Command Name. Default null</para>
    /// </summary>
    public string? MethodName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Callback Method. Default null</para>
    /// </summary>
    public Func<RecognizerStatus, string?, Task>? Callback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 识别语音文化 默认 zh-CN</para>
    /// <para lang="en">Gets or sets Speech Recognition Culture. Default zh-CN</para>
    /// </summary>
    public string SpeechRecognitionLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// <para lang="zh">获得/设置 结果文化 默认 zh-CN</para>
    /// <para lang="en">Gets or sets Result Culture. Default zh-CN</para>
    /// </summary>
    public string TargetLanguage { get; set; } = "zh-CN";

    /// <summary>
    /// <para lang="zh">获得/设置 自动识别时间 默认 5000 设置 0 时禁用需要手动关闭</para>
    /// <para lang="en">Gets or sets Auto Recognition Time. Default 5000. Set 0 to disable and need to close manually</para>
    /// </summary>
    public int AutoRecoginzerElapsedMilliseconds { get; set; } = 5000;
}
