// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
/// Web Speech 服务
/// </summary>
public class WebSpeechService(IJSRuntime runtime, IComponentIdGenerator ComponentIdGenerator, ILogger<WebSpeechService> logger)
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
            var moduleName = "./_content/BootstrapBlazor/modules/synthesis.js";
            logger.LogInformation("load module {moduleName}", moduleName);
            SynthesisModule = await runtime.LoadModule(moduleName);
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
            var moduleName = "./_content/BootstrapBlazor/modules/recognition.js";
            logger.LogInformation("load module {moduleName}", moduleName);
            RecognitionModule = await runtime.LoadModule(moduleName);
        }
        return new WebSpeechRecognition(RecognitionModule, ComponentIdGenerator);
    }
}
