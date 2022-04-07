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
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task RecognizeOnceAsync(this SpeechService service, Func<string, Task> callback)
    {
        var option = new SpeechOption()
        {
            MethodName = "bb_speech_recognizeOnce",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="service"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task CloseAsync(this SpeechService service, Func<string, Task> callback)
    {
        var option = new SpeechOption()
        {
            MethodName = "bb_close",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }
}
