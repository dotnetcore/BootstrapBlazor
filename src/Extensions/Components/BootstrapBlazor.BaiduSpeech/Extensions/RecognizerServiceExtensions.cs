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
    public static async Task BaiduRecognizeOnceAsync(this RecognizerService service, Func<string, Task> callback)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_recognizeOnce",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 语音识别方法
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task BaiduRecognizeOnceAsync(this IRecognizerProvider provider, Func<string, Task> callback)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_recognizeOnce",
            Callback = callback
        };
        await provider.InvokeAsync(option);
    }

    /// <summary>
    /// 关闭语音识别方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task BaiduCloseAsync(this RecognizerService service, Func<string, Task> callback)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_close",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 关闭语音识别方法
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task BaiduCloseAsync(this IRecognizerProvider provider, Func<string, Task> callback)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_close",
            Callback = callback
        };
        await provider.InvokeAsync(option);
    }
}
