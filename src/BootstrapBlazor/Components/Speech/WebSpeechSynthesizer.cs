// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechSynthesizer 类
/// </summary>
public class WebSpeechSynthesizer(JSModule module, IComponentIdGenerator componentIdGenerator)
{
    private DotNetObjectReference<WebSpeechSynthesizer>? _interop;

    private string? _id;

    /// <summary>
    /// 获得/设置 朗读结束回调方法 默认 null
    /// </summary>
    public Func<Task>? OnEndAsync { get; set; }

    /// <summary>
    /// 获得/设置 发生错误回调方法 默认 null
    /// </summary>
    public Func<WebSpeechSynthesisError, Task>? OnErrorAsync { get; set; }

    /// <summary>
    /// 开始朗读方法
    /// </summary>
    /// <param name="text"></param>
    /// <param name="lang"></param>
    public async Task SpeakAsync(string? text, string? lang)
    {
        _id = componentIdGenerator.Generate(this);
        _interop = DotNetObjectReference.Create(this);
        await module.InvokeVoidAsync("speak", _id, _interop, new { text, lang });
    }

    /// <summary>
    /// 开始朗读方法
    /// </summary>
    /// <param name="text"></param>
    /// <param name="voice"></param>
    public async Task SpeakAsync(string? text, WebSpeechSynthesisVoice? voice = null)
    {
        _id = componentIdGenerator.Generate(this);
        _interop = DotNetObjectReference.Create(this);
        await module.InvokeVoidAsync("speak", _id, _interop, new { text, voice });
    }

    /// <summary>
    /// 暂停朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task Pause()
    {
        await module.InvokeVoidAsync("pause", _id);
    }

    /// <summary>
    /// 恢复朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task Resume()
    {
        await module.InvokeVoidAsync("resume", _id);
    }

    /// <summary>
    /// 获得 语音包方法
    /// </summary>
    /// <returns></returns>
    public async Task<WebSpeechSynthesisVoice[]> GetVoices() => await module.InvokeAsync<WebSpeechSynthesisVoice[]>("getVoices");

    /// <summary>
    /// 朗读异常回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerErrorCallback(WebSpeechSynthesisError error)
    {
        if (OnErrorAsync != null)
        {
            await OnErrorAsync(error);
        }
    }

    /// <summary>
    /// 朗读结束回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerEndCallback()
    {
        if (OnEndAsync != null)
        {
            await OnEndAsync();
        }
    }

    /// <summary>
    /// 正在朗读回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerSpeakingCallback()
    {
        await Task.CompletedTask;
    }
}
