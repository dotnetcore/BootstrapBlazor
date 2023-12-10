// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Html2Pdf 示例
/// </summary>
public partial class Html2Pdfs
{
    [Inject]
    [NotNull]
    private IHtml2Pdf? PdfService { get; set; }

    [Inject]
    [NotNull]
    private DownloadService? DownloadService { get; set; }

    [Inject]
    [NotNull]
    private IComponentHtmlRenderer? HtmlRenderService { get; set; }

    [Inject]
    [NotNull]
    private IWebHostEnvironment? WebHostEnvironment { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private async Task OnExportAsync()
    {
        // 通过脚本获得 table 表格 Html
        var html = await InvokeAsync<string>("getHtml", "table-9527");

        // 通过 template 模板文件生成网页文件
        var templateFileName = Path.Combine(WebHostEnvironment.WebRootPath, "pdf/template.htm");
        var template = await File.ReadAllTextAsync(templateFileName);

        // 生成静态 html 文件
        var fileName = $"pdf/{Guid.NewGuid()}.html";
        var filePath = Path.Combine(WebHostEnvironment.WebRootPath, fileName);
        using var writer = File.CreateText(filePath);
        await writer.WriteLineAsync(string.Format(template, html));
        await writer.FlushAsync();
        writer.Close();

        // 拼接导出文件网址
        var url = $"{NavigationManager.BaseUri}{fileName}";
        var data = await PdfService.ExportDataAsync(url);
        using var stream = new MemoryStream(data);
        await DownloadService.DownloadFromStreamAsync("table.pdf", stream);
        await ToastService.Success("Pdf Export", "Export pdf element success.");
    }
}
