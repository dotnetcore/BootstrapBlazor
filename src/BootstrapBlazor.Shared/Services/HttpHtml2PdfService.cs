// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">WebAssembly PDF 服务，通过后台生成 PDF，下载仍由调用方处理。</para>
/// <para lang="en">WebAssembly PDF service that generates PDFs on the server and leaves downloading to the caller.</para>
/// </summary>
public sealed class HttpHtml2PdfService(HttpClient client) : IHtml2Pdf
{
    /// <inheritdoc />
    public Task<byte[]> PdfDataAsync(string url, PdfOptions? options = null) =>
        throw new NotSupportedException("The demo PDF endpoint accepts HTML, not arbitrary URLs.");

    /// <inheritdoc />
    public Task<Stream> PdfStreamAsync(string url, PdfOptions? options = null) =>
        throw new NotSupportedException("The demo PDF endpoint accepts HTML, not arbitrary URLs.");

    /// <inheritdoc />
    public async Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null)
    {
        if (scripts?.Any() == true)
        {
            throw new NotSupportedException("The demo PDF endpoint does not execute scripts.");
        }

        using var response = await client.PostAsJsonAsync("api/html2pdf", new Html2PdfRequest
        {
            Html = html,
            // TableExport 将样式链接嵌入 HTML，未单独传 links；由后台内联对应内置样式。
            // TableExport embeds stylesheet links in HTML instead of passing links; inline the built-in styles on the server.
            Links = links?.ToList() ??
            [
                "_content/BootstrapBlazor.FontAwesome/css/font-awesome.min.css",
                "_content/BootstrapBlazor.MaterialDesign/css/md.min.css",
                "_content/BootstrapBlazor.BootstrapIcon/css/bootstrap-icons.min.css",
                "_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css",
                "_content/BootstrapBlazor/css/motronic.min.css"
            ],
            Options = options
        });
        response.EnsureSuccessStatusCode();
        if (response.Content.Headers.ContentType?.MediaType != "application/pdf")
        {
            throw new InvalidDataException("The server did not return a PDF.");
        }
        return await response.Content.ReadAsByteArrayAsync();
    }

    /// <inheritdoc />
    public async Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null, PdfOptions? options = null) =>
        new MemoryStream(await PdfDataFromHtmlAsync(html, links, scripts, options), writable: false);
}
