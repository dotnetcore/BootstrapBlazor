// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// PdfViewers
/// </summary>
public partial class OfficeViewers
{
    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private readonly List<SelectedItem> _docs =
    [
        new SelectedItem("https://www.blazor.zone/samples/sample.docx", "sample.docx"),
        new SelectedItem("https://www.blazor.zone/samples/sample.xlsx", "sample.xlsx"),
        new SelectedItem("https://www.blazor.zone/samples/sample.pptx", "sample.pptx"),
    ];

    private string _doc = "https://www.blazor.zone/samples/sample.docx";

    private Task OnLoaded() => ToastService.Success("Office Documentation Viewer", Localizer["OfficeViewerToastSuccessfulContent"]);
}
