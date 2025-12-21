// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace BootstrapBlazor.Server.Components.Samples.Charts;

/// <summary>
/// Line 图表示例
/// </summary>
[JSModuleAutoLoader("Samples/Charts/Line.razor.js", JSObjectReference = true)]
public partial class Line : IDisposable
{
    private readonly Random _randomer = new();

    private int _lineDatasetCount = 2;

    private int _lineDataCount = 7;

    private Chart _lineChart = default!;

    private Chart _continueChart = default!;

    private ConsoleLogger _logger = default!;

    private ConsoleLogger _loggerTooltip = default!;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    private readonly List<int> _continueData = [1, 4, 5, 3, 4, 2, 6, 4, 9, 3, 1, 4, 5, 3, 4, 2, 6, 4, 9, 3];

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

    private string CustomTooltipId => $"custom_tooltip_{Id}";

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _code = await CodeSnippetService.GetFileContentAsync("Charts\\Line.razor.js");
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _logger.Log("Line loading data ...");

            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(2000, _cancellationTokenSource.Token);

                        await _continueChart.Reload();
                    }
                    catch (OperationCanceledException) { }
                }
            });
        }
    }

    private async Task AddDataset()
    {
        var dataset = new ChartDataset()
        {
            BorderWidth = _randomer.Next(1, 5),
            Label = $"Set {DateTime.Now:mmss}",
            Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (object)_randomer.Next(20, 37)),
            ShowPointStyle = true,
            PointStyle = chartPointStyles[_randomer.Next(0, 9)],
            PointRadius = 5,
            PointHoverRadius = 10
        };
        await _lineChart.AddDataset(dataset, 0);
    }

    private async Task RemoveDataset()
    {
        await _lineChart.RemoveDatasetAt(0);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        var chartData = Enumerable.Range(1, 7).Select(_ => Random.Next(25, 85)).ToArray();
        await InvokeVoidAsync("init", Id, chartData);
        await InvokeVoidAsync("customTooltip", CustomTooltipId, Interop, nameof(TooltipLog));
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

        ds.Labels = Enumerable.Range(1, _lineDataCount).Select(i => i.ToString());
        for (var index = 0; index < _lineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                BorderWidth = _randomer.Next(1, 5),
                Tension = tension,
                Label = $"Set {index}",
                Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)_randomer.Next(20, 37)),
                ShowPointStyle = true,
                PointStyle = chartPointStyles[_randomer.Next(0, 9)],
                PointRadius = 5,
                PointHoverRadius = 10
            });
        }

        // 模拟异步
        await Task.Delay(100);
        return ds;
    }

    private int _lineChartStartIndex = 0;

    private async Task<ChartDataSource> OnInitContinue()
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Line Chart";
        ds.Options.LegendLabelsFontSize = 16;
        ds.Options.X.Title = "Count";
        ds.Options.Y.Title = "Value";

        _lineChartStartIndex++;
        _continueData.RemoveAt(0);
        _continueData.Add(_randomer.Next(10));

        ds.Labels = Enumerable.Range(_lineChartStartIndex, 20).Select(i => i.ToString());
        ds.Data.Add(new ChartDataset()
        {
            BorderWidth = 1,
            Label = "Dataset 1",
            Data = _continueData.Select(i => (object)i),
            ShowPointStyle = false,
            PointStyle = ChartPointStyle.Circle,
            PointRadius = 2,
            PointHoverRadius = 4
        });
        await Task.Delay(100);
        return ds;
    }

    private Task OnAfterInit()
    {
        _logger.Log("Line initialization is complete");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        _logger.Log($"Line Figure update data operation completed -- {action}");
        return Task.CompletedTask;
    }

    private Task OnReloadChart()
    {
        _lineDataCount = _randomer.Next(5, 15);
        _lineChart.Reload();
        return Task.CompletedTask;
    }

    private async Task<ChartDataSource> OnInitTension(float tension, bool hasNull)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Line Chart";
        ds.Options.X.Title = "days";
        ds.Options.Y.Title = "Numerical value";
        ds.Labels = Enumerable.Range(1, _lineDataCount).Select(i => i.ToString());
        for (var index = 0; index < _lineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"Set {index}",
                Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)_randomer.Next(20, 37))
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
        ds.Labels = Enumerable.Range(1, _lineDataCount).Select(i => i.ToString());
        for (var index = 0; index < _lineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"Set {index}",
                Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)_randomer.Next(20, 37))
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

        ds.Labels = Enumerable.Range(1, _lineDataCount).Select(i => i.ToString());
        var index = 0;
        ds.Data.Add(new ChartDataset()
        {
            Tension = tension,
            Label = $"Y2 Set {index}",
            IsAxisY2 = index == 0,
            Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)_randomer.Next(20, 7000))
        });

        for (index = 1; index < _lineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"Y Set {index}",
                IsAxisY2 = index == 0,
                Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)_randomer.Next(20, 37))
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
        ds.Labels = Enumerable.Range(1, _lineDataCount).Select(i => i.ToString());
        for (var index = 0; index < _lineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"Set {index}",
                Data = Enumerable.Range(1, _lineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)_randomer.Next(20, 37))
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

    private string? _code;

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
    }

    private static Task<ChartDataSource> GetData()
    {
        var BarDataCount = 6;
        var BarDatasetCount = 3;
        var Randomer = new Random();
        var ds = new ChartDataSource();
        ds.Options.Title = "Bar Histogram";
        ds.Options.X.Title = "Days";
        ds.Options.Y.Title = "Numerical value";
        ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
        for (var index = 0; index < BarDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Label = $"Set {index}",
                Data = Enumerable.Range(1, BarDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
            });
        }
        ds.AppendData = GetAppendData();
        return Task.FromResult(ds);
    }

    private static object GetAppendData()
    {
        var steps = new List<object>();
        for (var index = 0; index < 3; index++)
        {
            // 添加 ChartDataset 未提供的 Stepped 参数
            steps.Add(new { Stepped = true });
        }
        return new { Data = steps };
    }

    /// <summary>
    /// 自定义 Tooltip 回调方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public Task TooltipLog(long sum)
    {
        _loggerTooltip.Log($"Tooltip sum callback: {sum}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
