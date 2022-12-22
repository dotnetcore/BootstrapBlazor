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
        DemoUrl = $"{SampleUrl}../Demos/";
    }

    /// <summary>
    /// 获得示例源码方法
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetCodeAsync(string codeFile, string? blockTitle, string? demo)
    {
        var content = "";
        try
        {
            var payload = await GetContentFromDemo(demo) ?? await GetContentFromFile(codeFile);

            if (blockTitle != null)
            {
                // 生成资源文件
                content = CacheManager.GetCode(codeFile, blockTitle, entry =>
                {
                    payload = Filter(payload);

                    entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                    return payload;
                });
            }
            else
            {
                content = payload;
            }
        }
        catch (Exception ex) { content = $"Error: {ex.Message}"; }
        return content;

        string Filter(string content)
        {
            var beginFlag = "<DemoBlock ";
            var endFlag = "</DemoBlock>";
            if (!string.IsNullOrEmpty(blockTitle))
            {
                var findStrings = new string[] { $"Name=\"{blockTitle}\"", $"Title=\"{blockTitle}\"" };
                var endLength = endFlag.Length;
                while (content.Length > 0)
                {
                    var star = content.IndexOf(beginFlag);
                    if (star == -1)
                    {
                        break;
                    }

                    var length = content.IndexOf(endFlag);
                    if (length == -1)
                    {
                        break;
                    }

                    var seg = content[star..(length + endLength)];
                    if (seg.IndexOf(findStrings[0]) > -1 || seg.IndexOf(findStrings[1]) > -1)
                    {
                        var lineFlag = "\n";
                        var seqStar = seg.IndexOf(lineFlag);
                        var end = seg.IndexOf(endFlag);
                        var data = seg[seqStar..end];
                        content = data.Replace("\n    ", "\n").TrimStart('\n');
                        break;
                    }
                    else
                    {
                        content = content[(length + endLength)..];
                    }
                }
            }
            TrimTips();
            return content;

            void TrimTips()
            {
                beginFlag = "<Tips>";
                endFlag = $"</Tips>{Environment.NewLine}";
                var endLength = endFlag.Length;
                var star = content.IndexOf(beginFlag);
                if (star > -1)
                {
                    var length = content.IndexOf(endFlag);
                    if (length > -1)
                    {
                        content = $"{content[..star]}{content[(length + endLength)..]}";
                    }
                }
            }
        }
    }

    private async Task<string?> GetContentFromDemo(string? demo) => string.IsNullOrEmpty(demo)
        ? null
        : await CacheManager.GetContentFromDemoAsync(demo, async entry =>
    {
        var payload = "";

        demo = demo.Replace('.', Path.DirectorySeparatorChar);
        demo = $"{demo}.razor";

        if (IsDevelopment)
        {
            payload = await ReadDemoTextAsync(demo);
        }
        else
        {
            var client = Factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            if (OperatingSystem.IsBrowser())
            {
                client.BaseAddress = new Uri($"{ServerUrl}/api/");
                payload = await client.GetStringAsync($"Code?fileName={demo}");
            }
            else
            {
                client.BaseAddress = new Uri(DemoUrl);
                payload = await client.GetStringAsync(demo.Replace('\\', '/'));
            }
        }

        // 将资源文件信息替换
        CacheManager.GetDemoLocalizedStrings(demo, LocalizerOptions).ToList().ForEach(l => payload = ReplacePayload(payload, l));
        payload = ReplaceSymbols(payload);
        return payload;
    });

    private Task<string> GetContentFromFile(string codeFile) => CacheManager.GetContentFromFileAsync(codeFile, async entry =>
    {
        var payload = "";

        if (IsDevelopment)
        {
            payload = await ReadFileTextAsync(codeFile);
        }
        else
        {
            var client = Factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);

            if (OperatingSystem.IsBrowser())
            {
                client.BaseAddress = new Uri($"{ServerUrl}/api/");
                payload = await client.GetStringAsync($"Code?fileName={codeFile}");
            }
            else
            {
                client.BaseAddress = new Uri(SampleUrl);
                payload = await client.GetStringAsync(codeFile);
            }
        }
        if (Path.GetExtension(codeFile) == ".razor")
        {
            // 将资源文件信息替换
            CacheManager.GetLocalizedStrings(codeFile, LocalizerOptions).ToList().ForEach(l => payload = ReplacePayload(payload, l));
            payload = ReplaceSymbols(payload);
        }
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

    private async Task<string> ReadFileTextAsync(string codeFile)
    {
        var payload = "";
        var paths = new string[] { "..", "BootstrapBlazor.Shared", "Samples" };
        var folder = Path.Combine(ContentRootPath, string.Join(Path.DirectorySeparatorChar, paths));
        var file = Path.Combine(folder, codeFile);
        if (File.Exists(file))
        {
            payload = await File.ReadAllTextAsync(file);
        }
        return payload;
    }

    private async Task<string> ReadDemoTextAsync(string codeFile)
    {
        var payload = "";
        var paths = new string[] { "..", "BootstrapBlazor.Shared", "Demos" };
        var folder = Path.Combine(ContentRootPath, string.Join(Path.DirectorySeparatorChar, paths));
        var file = Path.Combine(folder, codeFile);
        if (File.Exists(file))
        {
            payload = await File.ReadAllTextAsync(file);
        }
        return payload;
    }
}
