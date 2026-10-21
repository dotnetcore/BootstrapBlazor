// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// PdfReaders
/// </summary>
public partial class PdfReaders
{
    [Inject, NotNull]
    private HttpClient? HttpClient { get; set; }

    [Inject, NotNull]
    private NavigationManager? NavigationManager { get; set; }

    private bool _showTwoPagesOneView = true;
    private bool _showPrint = true;
    private bool _enableThumbnails = true;
    private bool _showDownload = true;
    private bool _showToolbar = true;
    private bool _showFileName = true;
    private string _url = "./samples/sample.pdf";
    private string _streamFileName = "";

    private async Task<Stream> OnGetStreamAsync()
    {
        if (string.IsNullOrEmpty(_streamFileName))
        {
            return Stream.Null;
        }

        return await HttpClient.GetStreamAsync(NavigationManager.ToAbsoluteUri($"samples/{_streamFileName}"));
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
