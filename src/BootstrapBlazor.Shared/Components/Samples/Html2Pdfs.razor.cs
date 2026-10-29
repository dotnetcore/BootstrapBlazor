// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">Html2Pdf 示例</para>
/// <para lang="en">Html2Pdf examples.</para>
/// </summary>
public partial class Html2Pdfs
{
    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IHtml2Pdf? Html2PdfService { get; set; }

    [Inject]
    [NotNull]
    private DownloadService? DownloadService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Html2Pdfs>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    /// <para lang="zh">获得 IconTheme 实例</para>
    /// <para lang="en">Gets the IconTheme instance.</para>
    /// </summary>
    [Inject]
    [NotNull]
    protected IIconTheme? IconTheme { get; set; }

    [NotNull]
    private List<Foo>? Items { get; set; }

    private string? _exportIcon = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(FooLocalizer);

        _exportIcon = IconTheme.GetIconByKey(ComponentIcons.TableExportPdfIcon);
    }

    private async Task OnExportAsync()
    {
        // 通过脚本 getHtml 获得 table 表格 Html
        // Get the table HTML through the getHtml script.
        var module = await JSRuntime.LoadUtility();
        var html = await module.GetHtml("table-9527");
        if (!string.IsNullOrEmpty(html))
        {
            // 通过模板生成完整的 Html
            // Generate a complete HTML document from the template.
            var htmlString = $"""
                <!DOCTYPE html>

                <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
                <head>
                    <meta charset="utf-8" />
                </head>
                <body class="p-3">
                    {html}
                </body>
                </html>
                """;

            // 增加网页所需样式表文件
            // Include the styles required by the page.
            using var stream = await Html2PdfService.PdfStreamFromHtmlAsync(htmlString, [$"{NavigationManager.BaseUri}_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css"], options: new PdfOptions()
            {
                Format = PaperFormat.A4
            });

            // 下载 Pdf 文件
            // Download the PDF returned by the locally registered or HTTP-backed service.
            await DownloadService.DownloadFromStreamAsync($"table-{DateTime.Now:HHmmss}.pdf", stream);

            // 提示文件下载成功
            // Report completion after the download succeeds.
            await ToastService.Success(Localizer["ToastTitle"], Localizer["ToastContent"]);
        }
    }
}
