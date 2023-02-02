// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 百度语音合成提供类
/// </summary>
public class BaiduSynthesizerProvider : ISynthesizerProvider, IAsyncDisposable
{
    private DotNetObjectReference<BaiduSynthesizerProvider>? Interop { get; set; }

    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private SynthesizerOption? Option { get; set; }

    private BaiduSpeechOption SpeechOption { get; }

    private IJSRuntime JSRuntime { get; }

    private Baidu.Aip.Speech.Tts Client { get; }

    private ILogger Logger { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="runtime"></param>
    /// <param name="logger"></param>
    public BaiduSynthesizerProvider(IOptionsMonitor<BaiduSpeechOption> options, IJSRuntime runtime, ILogger<BaiduSynthesizerProvider> logger)
    {
        JSRuntime = runtime;
        SpeechOption = options.CurrentValue;
        Client = new Baidu.Aip.Speech.Tts(SpeechOption.ApiKey, SpeechOption.Secret);
        Logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public async Task InvokeAsync(SynthesizerOption option)
    {
        Option = option;

        // 加载模块
        if (Module == null)
        {
            var moduleName = "./_content/BootstrapBlazor.BaiduSpeech/js/synthesizer.js";
            Logger.LogInformation("load module {moduleName}", moduleName);
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", moduleName);
        }
        Interop ??= DotNetObjectReference.Create(this);

        switch (Option.MethodName)
        {
            case "bb_baidu_speech_synthesizerOnce" when !string.IsNullOrEmpty(Option.Text):
                {
                    var result = Client.Synthesis(Option.Text, new Dictionary<string, object>()
                    {
                        { "spd", SpeechOption.Speed }
                    });
                    if (result.Success)
                    {
                        await Module.InvokeVoidAsync(Option.MethodName, Interop, nameof(Callback), result.Data);
                    }
                    Logger.LogInformation("bb_baidu_speech_synthesizerOnce {result}", result.Success);
                    if (!result.Success)
                    {
                        Logger.LogError("{ErrorCode}: {ErrorMsg}", result.ErrorCode, result.ErrorMsg);
                    }
                    break;
                }
            case "bb_baidu_close_synthesizer":
                // 停止语音
                await Module.InvokeVoidAsync(Option.MethodName, Interop, nameof(Callback));
                Logger.LogInformation("bb_baidu_close_synthesizer");
                break;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task Callback(SynthesizerStatus status)
    {
        if (Option.Callback != null)
        {
            await Option.Callback(status);
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
            Interop?.Dispose();
            if (Module != null)
            {
                await Module.DisposeAsync();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
