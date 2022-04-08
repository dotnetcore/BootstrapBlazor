// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class AzureRecognizerProvider : IRecognizerProvider, IAsyncDisposable
{
    private DotNetObjectReference<AzureRecognizerProvider>? Interop { get; set; }

    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private RecognizerOption? Option { get; set; }

    private AzureSpeechOption SpeechOption { get; }

    private IJSRuntime JSRuntime { get; }

    private HttpClient Client { get; }

    private IMemoryCache Cache { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="runtime"></param>
    /// <param name="factory"></param>
    /// <param name="cache"></param>
    public AzureRecognizerProvider(IOptions<AzureSpeechOption> options, IJSRuntime runtime, IHttpClientFactory factory, IMemoryCache cache)
    {
        Cache = cache;
        JSRuntime = runtime;
        SpeechOption = options.Value;
        Client = factory.CreateClient();
        Client.BaseAddress = new Uri(string.Format(SpeechOption.AuthorizationTokenUrl, SpeechOption.Region));
        if (SpeechOption.Timeout > 0)
        {
            Client.Timeout = TimeSpan.FromMilliseconds(SpeechOption.Timeout);
        }
        Client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SpeechOption.SubscriptionKey);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public async Task InvokeAsync(RecognizerOption option)
    {
        if (string.IsNullOrEmpty(option.MethodName))
        {
            throw new InvalidOperationException();
        }

        // 通过 SubscriptionKey 交换 Token
        var token = await ExchangeToken();

        Option = option;
        if (Module == null)
        {
            Module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.AzureSpeech/js/recognizer.js");
        }
        Interop ??= DotNetObjectReference.Create(this);
        await Module.InvokeVoidAsync(Option.MethodName, Interop, nameof(Callback), token, SpeechOption.Region, option.SpeechRecognitionLanguage, option.TargetLanguage);
    }

    private Task<string> ExchangeToken() => Cache.GetOrCreateAsync(SpeechOption.SubscriptionKey, async entry =>
    {
        var url = string.Format(SpeechOption.AuthorizationTokenUrl, SpeechOption.Region);
        var ret = "";
        try
        {
            var result = await Client.PostAsJsonAsync<string>(url, "");
            if (result.IsSuccessStatusCode)
            {
                ret = await result.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(ret))
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(9);
                }
            }
        }
        catch
        {

        }
        return ret;
    });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task Callback(string result)
    {
        if (Option.Callback != null)
        {
            await Option.Callback(result);
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
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
