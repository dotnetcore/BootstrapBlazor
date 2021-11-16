// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Services
{
    internal class CodeSnippetService
    {
        private HttpClient Client { get; set; }

        private string ServerUrl { get; set; }

        private bool IsDevelopment { get; }

        private string ContentRootPath { get; }

        private JsonLocalizationOptions Option { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        /// <param name="option"></param>
        public CodeSnippetService(HttpClient client, IOptions<WebsiteOptions> options, IOptions<JsonLocalizationOptions> option)
        {
            Client = client;
            Client.Timeout = TimeSpan.FromSeconds(5);
            Client.BaseAddress = new Uri(options.Value.RepositoryUrl);

            IsDevelopment = options.Value.IsDevelopment;
            ContentRootPath = options.Value.ContentRootPath;
            ServerUrl = options.Value.ServerUrl;

            Option = option.Value;
        }

        /// <summary>
        /// 获得示例源码方法
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
                    content = CacheManagerHelper.GetCode(codeFile, blockTitle, entry =>
                    {
                        payload = Filter(payload.AsMemory(), blockTitle.AsMemory());

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

        private Task<string> GetContentFromFile(string codeFile) => CacheManagerHelper.GetContentFromFileAsync(codeFile, async entry =>
        {
            var payload = "";

            if (IsDevelopment)
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

            List<KeyValuePair<string, string>> GetLocalizers() => CacheManagerHelper.GetLocalizers(codeFile, entry =>
            {
                var typeName = Path.GetFileNameWithoutExtension(codeFile);
                var sections = CacheManager.GetJsonStringConfig(typeof(CodeSnippetService).Assembly, Option);
                var v = sections
                    .FirstOrDefault(s => $"BootstrapBlazor.Shared.Samples.{typeName}".Equals(s.Key, StringComparison.OrdinalIgnoreCase))?
                    .GetChildren()
                    .SelectMany(c => new KeyValuePair<string, string>[]
                    {
                        new KeyValuePair<string, string>(c.Key, c.Value)
                    }).ToList();
                return v ?? new List<KeyValuePair<string, string>>();
            });
        });

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

        private string Filter(ReadOnlyMemory<char> content, ReadOnlyMemory<char> blockTitle)
        {
            var beginFlag = "<DemoBlock ";
            var endFlag = "</DemoBlock>";
            var endLength = endFlag.Length;

            var findStrings = new string[] { $"Name=\"{blockTitle}\"", $"Title=\"{blockTitle}\"" };
            while (!content.IsEmpty)
            {
                var star = content.Span.IndexOf(beginFlag);
                if (star == -1)
                {
                    break;
                }

                var length = content.Span.IndexOf(endFlag);
                if (length == -1)
                {
                    break;
                }

                // 获得 DemoBlock 代码块
                var seg = content[star..length];
                if (seg.Span.IndexOf(findStrings[0]) > -1 || seg.Span.IndexOf(findStrings[1]) > -1)
                {
                    FormatIndent(seg);
                    break;
                }

                // 处理后续字符串
                content = content[(length + endLength)..];
            }
            TrimTips();
            return content.ToString();

            void FormatIndent(ReadOnlyMemory<char> seg)
            {
                var lineFlag = "\n";
                var star = seg.Span.IndexOf(lineFlag);
                var data = seg[star..];

                var sb = new StringBuilder();
                while (!data.IsEmpty)
                {
                    var index = data.Span.IndexOf("\n    ");
                    if (index == 0)
                    {
                        data = data[5..];
                    }
                    else if (index > 0)
                    {
                        sb.Append(data.Span[..(index + 1)]);
                        data = data[(index + 5)..];
                    }
                    else
                    {
                        sb.Append(data);
                        data = default;
                    }
                }
                content = sb.ToString().AsMemory();
            }

            void TrimTips()
            {
                var beginFlag = "<Tips>";
                var endFlag = $"</Tips>{Environment.NewLine}";
                var endLength = endFlag.Length;
                var star = content.Span.IndexOf(beginFlag);
                if (star > -1)
                {
                    var length = content.Span.IndexOf(endFlag);
                    if (length > -1)
                    {
                        content = $"{content[..star]}{content[(length + endLength)..]}".AsMemory();
                    }
                }
            }
        }
    }
}
