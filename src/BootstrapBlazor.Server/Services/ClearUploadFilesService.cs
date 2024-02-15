// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Longbow.Tasks;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// 后台任务服务类
/// </summary>
internal class ClearTempFilesService : BackgroundService
{
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="env"></param>
    /// <param name="websiteOption"></param>
    public ClearTempFilesService(IWebHostEnvironment env, IOptionsMonitor<WebsiteOptions> websiteOption)
    {
        _env = env;
        websiteOption.CurrentValue.WebRootPath = env.WebRootPath;
        websiteOption.CurrentValue.ContentRootPath = env.ContentRootPath;
        websiteOption.CurrentValue.IsDevelopment = env.IsDevelopment();
    }

    /// <summary>
    /// 运行任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TaskServicesManager.GetOrAdd("Clear Upload Files", (provider, token) =>
        {
            var webSiteUrl = $"images{Path.DirectorySeparatorChar}uploader{Path.DirectorySeparatorChar}";
            var filePath = Path.Combine(_env.WebRootPath, webSiteUrl);
            if (Directory.Exists(filePath))
            {
                Directory.EnumerateFiles(filePath).Take(10).ToList().ForEach(file => DeleteFile(file, token));
            }

            // 清除导出临时文件
            var exportFilePath = Path.Combine(_env.WebRootPath, "pdf");
            if (Directory.Exists(exportFilePath))
            {
                Directory.EnumerateFiles(exportFilePath, "*.html").Take(10).Where(file =>
                {
                    var fileInfo = new FileInfo(file);
                    return fileInfo.CreationTime.AddMinutes(5) < DateTime.Now;
                }).ToList().ForEach(i => DeleteFile(i, token));
            }
            return Task.CompletedTask;
        }, TriggerBuilder.Build(Cron.Minutely(10)));

        return Task.CompletedTask;

        void DeleteFile(string file, CancellationToken token)
        {
            try
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                File.Delete(file);
            }
            catch
            {
                // ignored
            }
        }
    }
}
