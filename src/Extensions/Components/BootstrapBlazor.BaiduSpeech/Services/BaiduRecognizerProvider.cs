// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// 百度语音识别提供类
/// </summary>
public class BaiduRecognizerProvider : IRecognizerProvider, IAsyncDisposable
{
    private DotNetObjectReference<BaiduRecognizerProvider>? Interop { get; set; }

    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private RecognizerOption? Option { get; set; }

    private BaiduSpeechOption SpeechOption { get; }

    private IJSRuntime JSRuntime { get; }

    private Baidu.Aip.Speech.Asr Client { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="runtime"></param>
    public BaiduRecognizerProvider(IOptionsMonitor<BaiduSpeechOption> options, IJSRuntime runtime)
    {
        JSRuntime = runtime;
        SpeechOption = options.CurrentValue;
        Client = new Baidu.Aip.Speech.Asr(SpeechOption.AppId, SpeechOption.ApiKey, SpeechOption.Secret);
    }

    /// <summary>
    /// 回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task InvokeAsync(RecognizerOption option)
    {
        if (string.IsNullOrEmpty(option.MethodName))
        {
            throw new InvalidOperationException();
        }

        Option = option;
        if (Module == null)
        {
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "/_content/BootstrapBlazor.BaiduSpeech/js/recognizer.js");
        }
        Interop ??= DotNetObjectReference.Create(this);
        await Module.InvokeVoidAsync(Option.MethodName, Interop, nameof(RecognizeCallback), Option.AutoRecoginzerElapsedMilliseconds);
    }

    /// <summary>
    /// RecognizeCallback 回调方法
    /// </summary>
    [JSInvokable]
    public async Task RecognizeCallback(RecognizerStatus status, byte[]? bytes)
    {
        string data = "Error";
        if (status == RecognizerStatus.Finished)
        {
            var result = Client.Recognize(bytes, "wav", 16000);
            var sb = new StringBuilder();
            var text = result["result"].ToArray();
            foreach (var item in text)
            {
                sb.Append(item.ToString());
            }
            data = sb.ToString();
        }

        if (Option.Callback != null)
        {
            await Option.Callback(status, data);
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
            }
            if (Module is not null)
            {
                await Module.DisposeAsync();
            }
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
