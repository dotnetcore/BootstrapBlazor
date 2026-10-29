// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// <para lang="zh">为 WebAssembly 示例生成受限 HTML 的 PDF。</para>
/// <para lang="en">Generates PDFs from restricted HTML for the WebAssembly sample.</para>
/// </summary>
[ApiController]
public sealed class Html2PdfController(IHtml2Pdf pdfService, IWebHostEnvironment environment) : ControllerBase
{
    private static readonly SemaphoreSlim _generationLock = new(1, 1);
    private static readonly HashSet<string> _styles = new(StringComparer.Ordinal)
    {
        "_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css",
        "_content/BootstrapBlazor.FontAwesome/css/font-awesome.min.css"
    };

    /// <summary>
    /// <para lang="zh">生成 PDF 二进制响应，不保存临时下载文件。</para>
    /// <para lang="en">Returns generated PDF bytes without saving a temporary download file.</para>
    /// </summary>
    [HttpPost("api/html2pdf")]
    [Consumes("application/json")]
    [RequestSizeLimit(1024 * 1024)]
    public async Task<IActionResult> Generate(Html2PdfRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Html) || request.Links == null || request.Links.Count > _styles.Count)
        {
            return Problem(detail: "HTML and supported site styles are required.", statusCode: 400);
        }
        if (request.Options is { } options &&
            (options.Scale < 0.1m || options.Scale > 2 || options.Format == null ||
             options.Format.Width <= 0 || options.Format.Width > 48 ||
             options.Format.Height <= 0 || options.Format.Height > 48 || options.MarginOptions == null))
        {
            return Problem(detail: "Invalid PDF page options.", statusCode: 400);
        }

        var styles = new StringBuilder();
        foreach (var link in request.Links)
        {
            // 只读取固定白名单中的本地样式，绝不从客户端提供的 URL 下载资源。
            // Read only allowlisted local styles; never fetch resources from client-supplied URLs.
            if (!Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out var uri))
            {
                return Problem(detail: "Unsupported stylesheet.", statusCode: 400);
            }
            var path = uri.IsAbsoluteUri ? uri.AbsolutePath.TrimStart('/') : link.TrimStart('/');
            if (!_styles.Contains(path))
            {
                return Problem(detail: "Unsupported stylesheet.", statusCode: 400);
            }
            var file = environment.WebRootFileProvider.GetFileInfo(path);
            if (!file.Exists)
            {
                throw new FileNotFoundException("The PDF stylesheet is unavailable.", path);
            }
            await using var source = file.CreateReadStream();
            using var reader = new StreamReader(source);
            styles.AppendLine(await reader.ReadToEndAsync(cancellationToken));
        }

        if (!await _generationLock.WaitAsync(0, cancellationToken))
        {
            return Problem(detail: "PDF generation is busy. Please retry.", statusCode: 429);
        }
        try
        {
            // 暂不清理 HTML，保留 CSP 对脚本及资源加载的限制；此接口不适合作为通用的不可信 HTML 转换服务。
            // HTML sanitization is deferred; retain CSP restrictions. This is not a general-purpose untrusted HTML conversion service.
            var html = $"""
                <!DOCTYPE html><html><head><meta charset="utf-8">
                <meta http-equiv="Content-Security-Policy" content="default-src 'none'; style-src 'unsafe-inline'; base-uri 'none'; form-action 'none'">
                <style>{styles}</style></head><body>{request.Html}</body></html>
                """;
            var data = await pdfService.PdfDataFromHtmlAsync(html, options: request.Options);
            cancellationToken.ThrowIfCancellationRequested();
            Response.Headers.CacheControl = "no-store";
            return File(data, "application/pdf");
        }
        finally
        {
            _generationLock.Release();
        }
    }
}
