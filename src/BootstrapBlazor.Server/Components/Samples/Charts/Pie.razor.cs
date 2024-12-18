﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Charts;

/// <summary>
/// Pie 图表示例
/// </summary>
public partial class Pie
{
    private Random Randomer { get; } = new();

    private int PieDatasetCount = 1;

    private int PieDataCount = 5;

    [NotNull]
    private Chart? PieChart { get; set; }

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
            Logger.Log("Pie loading data ...");
        }
    }

    private Task OnAfterInit()
    {
        Logger.Log("Pie initialization is complete");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        Logger.Log($"Pie Figure update data operation completed -- {action}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Pie chart";
        ds.Labels = Utility.Colors.Take(PieDataCount);
        for (var index = 0; index < PieDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, PieDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }

    private Task OnReloadChart()
    {
        PieDataCount = Randomer.Next(5, 15);
        PieChart?.Reload();
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInitAspectRatio()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Pie chart";
        ds.Labels = Utility.Colors.Take(PieDataCount);
        for (var index = 0; index < PieDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, PieDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitLegendPosition()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Pie chart";
        ds.Options.LegendPosition = ChartLegendPosition.Left;
        ds.Labels = Utility.Colors.Take(PieDataCount);
        for (var index = 0; index < PieDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, PieDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }
}
