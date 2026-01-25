// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 剪切板示例
/// </summary>
public partial class Clipboards
{
    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private string content { get; set; } = "Hello BootstrapBlazor";

    private async Task Copy()
    {
        await ClipboardService.Copy(content);
        await ToastService.Success("Clipboard", Localizer["ClipboardMessage", content]);
    }

    private async Task Get()
    {
        var res = await ClipboardService.Get();
        if (res is not null)
        {
            var first = res.FirstOrDefault();
            if (first is not null)
            {
                content = first.Text;
                await ToastService.Success("Clipboard", Localizer["ClipboardGetTextMessage", content]);
            }
        }
    }
}
