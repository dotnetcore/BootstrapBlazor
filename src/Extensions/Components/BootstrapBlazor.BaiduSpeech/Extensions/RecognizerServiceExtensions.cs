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
    /// <param name="autoRecoginzerElapsedMilliseconds">默认 5000 毫秒后自动识别，设置 0 时禁用</param>
    public static async Task RecognizeOnceAsync(this RecognizerService service, Func<RecognizerStatus, string?, Task> callback, int? autoRecoginzerElapsedMilliseconds = null)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_recognizeOnce",
            Callback = callback
        };

        // 设置自动识别时间
        if (autoRecoginzerElapsedMilliseconds.HasValue)
        {
            option.AutoRecoginzerElapsedMilliseconds = autoRecoginzerElapsedMilliseconds.Value;
        }
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 语音识别方法
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="callback"></param>
    /// <param name="autoRecoginzerElapsedMilliseconds">默认 5000 毫秒后自动识别，设置 0 时禁用</param>
    public static async Task RecognizeOnceAsync(this IRecognizerProvider provider, Func<RecognizerStatus, string?, Task> callback, int? autoRecoginzerElapsedMilliseconds = null)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_recognizeOnce",
            Callback = callback
        };

        // 设置自动识别时间
        if (autoRecoginzerElapsedMilliseconds.HasValue)
        {
            option.AutoRecoginzerElapsedMilliseconds = autoRecoginzerElapsedMilliseconds.Value;
        }
        await provider.InvokeAsync(option);
    }

    /// <summary>
    /// 关闭语音识别方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task CloseAsync(this RecognizerService service, Func<RecognizerStatus, string?, Task>? callback = null)
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
    public static async Task CloseAsync(this IRecognizerProvider provider, Func<RecognizerStatus, string?, Task>? callback = null)
    {
        var option = new RecognizerOption()
        {
            MethodName = "bb_baidu_speech_close",
            Callback = callback
        };
        await provider.InvokeAsync(option);
    }
}
