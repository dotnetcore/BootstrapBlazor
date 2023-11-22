// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.Charts;

/// <summary>
/// Line 图表示例
/// </summary>
[JSModuleAutoLoader("Samples/Charts/Line.razor.js")]
public partial class Line
{
    private Random Randomer { get; } = new();

    private int LineDatasetCount = 2;

    private int LineDataCount = 7;

    [NotNull]
    private Chart? LineChart { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private readonly ChartPointStyle[] chartPointStyles =
    [
        ChartPointStyle.Circle,
        ChartPointStyle.Cross,
        ChartPointStyle.CrossRot,
        ChartPointStyle.Dash,
        ChartPointStyle.Line,
        ChartPointStyle.Rect,
        ChartPointStyle.RectRounded,
        ChartPointStyle.RectRot,
        ChartPointStyle.Star,
        ChartPointStyle.Triangle,
    ];

    /// <summary>
    /// <inheritdoc/>
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

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Code = await CodeSnippetService.GetFileContentAsync("Charts\\Line.razor.js");
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var chartData = Enumerable.Range(1, 7).Select(_ => Random.Next(25, 85)).ToArray();
        await InvokeVoidAsync("init", Id, chartData);
    }

    private async Task<ChartDataSource> OnInit(float tension, bool hasNull)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Line Chart";
        ds.Options.LegendLabelsFontSize = 16;
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Options.XScalesBorderColor = "red";
        ds.Options.YScalesBorderColor = "red";

        ds.Options.XScalesGridColor = "blue";
        ds.Options.XScalesGridTickColor = "blue";
        ds.Options.XScalesGridBorderColor = "blue";

        ds.Options.YScalesGridColor = "blue";
        ds.Options.YScalesGridTickColor = "blue";
        ds.Options.YScalesGridBorderColor = "blue";

        ds.Labels = Enumerable.Range(1, LineDataCount).Select(i => i.ToString());
        for (var index = 0; index < LineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                BorderWidth = Randomer.Next(1, 5),
                Tension = tension,
                Label = $"Set {index}",
                Data = Enumerable.Range(1, LineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)Randomer.Next(20, 37)),
                ShowPointStyle = true,
                PointStyle = chartPointStyles[Randomer.Next(0, 9)],
                PointRadius = 5,
                PointHoverRadius = 10
            });
        }

        // 模拟异步
        await Task.Delay(100);
        return ds;
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

    private Task OnReloadChart()
    {
        LineDataCount = Randomer.Next(5, 15);
        LineChart?.Reload();
        return Task.CompletedTask;
    }

    private async Task<ChartDataSource> OnInitTension(float tension, bool hasNull)
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

        // 模拟异步
        await Task.Delay(100);
        return ds;
    }

    private Task<ChartDataSource> OnInitNullable(float tension, bool hasNull)
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

    private Task<ChartDataSource> OnInitTwoAxes(float tension, bool hasNull)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Line Chart";
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Y value";
        ds.Options.Y2.Title = "Y2 value";
        ds.Options.Y2.PositionLeft = false;

        ds.Labels = Enumerable.Range(1, LineDataCount).Select(i => i.ToString());
        var index = 0;
        ds.Data.Add(new ChartDataset()
        {
            Tension = tension,
            Label = $"Y2 Set {index}",
            IsAxisY2 = index == 0,
            Data = Enumerable.Range(1, LineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)Randomer.Next(20, 7000))
        });

        for (index = 1; index < LineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"Y Set {index}",
                IsAxisY2 = index == 0,
                Data = Enumerable.Range(1, LineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)Randomer.Next(20, 37))
            });
        }
        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitAspectRatio(float tension, bool hasNull)
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
    /// Random 随机数生成器
    /// </summary>
    private Random Random { get; } = new();

    private async Task Randomize()
    {
        //随机生成一组数据
        //Randomly generate a set of data
        var chartData = Enumerable.Range(1, 7).Select(_ => Random.Next(25, 85));
        await InvokeVoidAsync("randomize", Id, chartData);
    }

    /// <summary>
    /// JS 代码段
    /// JS Code
    /// </summary>
    private string? Code { get; set; }
}
