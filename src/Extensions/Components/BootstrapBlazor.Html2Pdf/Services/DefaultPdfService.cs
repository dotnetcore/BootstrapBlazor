// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using PuppeteerSharp;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 Html to Pdf 实现 
/// </summary>
class DefaultPdfService(NavigationManager navigationManager) : IHtml2Pdf
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

        await AddWebsiteLinks(page, links);
        await AddWebsiteScripts(page, scripts);

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
        await page.SetExtraHttpHeadersAsync(new Dictionary<string, string>
        {
            ["Content-Type"] = "text/html; charset=utf-8"
        });
        await page.SetContentAsync(html);

        await AddWebsiteLinks(page, links);
        await AddWebsiteScripts(page, scripts);

        var content = await page.GetContentAsync();
        return await page.PdfStreamAsync();
    }

    private async Task AddWebsiteLinks(IPage page, IEnumerable<string>? links = null)
    {
        var baseUri = navigationManager.BaseUri;
        var websiteLinks = new List<string>()
        {
            $"{baseUri}_content/BootstrapBlazor.FontAwesome/css/font-awesome.min.css",
            $"{baseUri}_content/BootstrapBlazor.MaterialDesign/css/md.min.css",
            $"{baseUri}_content/BootstrapBlazor.BootstrapIcon/css/bootstrap-icons.min.css",
            $"{baseUri}_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css",
            $"{baseUri}_content/BootstrapBlazor/css/motronic.min.css"
        };

        if (links != null)
        {
            websiteLinks.AddRange(links);
        }

        foreach (var link in websiteLinks)
        {
            await page.AddStyleTagAsync(link);
        }
    }

    private static async Task AddWebsiteScripts(IPage page, IEnumerable<string>? scripts = null)
    {
        var websiteScripts = new List<string>();

        if (scripts != null)
        {
            websiteScripts.AddRange(scripts);
        }

        foreach (var script in websiteScripts)
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
