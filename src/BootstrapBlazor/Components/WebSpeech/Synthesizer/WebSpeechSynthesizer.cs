// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebSpeechSynthesizer 类</para>
/// <para lang="en">WebSpeechSynthesizer 类</para>
/// </summary>
public class WebSpeechSynthesizer(JSModule module, IComponentIdGenerator componentIdGenerator)
{
    private DotNetObjectReference<WebSpeechSynthesizer>? _interop;

    private string? _id;

    /// <summary>
    /// <para lang="zh">获得/设置 朗读结束回调方法 默认 null</para>
    /// <para lang="en">Gets or sets 朗读结束callback method Default is null</para>
    /// </summary>
    public Func<Task>? OnEndAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 发生错误回调方法 默认 null</para>
    /// <para lang="en">Gets or sets 发生错误callback method Default is null</para>
    /// </summary>
    public Func<WebSpeechSynthesisError, Task>? OnErrorAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得 语音包方法</para>
    /// <para lang="en">Gets 语音包方法</para>
    /// </summary>
    /// <returns></returns>
    public async Task<WebSpeechSynthesisVoice[]?> GetVoices() => await module.InvokeAsync<WebSpeechSynthesisVoice[]?>("getVoices");

    /// <summary>
    /// <para lang="zh">开始朗读方法</para>
    /// <para lang="en">开始朗读方法</para>
    /// </summary>
    /// <param name="utterance"></param>
    public async Task SpeakAsync(WebSpeechSynthesisUtterance utterance)
    {
        _id = componentIdGenerator.Generate(this);
        _interop = DotNetObjectReference.Create(this);
        await module.InvokeVoidAsync("speak", _id, _interop, utterance);
    }

    /// <summary>
    /// <para lang="zh">开始朗读方法</para>
    /// <para lang="en">开始朗读方法</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="lang"></param>
    public Task SpeakAsync(string? text, string? lang) => SpeakAsync(new WebSpeechSynthesisUtterance()
    {
        Text = text,
        Lang = lang
    });

    /// <summary>
    /// <para lang="zh">开始朗读方法</para>
    /// <para lang="en">开始朗读方法</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="voice"></param>
    public Task SpeakAsync(string? text, WebSpeechSynthesisVoice? voice = null) => SpeakAsync(new WebSpeechSynthesisUtterance()
    {
        Text = text,
        Voice = voice
    });

    /// <summary>
    /// <para lang="zh">暂停朗读方法</para>
    /// <para lang="en">暂停朗读方法</para>
    /// </summary>
    /// <returns></returns>
    public async Task PauseAsync()
    {
        await module.InvokeVoidAsync("pause", _id);
    }

    /// <summary>
    /// <para lang="zh">恢复朗读方法</para>
    /// <para lang="en">恢复朗读方法</para>
    /// </summary>
    /// <returns></returns>
    public async Task ResumeAsync()
    {
        await module.InvokeVoidAsync("resume", _id);
    }

    /// <summary>
    /// <para lang="zh">取消朗读方法</para>
    /// <para lang="en">取消朗读方法</para>
    /// </summary>
    /// <returns></returns>
    public async Task CancelAsync()
    {
        await module.InvokeVoidAsync("cancel", _id);
    }

    /// <summary>
    /// <para lang="zh">朗读异常回调方法由 Javascript 调用</para>
    /// <para lang="en">朗读exceptioncallback method由 Javascript 调用</para>
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
    /// <para lang="zh">朗读结束回调方法由 Javascript 调用</para>
    /// <para lang="en">朗读结束callback method由 Javascript 调用</para>
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
    /// <para lang="zh">正在朗读回调方法由 Javascript 调用</para>
    /// <para lang="en">正在朗读callback method由 Javascript 调用</para>
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
