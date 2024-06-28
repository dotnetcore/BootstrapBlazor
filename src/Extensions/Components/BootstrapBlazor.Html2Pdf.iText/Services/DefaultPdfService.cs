// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.IO.Font;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 Html to Pdf 实现 
/// </summary>
class DefaultPdfService(IServiceProvider provider) : IHtml2Pdf
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

    public Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null)
    {
        return PdfStreamFromHtmlAsync(html, fonts: null);
    }

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="fonts"></param>
    public Task<Stream> PdfStreamFromHtmlAsync(string html, List<string>? fonts = null) => Task.Run(() =>
    {
        ConverterProperties? converterProperties = null;

        if (CultureInfo.CurrentUICulture.Name == "zh-CN" && fonts == null)
        {
            var webHost = provider.CreateScope().ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            fonts =
            [
                Path.Combine(webHost.WebRootPath, "_content/BootstrapBlazor.Html2PdfiText/simhei.ttf")
            ];
        }
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
        var data = stream.ToArray();

        Stream ret = new MemoryStream(data);
        return ret;
    });
}
