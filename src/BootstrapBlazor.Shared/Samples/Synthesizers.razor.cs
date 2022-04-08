// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Synthesizers
{
    [Inject]
    [NotNull]
    private SynthesizerService? SynthesizerService { get; set; }

    private bool Start { get; set; }

    private string? InputText { get; set; }

    private string ButtonText { get; set; } = "开始合成";

    private string ButtonIcon { get; set; } = "fa fa-fw fa-microphone";

    private bool IsDisabled { get; set; }

    private async Task OnStart()
    {
        if (ButtonText == "开始合成")
        {
            IsDisabled = true;
            ButtonIcon = "fa fa-fw fa-spin fa-spinner";
            await SynthesizerService.SynthesizerOnceAsync(InputText, Recognize);
        }
        else
        {
            await Close();
        }
    }

    private Task Recognize(SynthesizerStatus status)
    {
        if (status == SynthesizerStatus.Synthesizer)
        {
            Start = true;
            IsDisabled = false;
            ButtonIcon = "fa fa-fw fa-spin fa-spinner";
            ButtonText = "停止合成";
        }
        else
        {
            Start = false;
            IsDisabled = false;
            ButtonIcon = "fa fa-fw fa-microphone";
            ButtonText = "开始合成";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task Close()
    {
        await SynthesizerService.CloseAsync(Recognize);
    }
}
