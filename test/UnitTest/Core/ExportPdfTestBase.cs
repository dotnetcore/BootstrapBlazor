// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text;

namespace UnitTest.Core;

public class ExportPdfTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor();
        services.AddSingleton<IHtml2Pdf, MockHtml2PdfService>();
    }
}

class MockHtml2PdfService : IHtml2Pdf
{
    public Task<byte[]> PdfDataAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> PdfStreamAsync(string url)
    {
        throw new NotImplementedException();
    }

    public Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null) => Task.FromResult<Stream>(new MemoryStream(Encoding.UTF8.GetBytes("Hello World")));
}
