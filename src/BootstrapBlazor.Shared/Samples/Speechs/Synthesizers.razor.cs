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
    private IEnumerable<ISynthesizerProvider>? SynthesizerProviders { get; set; }

    [NotNull]
    private ISynthesizerProvider? SynthesizerProvider { get; set; }

    private bool Start { get; set; }

    private string? InputText { get; set; }

    private string ButtonText { get; set; } = "开始合成";

    private string ButtonIcon { get; set; } = "fa fa-fw fa-microphone";

    private bool IsDisabled { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        InitProvider();
    }

    private void InitProvider()
    {
        if (SpeechItem == "Azure")
        {
            SynthesizerProvider = SynthesizerProviders.OfType<AzureSynthesizerProvider>().FirstOrDefault();
        }
        else
        {
            SynthesizerProvider = SynthesizerProviders.OfType<BaiduSynthesizerProvider>().FirstOrDefault();
        }
    }

    private Task OnSpeechProviderChanged(string value)
    {
        SpeechItem = value;
        InitProvider();
        return Task.CompletedTask;
    }

    private async Task OnStart()
    {
        if (ButtonText == "开始合成")
        {
            IsDisabled = true;
            ButtonIcon = "fa fa-fw fa-spin fa-spinner";
            if (SpeechItem == "Azure")
            {
                await SynthesizerProvider.AzureSynthesizerOnceAsync(InputText, Recognize);
            }
            else
            {
                await SynthesizerProvider.BaiduSynthesizerOnceAsync(InputText, Recognize);
            }
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
            ButtonIcon = "fa fa-fw fa-microphone";
            ButtonText = "开始合成";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task Close()
    {
        if (SpeechItem == "Azure")
        {
            await SynthesizerProvider.AzureCloseAsync(Recognize);
        }
        else
        {
            await SynthesizerProvider.BaiduCloseAsync(Recognize);
        }
    }
}
