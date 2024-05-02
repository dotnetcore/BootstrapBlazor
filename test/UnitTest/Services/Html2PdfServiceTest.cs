// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

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
}
