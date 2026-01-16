// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebSpeechRecognitionEvent 类
///</para>
/// <para lang="en">WebSpeechRecognitionEvent 类
///</para>
/// </summary>
public class WebSpeechRecognitionEvent
{
    /// <summary>
    /// <para lang="zh">获得/设置 识别文本内容
    ///</para>
    /// <para lang="en">Gets or sets 识别文本content
    ///</para>
    /// </summary>
    public string? Transcript { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否已经结束
    ///</para>
    /// <para lang="en">Gets or sets whether已经结束
    ///</para>
    /// </summary>
    public bool IsFinal { get; set; }
}
