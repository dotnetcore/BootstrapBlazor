// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;
using System.Globalization;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// 构造方法
/// </summary>
/// <param name="factory"></param>
/// <param name="cacheManager"></param>
/// <param name="options"></param>
/// <param name="configuration"></param>
/// <param name="localizerOptions"></param>
class CodeSnippetService(
    IHttpClientFactory factory,
    ICacheManager cacheManager,
    IOptions<WebsiteOptions> options,
    IConfiguration configuration,
    IOptions<JsonLocalizationOptions> localizerOptions)
{
    /// <summary>
    /// 获得示例源码方法
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetCodeAsync(string codeFile)
    {
        string? content;
        try
        {
            // codeFile = ajax.razor.cs
            var segs = codeFile.Split('.');
            var key = segs[0];
            var typeName = options.Value.SourceCodes.TryGetValue(key.ToLowerInvariant(), out var value) ? value : string.Empty;
            if (!string.IsNullOrEmpty(typeName))
            {
                var fileName = codeFile.Replace(key, typeName);
                content = await GetFileContentAsync(fileName);

                // 源码修正
                var type = typeName.Replace('\\', '.');
                Utility.GetJsonStringByTypeName(localizerOptions.Value, typeof(CodeSnippetService).Assembly, $"BootstrapBlazor.Server.Components.Samples.{type}").ToList()
                    .ForEach(l => content = ReplacePayload(content, l));
                content = ReplaceSymbols(content);
                content = RemoveBlockStatement(content, "@inject IStringLocalizer<");
            }
            else
            {
                content = "Error: Please config docs.json";
            }
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
            var client = factory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(5);
            client.BaseAddress = new Uri($"{options.Value.ServerUrl}/api/");
            payload = await client.GetStringAsync($"Code?fileName=BootstrapBlazor.Server/Components/Samples/{fileName}");
        }
        else
        {
            // 读取硬盘文件
            var key = $"{nameof(GetFileContentAsync)}-{fileName}-{CultureInfo.CurrentUICulture.Name}";
            payload = await cacheManager.GetOrCreateAsync(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromMinutes(10);
                return ReadFileAsync(fileName);
            });
        }
        return payload;
    }

    private async Task<string> ReadFileAsync(string fileName)
    {
        string? payload;
        var file = options.Value.IsDevelopment
            ? $"{options.Value.ContentRootPath}\\..\\BootstrapBlazor.Server\\Components\\Samples\\{fileName}"
            : $"{GetSourceCodePath()}BootstrapBlazor.Server\\Components\\Samples\\{fileName}";
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
            payload = "Error: File not found";
        }
        return payload;
    }

    private string GetSourceCodePath() => $"{configuration.GetValue<string>("SourceCodePath") ?? options.Value.SourceCodePath}";

    private static string ReplaceSymbols(string payload) => payload
        .Replace("@@", "@")
        .Replace("&lt;", "<")
        .Replace("&gt;", ">");

    private static string ReplacePayload(string payload, LocalizedString l) => payload
        .Replace($"@((MarkupString)Localizer[\"{l.Name}\"].Value)", l.Value)
        .Replace($"@Localizer[\"{l.Name}\"]", l.Value)
        .Replace($"Localizer[\"{l.Name}\"]", $"\"{l.Value}\"");

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
