// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Charts;

/// <summary>
/// Bar 图表示例
/// </summary>
[JSModuleAutoLoader("Samples/Charts/Bar.razor.js", JSObjectReference = true)]
public partial class Bar
{
    private int _barDatasetCount = 2;
    private int _barDataCount = 7;

    private string CustomTooltipId => $"custom_tooltip_{Id}";

    private string CustomCategoryLabelId => $"custom_category_label_{Id}";

    private string TotalDataLabelId => $"total_data_label_{Id}";

    private int BarDatasetCount { get; set; } = 2;

    private int BarDataCount { get; set; } = 7;

    [NotNull]
    private Chart? BarChart { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    /// <summary>
    /// OnAfterRender
    /// </summary>
    /// <param name = "firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            Logger.Log("Bar loading data ...");
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task InvokeInitAsync()
    {
        await InvokeVoidAsync("customTooltip", CustomTooltipId);
        await InvokeVoidAsync("customCategoryLabel", CustomCategoryLabelId);
        await InvokeVoidAsync("customTotalDataLabel", TotalDataLabelId);
    }

    private Task OnAfterInit()
    {
        Logger.Log("Bar initialization is complete");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action) => InvokeAsync(() => Logger.Log($"Bar Figure update data operation completed -- {action}"));

    private Task OnClickDataAsync((int DatasetIndex, int Index) v)
    {
        Logger.Log($"Click: DatasetIndex={v.DatasetIndex} Index={v.Index}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit(bool stacked, bool setTitle = true)
    {
        var ds = new ChartDataSource();
        if (setTitle)
        {
            ds.Options.Title = "Bar Histogram";
        }

        ds.Options.ShowDataLabel = true;
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.X.Stacked = stacked;
        ds.Options.Y.Stacked = stacked;
        ds.Labels = Enumerable.Range(1, _barDataCount).Select(i => i.ToString());
        for (var index = 0; index < _barDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, _barDataCount).Select(i => Random.Shared.Next(20, 37) / 10.0f).Cast<object>()
            });
        }

        return Task.FromResult(ds);
    }

    private CancellationTokenSource _chartCancellationTokenSource = new();

    private Task OnPlayChart()
    {
        _chartCancellationTokenSource = new CancellationTokenSource();
        return Task.Run(async () =>
        {
            while (!_chartCancellationTokenSource.IsCancellationRequested)
            {
                await Task.Delay(800, _chartCancellationTokenSource.Token);
                if (!_chartCancellationTokenSource.IsCancellationRequested)
                {
                    await Utility.RandomData(BarChart);
                }
            }
        });
    }

    private void OnStopChart() => _chartCancellationTokenSource.Cancel();

    /// <summary>
    /// 强刷控件,重新初始化控件外观
    /// </summary>
    private Task OnReloadChart()
    {
        BarDataCount = Random.Shared.Next(5, 15);
        BarChart.Reload();
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInitTwoYAxes(bool stacked, bool setTitle = true)
    {
        var ds = new ChartDataSource();
        if (setTitle)
        {
            ds.Options.Title = "Bar Histogram";
        }
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.X.Stacked = stacked;
        ds.Options.Y.Stacked = stacked;
        ds.Options.Y2.Title = "Y2 value";
        ds.Options.Y2.PositionLeft = false;

        ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
        var index = 0;
        ds.Data.Add(new ChartDataset()
        {
            Label = $"Y2 Set {index}",
            IsAxisY2 = index == 0,
            Data = Enumerable.Range(1, BarDataCount).Select(i => Random.Shared.Next(20, 7000)).Cast<object>()
        });

        for (index = 1; index < BarDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Y Set {index}",
                IsAxisY2 = index == 0,
                Data = Enumerable.Range(1, BarDataCount).Select(i => Random.Shared.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitStack(bool stacked, bool setTitle = true)
    {
        var ds = new ChartDataSource();
        if (setTitle)
        {
            ds.Options.Title = "Bar Histogram";
        }
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.X.Stacked = stacked;
        ds.Options.Y.Stacked = stacked;
        ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
        for (var index = 0; index < BarDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, BarDataCount).Select(i => Random.Shared.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitAspectRatio(bool stacked)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Bar stack demo";
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.X.Stacked = stacked;
        ds.Options.Y.Stacked = stacked;
        ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
        for (var index = 0; index < BarDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, BarDataCount).Select(i => Random.Shared.Next(20, 37)).Cast<object>()
            });
        }
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitShowDataLabel(bool showDataLabel)
    {
        var ds = new ChartDataSource();

        ds.Options.Title = "Show data label demo";
        ds.Options.ShowLegend = false;
        ds.Options.ShowDataLabel = showDataLabel;
        ds.Options.Align = ChartDataLabelPosition.Start;
        ds.Options.Anchor = ChartDataLabelPosition.End;
        ds.Options.ChartDataLabelColor = "black";
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
        ds.Data.Add(new ChartDataset()
        {
            Label = $"Set {0}",
            Data = Enumerable.Range(1, BarDataCount).Select(i => Random.Shared.Next(20, 37)).Cast<object>()
        });
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitStackedShowDataLabel()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Stacked with zero value segment";
        ds.Options.ShowDataLabel = true;
        ds.Options.X.Title = "name";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.X.Stacked = true;
        ds.Options.Y.Stacked = true;
        ds.Labels = ["Alice", "Bob", "Carol"];
        ds.Data.Add(new ChartDataset()
        {
            Label = "Set 0",
            Data = new object[] { 3, 5, 2 }
        });
        ds.Data.Add(new ChartDataset()
        {
            Label = "Set 1",
            Data = new object[] { 2, 0, 4 }
        });
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitTotalDataLabel()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Stacked total";
        ds.Options.ShowDataLabel = true;
        ds.Options.ShowTotalDataLabel = true;
        ds.Options.Anchor = ChartDataLabelPosition.Center;
        ds.Options.X.Title = "name";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.X.Stacked = true;
        ds.Options.Y.Stacked = true;
        ds.Labels = ["Alice", "Bob", "Carol"];
        ds.Data.Add(new ChartDataset()
        {
            Label = "Set 0",
            Data = new object[] { 3, 5, 2 }
        });
        ds.Data.Add(new ChartDataset()
        {
            Label = "Set 1",
            Data = new object[] { 2, 0, 4 }
        });
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitAutoSkip(bool autoSkip)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = autoSkip ? "AutoSkip: true" : "AutoSkip: false";
        ds.Options.ShowLegend = false;
        ds.Options.X.AutoSkip = autoSkip;
        ds.Options.X.Title = "category";
        ds.Options.Y.Title = "Numerical value";
        ds.Labels = Enumerable.Range(1, 16).Select(i => $"Category {i}");
        ds.Data.Add(new ChartDataset()
        {
            Label = "Set 0",
            Data = Enumerable.Range(1, 16).Select(i => Random.Shared.Next(20, 37)).Cast<object>()
        });
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitBarColorSeparately(bool barColorSeparately)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Bar color separately";
        ds.Options.ShowLegend = false;
        ds.Options.BarColorSeparately = barColorSeparately;
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
        ds.Data.Add(new ChartDataset()
        {
            Label = $"Set {0}",
            Data = Enumerable.Range(1, BarDataCount).Select(i => Random.Shared.Next(20, 37)).Cast<object>(),
            BackgroundColor = ["rgb(54, 162, 235, 0.5)", "rgb(75, 192, 192, 0.5)", "rgb(255, 99, 132, 0.5)", "rgb(255, 159, 64, 0.5)", "rgb(255, 205, 86, 0.5)", "rgb(255, 99, 71, 0.5)", "rgb(255, 192, 203, 0.5)"],
            BorderColor = ["rgb(54, 162, 235)", "rgb(75, 192, 192)", "rgb(255, 99, 132)", "rgb(255, 159, 64)", "rgb(255, 205, 86)", "rgb(255, 99, 71)", "rgb(255, 192, 203)"]
        });
        return Task.FromResult(ds);
    }
}
