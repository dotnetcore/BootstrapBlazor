// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Speechs 示例
/// </summary>
public partial class Speechs
{
    [Inject]
    [NotNull]
    private VersionService? VersionManager { get; set; }

    [Inject]
    [NotNull]
    private RecognizerService? RecognizerService { get; set; }

    [Inject]
    [NotNull]
    private SynthesizerService? SynthesizerService { get; set; }

    private List<ConsoleMessageItem> ConsoleMessages { get; } = new();

    private string Version { get; set; } = "fetching";

    private bool Show { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("bootstrapblazor.azurespeech");
    }

    private void OnStart()
    {
        Show = true;
        _ = RecognizerService.RecognizeOnceAsync(Recognizer);
    }

    private Task Recognizer(string result)
    {
        Show = false;
        ConsoleMessages.Add(new ConsoleMessageItem()
        {
            Message = result,
            Color = Color.Success
        });

        ConfirmAction(result);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void ConfirmAction(string result) => Task.Run(async () =>
    {
        if (CheckReceivedData(result))
        {
            var text = "您确认要把灯打开吗？请确认";
            await SynthesizerService.SynthesizerOnceAsync(text, async status =>
            {
                ConsoleMessages.Add(new ConsoleMessageItem()
                {
                    Message = text,
                    Color = Color.Warning
                });
                RecognizerConfirm();
                await InvokeAsync(StateHasChanged);
            });
        }
    }).ConfigureAwait(false);

    private void RecognizerConfirm() => Task.Run(async () =>
    {
        Show = true;
        await InvokeAsync(StateHasChanged);

        await Task.Delay(1000);
        await RecognizerService.RecognizeOnceAsync(Confirm);
    }).ConfigureAwait(false);

    private async Task Confirm(string result)
    {
        Show = false;
        await InvokeAsync(StateHasChanged);

        if (result.Contains("确认"))
        {
            ConsoleMessages.Add(new ConsoleMessageItem()
            {
                Message = result,
                Color = Color.Success
            });
            ConsoleMessages.Add(new ConsoleMessageItem()
            {
                Message = "开灯",
                Color = Color.Danger
            });
            await InvokeAsync(StateHasChanged);
        }
    }

    private static bool CheckReceivedData(string result) => result.Contains("灯") && result.Contains("打开");
}
