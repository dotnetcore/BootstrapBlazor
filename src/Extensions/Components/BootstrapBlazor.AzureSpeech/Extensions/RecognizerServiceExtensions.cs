// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// RecognizerService 扩展方法
/// </summary>
public static class RecognizerServiceExtensions
{
    /// <summary>
    /// 语音识别方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task RecognizeOnceAsync(this RecognizerService service, Func<string, Task> callback)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_speech_recognizeOnce",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 关闭语音识别方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task CloseAsync(this RecognizerService service, Func<string, Task> callback)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_close",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }
}
