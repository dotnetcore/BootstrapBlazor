// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// PdfReaders
/// </summary>
public partial class PdfReaders
{
    [Inject, NotNull]
    private IWebHostEnvironment? WebHostEnvironment { get; set; }

    [Inject, NotNull]
    private DownloadService? DownloadService { get; set; }

    private bool _showTwoPagesOneView = true;
    private bool _showPrint = true;
    private bool _showDownload = true;
    private string _url = "./samples/sample.pdf";

    private async Task OnDownloadAsync()
    {
        var file = Path.Combine(WebHostEnvironment.WebRootPath, "samples", "sample.pdf");
        await DownloadService.DownloadFromFileAsync($"sample_{DateTime.Now:yyyyMMddHHmmss}.pdf", file);
    }
}
