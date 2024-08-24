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
    /// 获得 语音包方法
    /// </summary>
    /// <returns></returns>
    public async Task<WebSpeechSynthesisVoice[]> GetVoices() => await module.InvokeAsync<WebSpeechSynthesisVoice[]>("getVoices");

    /// <summary>
    /// 开始朗读方法
    /// </summary>
    /// <param name="utterance"></param>
    public async Task SpeakAsync(WebSpeechSynthesisUtterance utterance)
    {
        _id = componentIdGenerator.Generate(this);
        _interop = DotNetObjectReference.Create(this);
        await module.InvokeVoidAsync("speak", _id, _interop, utterance);
    }

    /// <summary>
    /// 开始朗读方法
    /// </summary>
    /// <param name="text"></param>
    /// <param name="lang"></param>
    public Task SpeakAsync(string? text, string? lang) => SpeakAsync(new WebSpeechSynthesisUtterance()
    {
        Text = text,
        Lang = lang
    });

    /// <summary>
    /// 开始朗读方法
    /// </summary>
    /// <param name="text"></param>
    /// <param name="voice"></param>
    public Task SpeakAsync(string? text, WebSpeechSynthesisVoice? voice = null) => SpeakAsync(new WebSpeechSynthesisUtterance()
    {
        Text = text,
        Voice = voice
    });

    /// <summary>
    /// 暂停朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task PauseAsync()
    {
        await module.InvokeVoidAsync("pause", _id);
    }

    /// <summary>
    /// 恢复朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task ResumeAsync()
    {
        await module.InvokeVoidAsync("resume", _id);
    }

    /// <summary>
    /// 取消朗读方法
    /// </summary>
    /// <returns></returns>
    public async Task CancelAsync()
    {
        await module.InvokeVoidAsync("cancel", _id);
    }

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
    public Task TriggerSpeakingCallback() => TriggerErrorCallback(new WebSpeechSynthesisError()
    {
        CharIndex = 0,
        ElapsedTime = 0,
        Error = "speaking"
    });
}
