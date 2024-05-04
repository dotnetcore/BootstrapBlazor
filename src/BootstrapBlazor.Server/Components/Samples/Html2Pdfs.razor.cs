// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Html2Pdf 示例
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
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [Inject]
    [NotNull]
    private IJSRuntime? JSRuntime { get; set; }

    /// <summary>
    /// 获得 IconTheme 实例
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

        Items = Foo.GenerateFoo(LocalizerFoo);

        _exportIcon = IconTheme.GetIconByKey(ComponentIcons.TableExportPdfIcon);
    }

    private async Task OnExportAsync()
    {
        // 通过脚本 getHtml 获得 table 表格 Html
        var module = await JSRuntime.LoadUtility();
        var html = await module.GetHtml("table-9527");
        if (!string.IsNullOrEmpty(html))
        {
            // 通过模板生成完整的 Html
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
            using var stream = await Html2PdfService.PdfStreamFromHtmlAsync(htmlString, [$"{NavigationManager.BaseUri}_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css"]);

            // 下载 Pdf 文件
            await DownloadService.DownloadFromStreamAsync($"table-{DateTime.Now:HHmmss}.pdf", stream);

            // 提示文件下载成功
            await ToastService.Success(Localizer["ToastTitle"], Localizer["ToastContent"]);
        }
    }
}
