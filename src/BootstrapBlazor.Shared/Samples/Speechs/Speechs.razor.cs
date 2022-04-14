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
    private IEnumerable<IRecognizerProvider>? RecognizerProviders { get; set; }

    [NotNull]
    private IRecognizerProvider? RecognizerProvider { get; set; }

    [Inject]
    [NotNull]
    private IEnumerable<ISynthesizerProvider>? SynthesizerProviders { get; set; }

    [NotNull]
    private ISynthesizerProvider? SynthesizerProvider { get; set; }

    private List<ConsoleMessageItem> ConsoleMessages { get; } = new();

    private string Version { get; set; } = "fetching";

    private bool Show { get; set; }

    [NotNull]
    private string? NugetPackageName { get; set; }

    [NotNull]
    private Func<string, Func<SynthesizerStatus, Task>, Task>? SynthesizerInvokeAsync { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        InitProvider();
    }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("bootstrapblazor.azurespeech");
    }

    private void InitProvider()
    {
        if (SpeechItem == "Azure")
        {
            NugetPackageName = "BootstrapBlazor.AzureSpeech";
            RecognizerProvider = RecognizerProviders.OfType<AzureRecognizerProvider>().FirstOrDefault();
            SynthesizerProvider = SynthesizerProviders.OfType<AzureSynthesizerProvider>().FirstOrDefault();
        }
        else
        {
            NugetPackageName = "BootstrapBlazor.BaiduSpeech";
            RecognizerProvider = RecognizerProviders.OfType<BaiduRecognizerProvider>().FirstOrDefault();
            SynthesizerProvider = SynthesizerProviders.OfType<BaiduSynthesizerProvider>().FirstOrDefault();
        }
    }

    private Task OnSpeechProviderChanged(string value)
    {
        SpeechItem = value;
        InitProvider();
        StateHasChanged();
        return Task.CompletedTask;
    }

    private async Task OnStart()
    {
        // 示例代码，由于注入两种语音服务 Baidu 语音晚于 Azure 内部服务注册 故默认获取的 语音提供服务均为 BaiduProvider 需要 手动构建
        // 实际生产中使用 Baidu 语音示例代码即可

        if (SpeechItem == "Azure")
        {
            Show = true;
        }

        if (SpeechItem == "Azure")
        {
            await RecognizerProvider.AzureRecognizeOnceAsync(Recognizer);
        }
        else
        {
            await RecognizerProvider.BaiduRecognizeOnceAsync(Recognizer);
        }
    }

    private Task Recognizer(string result)
    {
        if (SpeechItem == "Baidu")
        {
            if (result == RecognizerStatus.Start.ToString())
            {
                Show = true;
                StateHasChanged();
                return Task.CompletedTask;
            }
        }
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
            if (SpeechItem == "Azure")
            {
                SynthesizerInvokeAsync = SynthesizerProvider.AzureSynthesizerOnceAsync;
            }
            else
            {
                SynthesizerInvokeAsync = SynthesizerProvider.BaiduSynthesizerOnceAsync;
            }
            await SynthesizerInvokeAsync(text, async status =>
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
                    RecognizerConfirm();
                }
            });
        }
    }).ConfigureAwait(false);

    private void RecognizerConfirm() => Task.Run(async () =>
    {
        Show = true;
        await InvokeAsync(StateHasChanged);

        if (SpeechItem == "Azure")
        {
            await RecognizerProvider.AzureRecognizeOnceAsync(Confirm);
        }
        else
        {
            await RecognizerProvider.BaiduRecognizeOnceAsync(Confirm);
        }
    }).ConfigureAwait(false);

    private async Task Confirm(string result)
    {
        if (result == RecognizerStatus.Start.ToString())
        {
            Show = true;
            StateHasChanged();
        }
        else
        {
            Show = false;
            await InvokeAsync(StateHasChanged);
        }

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
            await InvokeAsync(StateHasChanged);
            if (SpeechItem == "Azure")
            {
                //模拟后台执行任务
                await Task.Delay(2000);
                SynthesizerInvokeAsync = SynthesizerProvider.AzureSynthesizerOnceAsync;
            }
            else
            {
                //模拟后台执行任务
                await Task.Delay(2000);
                SynthesizerInvokeAsync = SynthesizerProvider.BaiduSynthesizerOnceAsync;
            }

            await SynthesizerInvokeAsync("已经为您打开", async status =>
            {
                if (status == SynthesizerStatus.Synthesizer)
                {
                    ConsoleMessages.Add(new ConsoleMessageItem()
                    {
                        Message = "已经为您打开",
                        Color = Color.Danger
                    });
                    await InvokeAsync(StateHasChanged);
                }
            });
        }
    }

    private static bool CheckReceivedData(string result) => result.Contains('灯') && result.Contains("打开");
}
