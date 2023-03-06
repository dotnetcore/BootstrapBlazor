// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Charts;

/// <summary>
/// 
/// </summary>
public partial class Line
{
    private Random Randomer { get; } = new Random();
    private int LineDatasetCount = 2;
    private int LineDataCount = 7;

    [NotNull]
    private Chart? LineChart { get; set; }

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
            Logger.Log("Line loading data ...");
        }
    }

    private Task OnAfterInit()
    {
        Logger.Log("Line initialization is complete");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        Logger.Log($"Line Figure update data operation completed -- {action}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit(float tension, bool hasNull)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Line Chart";
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Labels = Enumerable.Range(1, LineDataCount).Select(i => i.ToString());
        for (var index = 0; index < LineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"Set {index}",
                Data = Enumerable.Range(1, LineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)Randomer.Next(20, 37))
            });
        }
        return Task.FromResult(ds);
    }

    /// <summary>
    /// 强刷控件,重新初始化控件外观
    /// </summary>
    private Task OnReloadChart()
    {
        LineDataCount = Randomer.Next(5, 15);
        LineChart?.Reload();
        return Task.CompletedTask;
    }
}
