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

    private bool Start { get; set; }

    private string? Result { get; set; }

    private string ButtonText { get; set; } = "开始识别";

    private async Task OnStart()
    {
        if (ButtonText == "开始识别")
        {
            Start = true;
            ButtonText = "结束识别";
            await SpeechService.RecognizeOnceAsync(Recognize);
        }
        else
        {
            await Close();
        }
    }

    private async Task OnTimeout()
    {
        await Close();
        StateHasChanged();
    }

    private Task Recognize(string result)
    {
        Result = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task Close()
    {
        Start = false;
        ButtonText = "开始识别";
        await SpeechService.CloseAsync(Recognize);
    }
}
