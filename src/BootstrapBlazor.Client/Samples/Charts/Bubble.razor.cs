// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Charts;

/// <summary>
/// Bubble 图例示例
/// </summary>
public partial class Bubble
{
    private Random Randomer { get; } = new();

    private int BubbleDatasetCount = 2;

    private int BubbleDataCount = 7;

    [NotNull]
    private Chart? BubbleChart { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Logger.Log("Bubble is loading data ...");
        }
    }

    private Task OnAfterInit()
    {
        Logger.Log("Bubble is initialized");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        Logger.Log($"Bubble graph update data operation completed -- {action}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit()
    {
        var ds = new ChartDataSource
        {
            Labels = Enumerable.Range(1, BubbleDataCount).Select(i => i.ToString())
        };
        ds.Options.Title = "Bubble chart";

        for (var index = 0; index < BubbleDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
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

    private Task OnReloadChart()
    {
        BubbleDataCount = Randomer.Next(5, 15);
        BubbleChart?.Reload();
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInitAspectRatio()
    {
        var ds = new ChartDataSource
        {
            Labels = Enumerable.Range(1, BubbleDataCount).Select(i => i.ToString())
        };
        ds.Options.Title = "Bubble chart";

        for (var index = 0; index < BubbleDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
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
