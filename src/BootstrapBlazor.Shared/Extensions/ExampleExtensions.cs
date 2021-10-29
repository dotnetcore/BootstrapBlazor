// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.Options;
using System;
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
            services.AddTransient<ExampleService>();
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

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="client"></param>
        /// <param name="options"></param>
        public ExampleService(HttpClient client, IOptions<WebsiteOptions> options)
        {
            Client = client;
            Client.Timeout = TimeSpan.FromSeconds(5);
            Client.BaseAddress = new Uri(options.Value.RepositoryUrl);

            ServerUrl = options.Value.ServerUrl;
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
                if (OperatingSystem.IsBrowser())
                {
                    Client.BaseAddress = new Uri($"{ServerUrl}/api/");
                    content = await Client.GetStringAsync($"Code?fileName={codeFile}");
                }
                else
                {
                    content = await Client.GetStringAsync(codeFile);
                }

                if (codeFile.EndsWith(".razor", StringComparison.OrdinalIgnoreCase))
                {
                    content = Filter(content, blockTitle);
                }
            }
            catch (HttpRequestException) { content = "网络错误"; }
            catch (TaskCanceledException) { }
            catch (Exception) { }
            return content;
        }

        private static string Filter(string content, string? blockTitle)
        {
            if (!string.IsNullOrEmpty(blockTitle))
            {
                var beginFlag = "<Block ";
                var endFlag = "</Block>";
                var endLength = endFlag.Length;
                while (content.Length > 0)
                {
                    var span = content.AsSpan();
                    var index = span.IndexOf(beginFlag);
                    if (index == -1)
                    {
                        break;
                    }

                    var length = span.IndexOf(endFlag);
                    if (length == -1)
                    {
                        break;
                    }

                    var seg = span[index..(length + endLength)];
                    if (seg.IndexOf(blockTitle) > -1)
                    {
                        content = seg.ToString();
                        break;
                    }
                    else
                    {
                        content = span[(length + endLength)..].ToString();
                    }
                }
            }
            return content;
        }
    }
}
