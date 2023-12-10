// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using PuppeteerSharp;

namespace BootstrapBlazor.Components;

/// <summary>
/// 构造函数
/// </summary>
class DefaultPdfService : IHtml2Pdf
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<byte[]> ExportAsync(string url, PdfOptions? options = null)
    {
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);
        var content = await page.GetContentAsync();
        return await page.PdfDataAsync(options);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<Stream> ExportStreamAsync(string url, PdfOptions? options = null)
    {
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions() { Headless = true });
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);
        var content = await page.GetContentAsync();
        return await page.PdfStreamAsync(options);
    }
}
