// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">WebSpeechRecognition 类</para>
/// <para lang="en">WebSpeechRecognition 类</para>
/// </summary>
public class WebSpeechRecognition(JSModule module, IComponentIdGenerator componentIdGenerator)
{
    private DotNetObjectReference<WebSpeechRecognition>? _interop;

    private string? _id;

    /// <summary>
    /// <para lang="zh">fired when the speech recognition service has begun listening to incoming audio with intent to recognize grammars associated with the current SpeechRecognition.</para>
    /// <para lang="en">fired when the speech recognition service has begun listening to incoming audio with intent to recognize grammars associated with the current SpeechRecognition.</para>
    /// </summary>
    public Func<Task>? OnStartAsync { get; set; }

    /// <summary>
    /// <para lang="zh">fired when the speech recognition service has disconnected.</para>
    /// <para lang="en">fired when the speech recognition service has disconnected.</para>
    /// </summary>
    public Func<Task>? OnEndAsync { get; set; }

    /// <summary>
    /// <para lang="zh">fired when sound recognized by the speech recognition service as speech has been detected.</para>
    /// <para lang="en">fired when sound recognized by the speech recognition service as speech has been detected.</para>
    /// </summary>
    public Func<Task>? OnSpeechStartAsync { get; set; }

    /// <summary>
    /// <para lang="zh">fired when speech recognized by the speech recognition service has stopped being detected.</para>
    /// <para lang="en">fired when speech recognized by the speech recognition service has stopped being detected.</para>
    /// </summary>
    public Func<Task>? OnSpeechEndAsync { get; set; }

    /// <summary>
    /// <para lang="zh">fired when the speech recognition service returns a result — a word or phrase has been positively recognized and this has been communicated back to the app</para>
    /// <para lang="en">fired when the speech recognition service returns a result — a word or phrase has been positively recognized and this has been communicated back to the app</para>
    /// </summary>
    public Func<WebSpeechRecognitionEvent, Task>? OnResultAsync { get; set; }

    /// <summary>
    /// <para lang="zh">fired when a speech recognition error occurs.</para>
    /// <para lang="en">fired when a speech recognition error occurs.</para>
    /// </summary>
    public Func<WebSpeechRecognitionError, Task>? OnErrorAsync { get; set; }

    /// <summary>
    /// <para lang="zh">fired when the speech recognition service returns a final result with no significant recognition.</para>
    /// <para lang="en">fired when the speech recognition service returns a final result with no significant recognition.</para>
    /// </summary>
    public Func<WebSpeechRecognitionError, Task>? OnNoMatchAsync { get; set; }

    /// <summary>
    /// <para lang="zh">开始识别方法</para>
    /// <para lang="en">开始识别方法</para>
    /// </summary>
    public Task StartAsync(string lang) => StartAsync(new WebSpeechRecognitionOption() { Lang = lang });

    /// <summary>
    /// <para lang="zh">开始识别方法</para>
    /// <para lang="en">开始识别方法</para>
    /// </summary>
    public async Task StartAsync(WebSpeechRecognitionOption option)
    {
        _id = componentIdGenerator.Generate(this);
        _interop = DotNetObjectReference.Create(this);
        await module.InvokeVoidAsync("start", _id, _interop, new
        {
            TriggerStart = OnStartAsync != null,
            TriggerSpeechStart = OnSpeechStartAsync != null,
            TriggerSpeechEnd = OnSpeechEndAsync != null,
            TriggerNoMatch = OnNoMatchAsync != null,
            TriggerEnd = OnEndAsync != null,
            TriggerError = OnErrorAsync != null
        }, option);
    }

    /// <summary>
    /// <para lang="zh">结束识别方法</para>
    /// <para lang="en">结束识别方法</para>
    /// </summary>
    public async Task StopAsync()
    {
        await module.InvokeVoidAsync("stop", _id);
    }

    /// <summary>
    /// <para lang="zh">中断识别方法</para>
    /// <para lang="en">中断识别方法</para>
    /// </summary>
    public async Task AbortAsync()
    {
        await module.InvokeVoidAsync("abort", _id);
    }

    /// <summary>
    /// <para lang="zh">开始识别回调方法由 Javascript 调用</para>
    /// <para lang="en">开始识别callback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerStartCallback()
    {
        if (OnStartAsync != null)
        {
            await OnStartAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">语音开始回调方法由 Javascript 调用</para>
    /// <para lang="en">语音开始callback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerSpeechStartCallback()
    {
        if (OnSpeechStartAsync != null)
        {
            await OnSpeechStartAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">语音结束回调方法由 Javascript 调用</para>
    /// <para lang="en">语音结束callback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerSpeechEndCallback()
    {
        if (OnSpeechEndAsync != null)
        {
            await OnSpeechEndAsync();
        }
    }

    /// <summary>
    /// <para lang="zh">异常回调方法由 Javascript 调用</para>
    /// <para lang="en">exceptioncallback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerErrorCallback(WebSpeechRecognitionError error)
    {
        if (OnErrorAsync != null)
        {
            await OnErrorAsync(error);
        }
    }

    /// <summary>
    /// <para lang="zh">识别结果回调方法由 Javascript 调用</para>
    /// <para lang="en">识别结果callback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerResultCallback(WebSpeechRecognitionEvent @event)
    {
        if (OnResultAsync != null)
        {
            await OnResultAsync(@event);
        }
    }

    /// <summary>
    /// <para lang="zh">无识别结果回调方法由 Javascript 调用</para>
    /// <para lang="en">无识别结果callback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerNoMatchCallback(WebSpeechRecognitionError error)
    {
        if (OnNoMatchAsync != null)
        {
            await OnNoMatchAsync(error);
        }
    }

    /// <summary>
    /// <para lang="zh">朗读结束回调方法由 Javascript 调用</para>
    /// <para lang="en">朗读结束callback method由 Javascript 调用</para>
    /// </summary>
    [JSInvokable]
    public async Task TriggerEndCallback()
    {
        if (OnEndAsync != null)
        {
            await OnEndAsync();
        }
    }
}
