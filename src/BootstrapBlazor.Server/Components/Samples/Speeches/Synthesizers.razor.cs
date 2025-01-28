// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Speeches;

/// <summary>
/// 语音合成组件示例代码
/// </summary>
public partial class Synthesizers
{
    private bool Start { get; set; }

    private string? InputText { get; set; }

    private string ButtonText { get; set; } = "开始合成";

    private async Task OnStart()
    {
        if (ButtonText == "开始合成")
        {
            if (!string.IsNullOrEmpty(InputText))
            {
                try
                {
                    await SynthesizerService.SynthesizerOnceAsync(InputText, Synthesizer);
                }
                catch (Exception)
                {
                }
            }
            else
            {
                await SynthesizerService.CloseAsync(Synthesizer);
            }
        }
    }

    private Task Synthesizer(SynthesizerStatus status)
    {
        if (status == SynthesizerStatus.Synthesizer)
        {
            Start = true;
            ButtonText = "停止合成";
        }
        else
        {
            Start = false;
            ButtonText = "开始合成";
        }
        StateHasChanged();
        return Task.CompletedTask;
    }
}
