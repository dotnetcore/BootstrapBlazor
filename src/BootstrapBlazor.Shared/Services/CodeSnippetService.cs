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
        SampleUrl = $"{options.CurrentValue.SourceUrl}BootstrapBlazor.Shared\\Samples\\";
    }

    /// <summary>
    /// 获得示例源码方法
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetCodeAsync(string codeFile)
    {
        string? content;
        try
        {
            content = await GetFileContentAsync(codeFile);
        }
        catch (Exception ex) { content = $"Error: {ex.Message}"; }
        return content;
    }

    /// <summary>
    /// 获得指定文件源码
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<string> GetFileContentAsync(string fileName)
    {
        string? payload;
        if (OperatingSystem.IsBrowser())
        {
            var client = Factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            client.BaseAddress = new Uri($"{ServerUrl}/api/");
            payload = await client.GetStringAsync($"Code?fileName=BootstrapBlazor.Shared/Samples/{fileName}");
        }
        else
        {
            // 读取硬盘文件
            payload = await ReadFileAsync(fileName);
        }

        // 源码修正
        var typeName = fileName.Split('.').FirstOrDefault()?.Replace('\\', '/').Replace('/', '.');
        if (!string.IsNullOrEmpty(typeName))
        {
            CacheManager.GetLocalizedStrings(typeName, LocalizerOptions).ToList().ForEach(l => payload = ReplacePayload(payload, l));
            payload = ReplaceSymbols(payload);
            payload = RemoveBlockStatement(payload, "@inject IStringLocalizer<");
        }
        return payload;
    }

    private async Task<string> ReadFileAsync(string fileName)
    {
        string? payload;
        var file = IsDevelopment
            ? $"{ContentRootPath}\\..\\BootstrapBlazor.Shared\\Samples\\{fileName}"
            : $"{SampleUrl}{fileName}";
        if (!OperatingSystem.IsWindows())
        {
            file = file.Replace('\\', '/');
        }
        if (File.Exists(file))
        {
            payload = await File.ReadAllTextAsync(file);
        }
        else
        {
            payload = "File not found.";
        }
        return payload;
    }

    private static string ReplaceSymbols(string payload) => payload
        .Replace("@@", "@")
        .Replace("&lt;", "<")
        .Replace("&gt;", ">");

    private static string ReplacePayload(string payload, LocalizedString l) => payload
        .Replace($"@(((MarkupString)Localizer[\"{l.Name}\"].Value).ToString())", l.Value)
        .Replace($"@((MarkupString)Localizer[\"{l.Name}\"].Value)", l.Value)
        .Replace($"@Localizer[\"{l.Name}\"]", l.Value);

    private static string RemoveBlockStatement(string payload, string removeString)
    {
        var index = payload.IndexOf(removeString);
        if (index > -1)
        {
            var end = payload.IndexOf("\n", index, StringComparison.OrdinalIgnoreCase);
            var target = payload[index..(end + 1)];
            payload = payload.Replace(target, string.Empty);
            payload = payload.TrimStart('\r', '\n');
        }
        return payload;
    }
}
