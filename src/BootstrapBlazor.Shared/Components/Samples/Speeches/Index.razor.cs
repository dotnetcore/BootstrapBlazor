// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Speeches;

/// <summary>
/// Speeches 示例
/// </summary>
public partial class Index
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Index>? Localizer { get; set; }

    private List<ConsoleMessageItem> ConsoleMessages { get; } = [];

    private bool Show { get; set; }

    [NotNull]
    private static string NugetPackageName => "BootstrapBlazor.BaiduSpeech";

    private int TotalTime { get; set; } = 5000;

    private async Task OnStart()
    {
        TotalTime = 5000;
        await RecognizeOnceAsync(Recognizer, TotalTime);
    }

    private async Task Recognizer(RecognizerStatus status, string? result)
    {
        if (status == RecognizerStatus.Start)
        {
            Show = true;
            StateHasChanged();
        }
        else if (status == RecognizerStatus.Finished)
        {
            Show = false;
            ConsoleMessages.Add(new ConsoleMessageItem()
            {
                Message = result ?? "",
                Color = Color.Success
            });

            await ConfirmAction(result ?? "");
            StateHasChanged();
        }
        else if (status == RecognizerStatus.Error)
        {
            Show = false;
            ConsoleMessages.Add(new ConsoleMessageItem()
            {
                Message = result ?? Localizer["SpeechRequestFailed"].Value,
                Color = Color.Danger
            });
            StateHasChanged();
        }
    }

    private async Task ConfirmAction(string result)
    {
        if (CheckReceivedData(result))
        {
            var text = "您确认要把灯打开吗？请确认";
            try
            {
                await SynthesizeOnceAsync(text, async status =>
                {
                    if (status == SynthesizerStatus.Synthesizer)
                    {
                        ConsoleMessages.Add(new ConsoleMessageItem()
                        {
                            Message = text,
                            Color = Color.Warning
                        });
                        await InvokeAsync(StateHasChanged);
                    }
                    if (status == SynthesizerStatus.Finished)
                    {
                        await RecognizerConfirm();
                    }
                });
            }
            catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or ArgumentException)
            {
                ConsoleMessages.Add(new ConsoleMessageItem()
                {
                    Message = ex.Message,
                    Color = Color.Danger
                });
                StateHasChanged();
            }
        }
    }

    private async Task RecognizerConfirm()
    {
        Show = true;
        TotalTime = 3000;
        StateHasChanged();
        await Task.Delay(300);
        await RecognizeOnceAsync(Confirm, TotalTime);
    }

    private async Task Confirm(RecognizerStatus status, string? result)
    {
        if (status == RecognizerStatus.Start)
        {
            Show = true;
            StateHasChanged();
            return;
        }

        Show = false;
        StateHasChanged();
        result ??= "";

        if (status == RecognizerStatus.Finished && result.Contains("确认"))
        {
            ConsoleMessages.Add(new ConsoleMessageItem()
            {
                Message = result,
                Color = Color.Success
            });
            ConsoleMessages.Add(new ConsoleMessageItem()
            {
                Message = "指令发送中...",
                Color = Color.Warning
            });
            StateHasChanged();

            //模拟后台执行任务
            await Task.Delay(2000);

            try
            {
                await SynthesizeOnceAsync("已经为您打开", status =>
                {
                    if (status == SynthesizerStatus.Synthesizer)
                    {
                        ConsoleMessages.Add(new ConsoleMessageItem()
                        {
                            Message = "已经为您打开",
                            Color = Color.Danger
                        });
                        StateHasChanged();
                    }
                    return Task.CompletedTask;
                });
            }
            catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or ArgumentException)
            {
                ConsoleMessages.Add(new ConsoleMessageItem()
                {
                    Message = ex.Message,
                    Color = Color.Danger
                });
                StateHasChanged();
            }
        }
    }

    private static bool CheckReceivedData(string result) => result.Contains('灯') && result.Contains("打开");
}
