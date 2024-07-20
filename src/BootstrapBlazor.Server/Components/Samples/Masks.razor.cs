// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Mask 组件示例文档
/// </summary>
public partial class Masks
{
    [Inject, NotNull]
    private MaskService? MaskService { get; set; }

    [NotNull]
    private Mask? CustomMask1 { get; set; }

    [NotNull]
    private Mask? CustomMask2 { get; set; }

    private async Task ShowMask()
    {
        await MaskService.Show(new MaskOption()
        {
            ChildContent = builder => builder.AddContent(0, new MarkupString("<i class=\"text-white fa-solid fa-3x fa-spinner fa-spin-pulse\"></i><span class=\"ms-3 fs-2 text-white\">loading ....</span>"))
        });
        await Task.Delay(3000);
        await MaskService.Close();
    }

    private async Task ShowDivMask()
    {
        await MaskService.Show(new MaskOption()
        {
            ChildContent = builder => builder.AddContent(0, new MarkupString("<i class=\"text-white fa-solid fa-3x fa-spinner fa-spin-pulse\"></i><span class=\"ms-3 fs-2 text-white\">loading ....</span>")),
            ContainerId = "div-mask-9527"
        });
        await Task.Delay(3000);
        await MaskService.Close();
    }

    private async Task ShowMultipleMask()
    {
        await MaskService.Show(new MaskOption()
        {
            ChildContent = builder => builder.AddContent(0, new MarkupString("<i class=\"text-white fa-solid fa-3x fa-spinner fa-spin-pulse\"></i><span class=\"ms-3 fs-2 text-white\">loading ....</span>")),
            ContainerId = "div-mask-9528"
        }, CustomMask1);
        await MaskService.Show(new MaskOption()
        {
            ChildContent = builder => builder.AddContent(0, new MarkupString("<i class=\"text-white fa-solid fa-3x fa-spinner fa-spin-pulse\"></i><span class=\"ms-3 fs-2 text-white\">loading ....</span>")),
            ContainerId = "div-mask-9529"
        }, CustomMask2);
        await Task.Delay(3000);
        await MaskService.Close(CustomMask1);
        await MaskService.Close(CustomMask2);
    }
}
