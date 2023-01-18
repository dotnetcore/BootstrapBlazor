// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
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

    private ILogger Logger { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="runtime"></param>
    /// <param name="logger"></param>
    public BaiduRecognizerProvider(IOptionsMonitor<BaiduSpeechOption> options, IJSRuntime runtime, ILogger<BaiduRecognizerProvider> logger)
    {
        JSRuntime = runtime;
        SpeechOption = options.CurrentValue;
        Client = new Baidu.Aip.Speech.Asr(SpeechOption.AppId, SpeechOption.ApiKey, SpeechOption.Secret);
        Logger = logger;
    }

    /// <summary>
    /// 回调方法
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task InvokeAsync(RecognizerOption option)
    {
        if (!string.IsNullOrEmpty(option.MethodName))
        {
            Option = option;
            if (Module == null)
            {
                var moduleName = "./_content/BootstrapBlazor.BaiduSpeech/js/recognizer.js";
                Logger.LogInformation("load module {moduleName}", moduleName);
                Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", moduleName);
            }
            Interop ??= DotNetObjectReference.Create(this);
            await Module.InvokeVoidAsync(Option.MethodName, Interop, nameof(RecognizeCallback), Option.AutoRecoginzerElapsedMilliseconds);
        }
    }

    /// <summary>
    /// RecognizeCallback 回调方法
    /// </summary>
    [JSInvokable]
    public async Task RecognizeCallback(RecognizerStatus status, byte[]? bytes)
    {
        Logger.LogInformation("RecognizerStatus: {status}", status);
        string data = "";
        if (status == RecognizerStatus.Finished)
        {
            // 此处同步调用卡 UI 改为异步
            await Task.Run(() =>
            {
                var result = Client.Recognize(bytes, "wav", 16000);
                var err_no = result.Value<int>("err_no");
                if (err_no == 0)
                {
                    var sb = new StringBuilder();
                    var text = result["result"].ToArray();
                    foreach (var item in text)
                    {
                        sb.Append(item.ToString());
                    }
                    data = sb.ToString();
                    Logger.LogInformation("recognizer: {data}", data);
                }
                else
                {
                    Logger.LogError("err_no: {err_no}", err_no);
                }
            });
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
