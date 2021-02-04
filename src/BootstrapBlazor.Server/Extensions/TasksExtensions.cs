// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Data;
using Longbow.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 后台任务扩展方法
    /// </summary>
    internal static class TasksExtensions
    {
        /// <summary>
        /// 添加示例后台任务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddBlazorBackgroundTask(this IServiceCollection services)
        {
            services.AddSingleton<WeatherForecastService>();
            services.AddTaskServices();
            services.AddHttpClient();
            services.AddVersionManager();
            services.AddExampleService();
            services.AddSingleton<IConfigureOptions<WebsiteOptions>, ConfigureOptions<WebsiteOptions>>();
            services.AddHostedService<BlazorBackgroundServices>();
            return services;
        }
    }

    /// <summary>
    /// 后台任务服务类
    /// </summary>
    internal class BlazorBackgroundServices : BackgroundService
    {
        private readonly IWebHostEnvironment? _env;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="env"></param>
        /// <param name="websiteOption"></param>
        public BlazorBackgroundServices(IWebHostEnvironment env, IOptions<WebsiteOptions> websiteOption)
        {
            _env = env;
            websiteOption.Value.WebRootPath = env.WebRootPath;
        }

        /// <summary>
        /// 运行任务
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            TaskServicesManager.GetOrAdd("Clear Upload Files", token =>
            {
                if (_env != null)
                {
                    var webSiteUrl = $"images{Path.DirectorySeparatorChar}uploader{Path.DirectorySeparatorChar}";
                    var filePath = Path.Combine(_env.WebRootPath, webSiteUrl);
                    if (Directory.Exists(filePath))
                    {
                        Directory.EnumerateFiles(filePath).Take(10).ToList().ForEach(file =>
                        {
                            try
                            {
                                if (token.IsCancellationRequested) return;
                                File.Delete(file);
                            }
                            catch { }
                        });
                    }

                }
                return Task.CompletedTask;
            }, TriggerBuilder.Build(Cron.Minutely(10)));

            return Task.CompletedTask;
        }
    }
}
