﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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

    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// <inheritdoc/>
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
        if (!string.IsNullOrEmpty(html))
        {
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

            using var stream = await Html2PdfService.PdfStreamFromHtmlAsync(htmlString, [$"{NavigationManager.BaseUri}_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css"]);
            await DownloadService.DownloadFromStreamAsync($"table-{DateTime.Now:HHmmss}.pdf", stream);
            await ToastService.Success(Localizer["ToastTitle"], Localizer["ToastContent"]);
        }
    }
}
