// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SynthesizerService 扩展操作类
/// </summary>
public static class SynthesizerServiceExtensions
{
    /// <summary>
    /// 语音合成方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="text"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task SynthesizerOnceAsync(this SynthesizerService service, string? text, Func<SynthesizerStatus, Task> callback)
    {
        var option = new SynthesizerOption()
        {
            Text = text,
            MethodName = "bb_speech_synthesizerOnce",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }

    /// <summary>
    /// 关闭语音合成方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static async Task CloseAsync(this SynthesizerService service, Func<SynthesizerStatus, Task> callback)
    {
        var option = new SynthesizerOption()
        {
            MethodName = "bb_close",
            Callback = callback
        };
        await service.InvokeAsync(option);
    }
}
