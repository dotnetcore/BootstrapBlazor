// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples.Charts;

/// <summary>
/// 
/// </summary>
public partial class Bubble
{
    private Random Randomer { get; } = new Random();
    private int BubbleDatasetCount = 2;
    private int BubbleDataCount = 7;

    [NotNull]
    private Chart? BubbleChart { get; set; }

    [NotNull]
    private BlockLogger? Logger { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Logger.Log("Bubble 正在加载数据 ...");
        }
    }

    private Task OnAfterInit()
    {
        Logger.Log("Bubble 初始化完毕");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        Logger.Log($"Bubble 图更新数据操作完毕 -- {action}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit()
    {
        var ds = new ChartDataSource
        {
            Labels = Enumerable.Range(1, BubbleDataCount).Select(i => i.ToString())
        };
        ds.Options.Title = "Bubble 气泡图";

        for (var index = 0; index < BubbleDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"数据集 {index}",
                Data = Enumerable.Range(1, BubbleDataCount).Select(i => new
                {
                    x = Randomer.Next(10, 40),
                    y = Randomer.Next(10, 40),
                    r = Randomer.Next(1, 20)
                })
            });
        }
        return Task.FromResult(ds);
    }
}
