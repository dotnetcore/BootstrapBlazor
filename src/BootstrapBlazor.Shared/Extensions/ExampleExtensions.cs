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
        public async Task<string> GetCodeAsync(string CodeFile)
        {
            var content = "";
            try
            {
                if (OperatingSystem.IsBrowser())
                {
                    Client.BaseAddress = new Uri($"{ServerUrl}/api/");
                    content = await Client.GetStringAsync($"Code?fileName={CodeFile}");
                }
                else
                {
                    content = await Client.GetStringAsync(CodeFile);
                }
            }
            catch (HttpRequestException) { content = "网络错误"; }
            catch (TaskCanceledException) { }
            catch (Exception) { }
            return content;
        }
    }
}
