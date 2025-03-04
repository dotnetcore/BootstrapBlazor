// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Longbow.Tasks;

namespace BootstrapBlazor.Server.Services;

class MockOnlineContributor(IDispatchService<Contributor> dispatchService) : BackgroundService
{
    /// <summary>
    /// 运行任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TaskServicesManager.GetOrAdd("OnlineSheet", (provider, token) =>
        {
            dispatchService.Dispatch(new DispatchEntry<Contributor>()
            {
                Name = "OnlineSheet-Demo",
                Entry = new Contributor()
                {
                    Name = "Argo Zhang",
                    Avatar = "/images/Argo-C.png",
                    Description = "正在更新单元格 A8",
                    Data = new UniverSheetData()
                    {
                        CommandName = "UpdateRange",
                        Data = new
                        {
                            Range = "A8",
                            Value = $"{DateTime.Now: yyyy-MM-dd HH:mm:ss} Argo 更新此单元格"
                        }
                    }
                }
            });
            return Task.CompletedTask;
        }, TriggerBuilder.Build(Cron.Secondly(5)));

        return Task.CompletedTask;
    }
}

/// <summary>
/// Contributor
/// </summary>
public class Contributor
{
    /// <summary>
    /// Gets or sets Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets Avatar
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// Gets or sets Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets Sheet data
    /// </summary>
    public UniverSheetData? Data { get; set; }
}
