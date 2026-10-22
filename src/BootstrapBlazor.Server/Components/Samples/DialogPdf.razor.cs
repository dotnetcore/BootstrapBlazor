// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Dialog PDF export sample.
/// </summary>
public partial class DialogPdf
{
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
