// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechRecognition 类
/// </summary>
public class WebSpeechRecognition(JSModule module, IComponentIdGenerator componentIdGenerator)
{
    private DotNetObjectReference<WebSpeechRecognition>? _interop;

    private string? _id;

    /// <summary>
    /// fired when the speech recognition service has begun listening to incoming audio with intent to recognize grammars associated with the current SpeechRecognition.
    /// </summary>
    public Func<Task>? OnStartAsync { get; set; }

    /// <summary>
    /// fired when the speech recognition service has disconnected.
    /// </summary>
    public Func<Task>? OnEndAsync { get; set; }

    /// <summary>
    /// fired when sound recognized by the speech recognition service as speech has been detected.
    /// </summary>
    public Func<Task>? OnSpeechStartAsync { get; set; }

    /// <summary>
    /// fired when speech recognized by the speech recognition service has stopped being detected.
    /// </summary>
    public Func<Task>? OnSpeechEndAsync { get; set; }

    /// <summary>
    /// fired when the speech recognition service returns a result — a word or phrase has been positively recognized and this has been communicated back to the app
    /// </summary>
    public Func<WebSpeechRecognitionEvent, Task>? OnResultAsync { get; set; }

    /// <summary>
    /// fired when a speech recognition error occurs.
    /// </summary>
    public Func<WebSpeechRecognitionError, Task>? OnErrorAsync { get; set; }

    /// <summary>
    /// fired when the speech recognition service returns a final result with no significant recognition.
    /// </summary>
    public Func<WebSpeechRecognitionError, Task>? OnNoMatchAsync { get; set; }

    /// <summary>
    /// 开始识别方法
    /// </summary>
    public Task StartAsync(string lang) => StartAsync(new WebSpeechRecognitionOption() { Lang = lang });

    /// <summary>
    /// 开始识别方法
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
    /// 结束识别方法
    /// </summary>
    /// <returns></returns>
    public async Task StopAsync()
    {
        await module.InvokeVoidAsync("stop", _id);
    }

    /// <summary>
    /// 中断识别方法
    /// </summary>
    /// <returns></returns>
    public async Task AbortAsync()
    {
        await module.InvokeVoidAsync("abort", _id);
    }

    /// <summary>
    /// 开始识别回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerStartCallback()
    {
        if (OnStartAsync != null)
        {
            await OnStartAsync();
        }
    }

    /// <summary>
    /// 语音开始回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerSpeechStartCallback()
    {
        if (OnSpeechStartAsync != null)
        {
            await OnSpeechStartAsync();
        }
    }

    /// <summary>
    /// 语音结束回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerSpeechEndCallback()
    {
        if (OnSpeechEndAsync != null)
        {
            await OnSpeechEndAsync();
        }
    }

    /// <summary>
    /// 异常回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerErrorCallback(WebSpeechRecognitionError error)
    {
        if (OnErrorAsync != null)
        {
            await OnErrorAsync(error);
        }
    }

    /// <summary>
    /// 识别结果回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerResultCallback(WebSpeechRecognitionEvent @event)
    {
        if (OnResultAsync != null)
        {
            await OnResultAsync(@event);
        }
    }

    /// <summary>
    /// 无识别结果回调方法由 Javascript 调用
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerNoMatchCallback(WebSpeechRecognitionError error)
    {
        if (OnNoMatchAsync != null)
        {
            await OnNoMatchAsync(error);
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
}
