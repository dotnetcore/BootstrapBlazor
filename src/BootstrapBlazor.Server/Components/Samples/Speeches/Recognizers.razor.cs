// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Speeches;

/// <summary>
/// 语音识别组件示例代码
/// </summary>
public partial class Recognizers
{
    private bool Start { get; set; }

    private string? Result { get; set; }

    private string ButtonText { get; set; } = "开始识别";

    private async Task OnStart()
    {
        if (ButtonText == "开始识别")
        {
            await RecognizerService.RecognizeOnceAsync(Recognize, 5000);
        }
        else
        {
            Start = false;
            ButtonText = "开始识别";
            StateHasChanged();
            await RecognizerService.CloseAsync(Recognize);
        }
    }

    private async Task OnTimeout()
    {
        await RecognizerService.CloseAsync(Recognize);
    }

    private Task Recognize(RecognizerStatus status, string? result)
    {
        if (status == RecognizerStatus.Start)
        {
            Start = true;
            ButtonText = "结束识别";
        }
        else if (status == RecognizerStatus.Finished)
        {
            Result = result;
            Start = false;
            ButtonText = "开始识别";
        }
        else
        {
            Result = "";
            Start = false;
            ButtonText = "开始识别";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }
}
