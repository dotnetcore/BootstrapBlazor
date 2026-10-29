// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Speeches;

/// <summary>
/// 语音合成组件示例代码
/// </summary>
public partial class Synthesizers
{
    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<Synthesizers>? Localizer { get; set; }

    [Inject, NotNull]
    private ILogger<Synthesizers>? Logger { get; set; }

    private bool Start { get; set; }

    private string? InputText { get; set; }

    private string ButtonText { get; set; } = "开始合成";

    private async Task OnStart()
    {
        if (Start)
        {
            await CloseSynthesisAsync();
        }
        else if (!string.IsNullOrEmpty(InputText))
        {
            try
            {
                await SynthesizeOnceAsync(InputText, Synthesizer);
            }
            catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or ArgumentException)
            {
                Logger.LogError(ex, "Speech synthesis sample request failed.");
                await ToastService.Error(Localizer["SynthesizersTitle"], ex.Message);
            }
        }
        else
        {
            await CloseSynthesisAsync();
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
