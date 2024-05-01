// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using PuppeteerSharp;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 Html to Pdf 实现 
/// </summary>
class DefaultPdfService : IHtml2Pdf
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<byte[]> PdfDataAsync(string url)
    {
        await using var browser = await LaunchBrowserAsync();
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        return await page.PdfDataAsync();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<Stream> PdfStreamAsync(string url)
    {
        await using var browser = await LaunchBrowserAsync();
        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);

        return await page.PdfStreamAsync();
    }

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    public async Task<byte[]> PdfDataFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null)
    {
        await using var browser = await LaunchBrowserAsync();
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);

        await AddStyleTagAsync(page, links);
        await AddScriptTagAsync(page, scripts);

        return await page.PdfDataAsync();
    }

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">html raw string</param>
    /// <param name="links"></param>
    /// <param name="scripts"></param>
    public async Task<Stream> PdfStreamFromHtmlAsync(string html, IEnumerable<string>? links = null, IEnumerable<string>? scripts = null)
    {
        await using var browser = await LaunchBrowserAsync();
        await using var page = await browser.NewPageAsync();
        await page.SetContentAsync(html);

        await AddStyleTagAsync(page, links);
        await AddScriptTagAsync(page, scripts);

        return await page.PdfStreamAsync();
    }

    private static async Task AddStyleTagAsync(IPage page, IEnumerable<string>? links = null)
    {
        var styles = new List<string>();

        if (links != null)
        {
            styles.AddRange(links);
        }

        foreach (var link in styles)
        {
            await page.AddStyleTagAsync(link);
        }
    }

    private static async Task AddScriptTagAsync(IPage page, IEnumerable<string>? scripts = null)
    {
        var tags = new List<string>();

        if (scripts != null)
        {
            tags.AddRange(scripts);
        }

        foreach (var script in tags)
        {
            await page.AddScriptTagAsync(script);
        }
    }

    private static LaunchOptions CreateOptions() => new() { Headless = true, Args = ["--no-sandbox", "--disable-setuid-sandbox"] };

    private static async Task<IBrowser> LaunchBrowserAsync()
    {
        using var browserFetcher = new BrowserFetcher();
        await browserFetcher.DownloadAsync();

        return await Puppeteer.LaunchAsync(CreateOptions());
    }
}
