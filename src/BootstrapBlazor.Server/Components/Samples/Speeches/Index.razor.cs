// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;

namespace BootstrapBlazor.Shared.Samples.Speeches;

/// <summary>
/// Speeches 示例
/// </summary>
public partial class Index
{
    [Inject]
    [NotNull]
    private PackageVersionService? VersionManager { get; set; }

    [Inject]
    [NotNull]
    private RecognizerService? RecognizerService { get; set; }

    [Inject]
    [NotNull]
    private SynthesizerService? SynthesizerService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Index>? Localizer { get; set; }

    private List<ConsoleMessageItem> ConsoleMessages { get; } = new();

    private string Version { get; set; } = "fetching";

    private bool Show { get; set; }

    [NotNull]
    private static string NugetPackageName => "BootstrapBlazor.BaiduSpeech";

    private int TotalTime { get; set; } = 5000;

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("bootstrapblazor.baiduspeech");
    }

    private async Task OnStart()
    {
        TotalTime = 5000;
        await RecognizerService.RecognizeOnceAsync(Recognizer, TotalTime);
    }

    private Task Recognizer(RecognizerStatus status, string? result)
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

            ConfirmAction(result ?? "");
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private void ConfirmAction(string result) => Task.Run(async () =>
    {
        if (CheckReceivedData(result))
        {
            var text = "您确认要把灯打开吗？请确认";
            await SynthesizerService.SynthesizerOnceAsync(text, async status =>
            {
                if (status == SynthesizerStatus.Synthesizer)
                {
                    ConsoleMessages.Add(new ConsoleMessageItem()
                    {
                        Message = text,
                        Color = Color.Warning
                    });
                    StateHasChanged();
                }
                if (status == SynthesizerStatus.Finished)
                {
                    await RecognizerConfirm();
                }
            });
        }
    });

    private async Task RecognizerConfirm()
    {
        Show = true;
        TotalTime = 3000;
        StateHasChanged();
        await Task.Delay(300);
        await RecognizerService.RecognizeOnceAsync(Confirm, TotalTime);
    }

    private async Task Confirm(RecognizerStatus status, string? result)
    {
        if (status == RecognizerStatus.Start)
        {
            Show = true;
            StateHasChanged();
        }
        else
        {
            Show = false;
            StateHasChanged();
        }

        result ??= "";

        if (result.Contains("确认"))
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

            await SynthesizerService.SynthesizerOnceAsync("已经为您打开", status =>
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
    }

    private static bool CheckReceivedData(string result) => result.Contains('灯') && result.Contains("打开");
}
