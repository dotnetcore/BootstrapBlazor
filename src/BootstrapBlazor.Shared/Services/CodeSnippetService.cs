// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared.Extensions;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Services;

class CodeSnippetService
{
    private IHttpClientFactory Factory { get; set; }

    private string ServerUrl { get; set; }

    private string SampleUrl { get; set; }

    private string DemoUrl { get; set; }

    private bool IsDevelopment { get; }

    private string ContentRootPath { get; }

    private ICacheManager CacheManager { get; set; }

    private JsonLocalizationOptions LocalizerOptions { get; }

    /// <summary>
    /// 构造方法
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cacheManager"></param>
    /// <param name="options"></param>
    /// <param name="localizerOptions"></param>
    public CodeSnippetService(
        IHttpClientFactory factory,
        ICacheManager cacheManager,
        IOptionsMonitor<WebsiteOptions> options,
        IOptionsMonitor<JsonLocalizationOptions> localizerOptions)
    {
        LocalizerOptions = localizerOptions.CurrentValue;

        CacheManager = cacheManager;
        Factory = factory;

        IsDevelopment = options.CurrentValue.IsDevelopment;
        ContentRootPath = options.CurrentValue.ContentRootPath;
        ServerUrl = options.CurrentValue.ServerUrl;
        SampleUrl = options.CurrentValue.SampleUrl;
        DemoUrl = $"{SampleUrl}../";
    }

    /// <summary>
    /// 获得示例源码方法
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetCodeAsync(string demo)
    {
        string? content;
        try
        {
            content = await GetContentFromDemo(demo);
        }
        catch (Exception ex) { content = $"Error: {ex.Message}"; }
        return content;
    }

    private Task<string> GetContentFromDemo(string demo) => CacheManager.GetContentFromDemoAsync(demo, async entry =>
    {
        var payload = "";

        var fileName = demo.Replace('.', Path.DirectorySeparatorChar);
        fileName = $"{fileName}.razor";

        if (IsDevelopment)
        {
            payload = await ReadDemoTextAsync(fileName);
        }
        else
        {
            var client = Factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            if (OperatingSystem.IsBrowser())
            {
                client.BaseAddress = new Uri($"{ServerUrl}/api/");
                payload = await client.GetStringAsync($"Code?fileName={fileName}");
            }
            else
            {
                client.BaseAddress = new Uri(DemoUrl);
                payload = await client.GetStringAsync(fileName.Replace('\\', '/'));
            }
        }

        // 将资源文件信息替换
        CacheManager.GetDemoLocalizedStrings(demo, LocalizerOptions).ToList().ForEach(l => payload = ReplacePayload(payload, l));
        payload = ReplaceSymbols(payload);
        payload = RemoveLocalizer(payload);
        return payload;
    });

    private static string ReplaceSymbols(string payload) => payload
        .Replace("@@", "@")
        .Replace("&lt;", "<")
        .Replace("&gt;", ">");

    private static string ReplacePayload(string payload, LocalizedString l) => payload
        .Replace($"@(((MarkupString)Localizer[\"{l.Name}\"].Value).ToString())", l.Value)
        .Replace($"@((MarkupString)Localizer[\"{l.Name}\"].Value)", l.Value)
        .Replace($"@Localizer[\"{l.Name}\"]", l.Value);

    private static string RemoveLocalizer(string payload)
    {
        var index = payload.IndexOf("@inject IStringLocalizer<");
        if (index > -1)
        {
            var end = payload.IndexOf("\r\n", index, StringComparison.OrdinalIgnoreCase);
            var target = payload[index..(end + 2)];
            payload = payload.Replace(target, string.Empty);
            payload = payload.TrimStart('\r', '\n');
        }
        return payload;
    }

    private async Task<string> ReadDemoTextAsync(string codeFile)
    {
        var payload = "";
        var paths = new string[] { "..", "BootstrapBlazor.Shared" };
        var folder = Path.Combine(ContentRootPath, string.Join(Path.DirectorySeparatorChar, paths));
        var file = Path.Combine(folder, codeFile);
        if (File.Exists(file))
        {
            payload = await File.ReadAllTextAsync(file);
        }
        return payload;
    }
}
