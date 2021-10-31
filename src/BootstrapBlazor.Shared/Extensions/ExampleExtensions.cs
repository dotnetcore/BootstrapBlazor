// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 示例代码获取服务
    /// </summary>
    public static class ExampleExtensions
    {
        /// <summary>
        /// 注入版本获取服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddExampleService(this IServiceCollection services)
        {
            services.AddScoped<ExampleService>();
            return services;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class ExampleService
    {
        private HttpClient Client { get; set; }

        private string ServerUrl { get; set; }

        private IWebHostEnvironment WebHost { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        private IMemoryCache MemoryCache { get; set; }

        private IEnumerable<IConfigurationSection> Sections { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        /// <param name="memoryCache"></param>
        /// <param name="host"></param>
        /// <param name="option"></param>
        public ExampleService(HttpClient client, IOptions<WebsiteOptions> options, IMemoryCache memoryCache, IWebHostEnvironment host, IOptions<JsonLocalizationOptions> option)
        {
            Client = client;
            Client.Timeout = TimeSpan.FromSeconds(5);
            Client.BaseAddress = new Uri(options.Value.RepositoryUrl);

            MemoryCache = memoryCache;
            WebHost = host;
            ServerUrl = options.Value.ServerUrl;

            Sections = JsonHelper.GetConfigurationSections(option.Value);
        }

        /// <summary>
        /// 获得组件版本号方法
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetCodeAsync(string codeFile, string? blockTitle)
        {
            var content = "";
            try
            {
                var payload = await GetContentFromFile(codeFile);

                if (blockTitle != null)
                {
                    // 生成资源文件
                    content = MemoryCache.GetOrCreate($"Snippet-{CultureInfo.CurrentUICulture.Name}-{codeFile}-{blockTitle}", entry =>
                    {
                        payload = Filter(codeFile, payload, blockTitle);

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
        }

        private Task<string> GetContentFromFile(string codeFile) => MemoryCache.GetOrCreateAsync($"{CultureInfo.CurrentUICulture.Name}-{codeFile}", async item =>
        {
            var payload = "";
            if (WebHost.IsDevelopment())
            {
                payload = await ReadFileTextAsync(codeFile);
            }
            else
            {
                if (OperatingSystem.IsBrowser())
                {
                    Client.BaseAddress = new Uri($"{ServerUrl}/api/");
                    payload = await Client.GetStringAsync($"Code?fileName={codeFile}");
                }
                else
                {
                    payload = await Client.GetStringAsync(codeFile);
                }
            }

            if (Path.GetExtension(codeFile) == ".razor")
            {
                // 将资源文件信息替换
                GetLocalizers().ForEach(kv =>
                {
                    payload = payload.Replace($"@(((MarkupString)Localizer[\"{kv.Key}\"].Value).ToString())", kv.Value);
                    payload = payload.Replace($"@((MarkupString)Localizer[\"{kv.Key}\"].Value)", kv.Value);
                    payload = payload.Replace($"@Localizer[\"{kv.Key}\"]", kv.Value);
                });
                payload = payload.Replace("@@", "@");
                payload = payload.Replace("&lt;", "<");
                payload = payload.Replace("&gt;", ">");
            }
            return payload;

            List<KeyValuePair<string, string>> GetLocalizers() => MemoryCache.GetOrCreate($"Localizer-{CultureInfo.CurrentUICulture.Name}-{codeFile}", entry =>
            {
                var typeName = Path.GetFileNameWithoutExtension(codeFile);
                return Sections.FirstOrDefault(s => $"BootstrapBlazor.Shared.Pages.{typeName}".Equals(s.Key, StringComparison.OrdinalIgnoreCase))?.GetChildren().SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) }).ToList() ?? new List<KeyValuePair<string, string>>();
            });
        });

        private async Task<string> ReadFileTextAsync(string codeFile)
        {
            var payload = "";
            var paths = new string[] { "..", "BootstrapBlazor.Shared", "Pages", "Samples" };
            var folder = Path.Combine(WebHost.ContentRootPath, string.Join(Path.DirectorySeparatorChar, paths));
            var file = Path.Combine(folder, codeFile);
            if (File.Exists(file))
            {
                payload = await File.ReadAllTextAsync(file);
            }
            return payload;
        }

        private string Filter(string codeFile, string content, string? blockTitle)
        {
            var beginFlag = "<Block ";
            var endFlag = "</Block>";
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
                        content = TrimBlock(codeFile, seg);
                        break;
                    }
                    else
                    {
                        content = content[(length + endLength)..];
                    }
                }
            }
            return content;
        }

        private string TrimBlock(string codeFile, string content)
        {
            var endFlag = "</Block>";
            var lineFlag = "\n";
            var star = content.IndexOf(lineFlag);
            var end = content.IndexOf(endFlag);
            var data = content[star..end];
            data = data.Replace("\n    ", "\n").TrimStart('\n');
            return data;
        }
    }
}
