// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 Html to Pdf 实现 
/// </summary>
class DefaultPdfService : IHtml2Pdf
{
    public Task<byte[]> PdfDataAsync(string url) => throw new NotImplementedException();

    public Task<Stream> PdfStreamAsync(string url) => throw new NotImplementedException();

    public Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null) => GeneratePdfFromHtmlAsync(html, null);

    public async Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null)
    {
        var data = await GeneratePdfFromHtmlAsync(html, null);
        return new MemoryStream(data);
    }

    private static Task<byte[]> GeneratePdfFromHtmlAsync(string html, List<string>? fonts = null) => Task.Run(() =>
    {
        ConverterProperties? converterProperties = null;
        if (fonts != null)
        {
            var fontProvider = new DefaultFontProvider(registerStandardPdfFonts: false, registerShippedFonts: false, registerSystemFonts: false);
            foreach (var font in fonts)
            {
                var fontProgram = FontProgramFactory.CreateFont(font);
                fontProvider.AddFont(fontProgram);
            }

            converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);
        }

        var stream = new MemoryStream();
        HtmlConverter.ConvertToPdf(html, stream, converterProperties);
        return stream.ToArray();
    });
}
