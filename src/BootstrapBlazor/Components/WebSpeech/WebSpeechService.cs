// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Web Speech 服务
/// </summary>
public class WebSpeechService(IJSRuntime runtime, IComponentIdGenerator ComponentIdGenerator)
{
    private JSModule? SynthesisModule { get; set; }

    private JSModule? RecognitionModule { get; set; }

    /// <summary>
    /// 语音合成方法
    /// </summary>
    /// <returns></returns>
    public async Task<WebSpeechSynthesizer> CreateSynthesizerAsync()
    {
        if (SynthesisModule == null)
        {
            SynthesisModule = await runtime.LoadModuleByName("synthesis");
        }
        return new WebSpeechSynthesizer(SynthesisModule, ComponentIdGenerator);
    }

    /// <summary>
    /// 语音识别方法
    /// </summary>
    /// <returns></returns>
    public async Task<WebSpeechRecognition> CreateRecognitionAsync()
    {
        if (RecognitionModule == null)
        {
            RecognitionModule = await runtime.LoadModuleByName("recognition");
        }
        return new WebSpeechRecognition(RecognitionModule, ComponentIdGenerator);
    }
}
