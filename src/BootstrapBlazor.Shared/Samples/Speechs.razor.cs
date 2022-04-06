// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Speechs
{
    [Inject]
    [NotNull]
    private SpeechService? SpeechService { get; set; }

    [Inject]
    [NotNull]
    private IServiceProvider? ServiceProvider { get; set; }

    [Inject]
    [NotNull]
    private ISpeechProvider? SpeechProvider { get; set; }

    private string? Result { get; set; }

    private async Task OnStart()
    {
        await SpeechService.RecognizeOnceAsync(new SpeechOption()
        {
            ServiceProvider = ServiceProvider,
            Callback = Recognize,
            Provider = SpeechProvider
        });
    }

    private Task Recognize(string result)
    {
        Result = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task OnStop()
    {
        await SpeechService.CloseAsync(new SpeechOption()
        {
            ServiceProvider = ServiceProvider,
            Callback = Recognize,
            Provider = SpeechProvider
        });
    }
}
