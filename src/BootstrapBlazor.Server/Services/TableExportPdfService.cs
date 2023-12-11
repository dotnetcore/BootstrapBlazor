// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Services;

class TableExportPdfService(IWebHostEnvironment webHostEnvironment, NavigationManager navigationManager, IHtml2Pdf html2Pdf) : ITableExportPdf
{
    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;
    private readonly NavigationManager _navigationManager = navigationManager;
    private readonly IHtml2Pdf _html2Pdf = html2Pdf;

    public async Task<byte[]> PdfDataAsync(string content)
    {
        var url = await GenerateHtmlAsync(content);

        // 生成 Pdf
        return await _html2Pdf.PdfDataAsync(url);
    }

    public async Task<Stream> PdfStreamAsync(string content)
    {
        var url = await GenerateHtmlAsync(content);

        // 生成 Pdf
        return await _html2Pdf.PdfStreamAsync(url);
    }

    private async Task<string> GenerateHtmlAsync(string content)
    {
        // 通过 template 模板文件生成网页文件
        var templateFileName = Path.Combine(_webHostEnvironment.WebRootPath, "pdf/template.htm");
        var template = await File.ReadAllTextAsync(templateFileName);

        // 生成静态 html 文件
        var htmlFileName = $"pdf/{Guid.NewGuid()}.html";
        var filePath = Path.Combine(_webHostEnvironment.WebRootPath, htmlFileName);
        using var writer = File.CreateText(filePath);
        await writer.WriteLineAsync(string.Format(template, content));
        await writer.FlushAsync();
        writer.Close();

        // 拼接导出文件网址
        return $"{_navigationManager.BaseUri}{htmlFileName}";
    }
}
