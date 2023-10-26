// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;
public partial class Clipboards
{
    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; set; }

    private string content { get; set; } = "Hello BootstrapBlazor";

    private async Task Copy()
    {
        await ClipboardService.Copy(content);

        await MessageService.Show(new MessageOption()
        {
            Content = $"{Localizer["ClipboardMessage1"].Value}{content}{Localizer["ClipboardMessage2"].Value}"
        });
    }

    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = "Copy",
            Description = Localizer["ClipboardIntro"].Value,
            Parameters = " - ",
            ReturnValue = "ValueTask"
        }
    };
}
