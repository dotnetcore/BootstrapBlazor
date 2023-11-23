// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Speeches;

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
