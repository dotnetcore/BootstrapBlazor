// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// PdfReaders
/// </summary>
public partial class EmbedPdfs
{
    [Inject, NotNull]
    private IWebHostEnvironment? WebHostEnvironment { get; set; }

    [Inject, NotNull]
    private DownloadService? DownloadService { get; set; }

    private EmbedPDFTabBarMode _tabBarMode = EmbedPDFTabBarMode.Always;
    private EmbedPDFTheme _theme = EmbedPDFTheme.System;
    private EmbedPDFScrollStrategy _strategy = EmbedPDFScrollStrategy.Vertical;
    private string _url = "./samples/sample.pdf";
    private string _streamFileName = "";
    private string _language = "";

    private List<SelectedItem> _languages = new List<SelectedItem>() {
        new SelectedItem("", "Auto"),
        new SelectedItem("en", "en-US"),
        new SelectedItem("zh-CN", "zh-CN")
    };

    private async Task<Stream> OnGetStreamAsync()
    {
        await Task.Yield();
        if (string.IsNullOrEmpty(_streamFileName))
        {
            return Stream.Null;
        }

        var stream = File.OpenRead(Path.Combine(WebHostEnvironment.WebRootPath, "samples", _streamFileName));
        return stream;
    }

    private void GetTestStream()
    {
        _url = "";
        _streamFileName = "ebook.pdf";
    }

    private void GetSampleStream()
    {
        _url = "";
        _streamFileName = "sample.pdf";
    }
}
