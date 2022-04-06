// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SpeechService 扩展方法
/// </summary>
public static class SpeechServiceExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    public static async Task RecognizeOnceAsync(this SpeechService service, SpeechOption option)
    {
        option.MethodName = "bb_speech_recognizeOnce";
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    /// <param name="option"></param>
    /// <returns></returns>
    public static async Task CloseAsync(this SpeechService service, SpeechOption option)
    {
        option.MethodName = "bb_close";
        await service.InvokeAsync(option);
    }
}
