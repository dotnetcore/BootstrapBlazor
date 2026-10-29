// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">Dialog PDF 导出示例。</para>
/// <para lang="en">Dialog PDF export sample.</para>
/// </summary>
public partial class DialogPdf
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DialogPdf>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private DialogService? DialogService { get; set; }

    private Task OnExportPdfDialogClick() => DialogService.Show(new DialogOption
    {
        Title = "Export Pdf Demo",
        ShowExportPdfButton = true,
        ShowExportPdfButtonInHeader = true,
        ShowFooter = false,
        BodyContext = 1,
        Component = BootstrapDynamicComponent.CreateComponent<DataDialogComponent>()
    });
}
