// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class AzureSpeechProvider : ISpeechProvider, IAsyncDisposable
{
    private DotNetObjectReference<AzureSpeechProvider>? Interop { get; set; }

    private IJSObjectReference? Module { get; set; }

    [NotNull]
    private ProviderOption? Option { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public async Task InvokeAsync(ProviderOption option)
    {
        if (string.IsNullOrEmpty(option.MethodName)) throw new ArgumentNullException(nameof(option.MethodName));

        Option = option;
        if (Option.ServiceProvider != null)
        {
            if (Module == null)
            {
                var jsRuntime = Option.ServiceProvider.GetRequiredService<IJSRuntime>();
                Module = await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BootstrapBlazor.Speech/js/speech.js");
            }
            Interop ??= DotNetObjectReference.Create(this);
            await Module.InvokeVoidAsync(Option.MethodName, Interop, nameof(Callback));
        }
    }

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
