// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Recognizers
{
    [Inject]
    [NotNull]
    private IEnumerable<IRecognizerProvider>? RecognizerProviders { get; set; }

    [NotNull]
    private IRecognizerProvider? RecognizerProvider { get; set; }

    private bool Start { get; set; }

    private string? Result { get; set; }

    private string ButtonText { get; set; } = "开始识别";

    [NotNull]
    private Func<Func<string, Task>, Task>? RecognizerInvokeAsync { get; set; }

    [NotNull]
    private Func<Func<string, Task>, Task>? CloseInvokeAsync { get; set; }

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
            RecognizerProvider = RecognizerProviders.OfType<AzureRecognizerProvider>().FirstOrDefault();
            RecognizerInvokeAsync = RecognizerProvider.AzureRecognizeOnceAsync;
            CloseInvokeAsync = RecognizerProvider.AzureCloseAsync;
        }
        else
        {
            RecognizerProvider = RecognizerProviders.OfType<BaiduRecognizerProvider>().FirstOrDefault();
            RecognizerInvokeAsync = RecognizerProvider.BaiduRecognizeOnceAsync;
            CloseInvokeAsync = RecognizerProvider.BaiduCloseAsync;
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
        if (ButtonText == "开始识别")
        {
            if (SpeechItem == "Azure")
            {
                Start = true;
                ButtonText = "结束识别";
            }
            await RecognizerInvokeAsync(Recognize);
        }
        else
        {
            await Close();
        }
    }

    private async Task OnTimeout()
    {
        await Close();
    }

    private Task Recognize(string result)
    {
        if (SpeechItem == "Baidu" && result == RecognizerStatus.Start.ToString())
        {
            Start = true;
            ButtonText = "结束识别";
        }
        else
        {
            Result = result;
            Start = false;
            ButtonText = "开始识别";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task Close() => CloseInvokeAsync(Recognize);
}
