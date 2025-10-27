// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class Html2PdfServiceTest
{
    [Fact]
    public async Task ExportPdf_Error()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBootstrapBlazor();

        var provider = serviceCollection.BuildServiceProvider();
        var pdfService = provider.GetRequiredService<IHtml2Pdf>();

        await Assert.ThrowsAsync<NotImplementedException>(() => pdfService.PdfDataAsync("https://www.baidu.com"));
        await Assert.ThrowsAsync<NotImplementedException>(() => pdfService.PdfStreamAsync("https://www.baidu.com"));
        await Assert.ThrowsAsync<NotImplementedException>(() => pdfService.PdfDataFromHtmlAsync("<h2>Test</h2>"));
        await Assert.ThrowsAsync<NotImplementedException>(() => pdfService.PdfStreamFromHtmlAsync("<h2>Test</h2>"));
    }

    [Fact]
    public void PdfOptions_Ok()
    {
        var options = new PdfOptions
        {
            Landscape = true
        };
        Assert.True(options.Landscape);
    }
}
