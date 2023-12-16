// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Html2Pdf 示例
/// </summary>
public partial class Html2Pdfs
{
    [Inject]
    [NotNull]
    private IHtml2Pdf? Html2PdfService { get; set; }

    [Inject]
    [NotNull]
    private DownloadService? DownloadService { get; set; }

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
        if (OperatingSystem.IsWindows())
        {
            // 通过脚本获得 table 表格 Html
            var html = await InvokeAsync<string>("getHtml", "table-9527");

            // 通过 template 模板文件生成网页文件
            var templateFileName = Path.Combine(WebHostEnvironment.WebRootPath, "pdf/template.htm");
            var template = await File.ReadAllTextAsync(templateFileName);

            // 生成静态 html 文件
            var fileName = $"pdf/{Guid.NewGuid()}.html";
            var filePath = Path.Combine(WebHostEnvironment.WebRootPath, fileName);
            await using var writer = File.CreateText(filePath);
            await writer.WriteLineAsync(string.Format(template, html));
            await writer.FlushAsync();
            writer.Close();

            // 拼接导出文件网址
            var url = $"{NavigationManager.BaseUri}{fileName}";
            var data = await Html2PdfService.PdfDataAsync(url);
            using var stream = new MemoryStream(data);
            await DownloadService.DownloadFromStreamAsync("table.pdf", stream);
            await ToastService.Success("Pdf Export", "Export pdf element success.");
        }
        else
        {
            await ToastService.Information("Pdf Export", "请本地运行此功能，服务器为 Linux 系统未配置其运行环境Please use localhost check this function. ");
        }
    }
}
