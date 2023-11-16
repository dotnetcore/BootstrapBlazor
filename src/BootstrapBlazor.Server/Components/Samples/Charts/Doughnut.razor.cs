// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Charts;

/// <summary>
/// Doughnut 图例示例
/// </summary>
public partial class Doughnut
{
    private Random Randomer { get; } = new();

    private int DoughnutDatasetCount = 1;

    private int DoughnutDataCount = 5;

    [NotNull]
    private Chart? DoughnutChart { get; set; }

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
            Logger.Log("Doughnut is loading data ...");
        }
    }

    private Task OnAfterInit()
    {
        Logger.Log("Doughnut is initialized");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        Logger.Log($"Doughnut graph update data operation completed -- {action}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Doughnut Donut Chart";
        ds.Labels = Utility.Colors.Take(DoughnutDataCount);
        for (var index = 0; index < DoughnutDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, DoughnutDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }

    private bool IsCircle { get; set; }

    private int Angle { get; set; }

    private async Task ToggleCircle()
    {
        IsCircle = !IsCircle;
        Angle = IsCircle ? 360 : 0;
        await DoughnutChart.Update(ChartAction.SetAngle);
    }

    private Task OnReloadChart()
    {
        DoughnutDataCount = Randomer.Next(5, 15);
        DoughnutChart?.Reload();
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInitAspectRatio()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Doughnut Donut Chart";
        ds.Labels = Utility.Colors.Take(DoughnutDataCount);
        for (var index = 0; index < DoughnutDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, DoughnutDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }
}
