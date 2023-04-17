// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;

namespace BootstrapBlazor.Shared.Practicals.Dashboard;

/// <summary>
/// Dashboard 组件
/// </summary>
public partial class Dashboard
{
    [NotNull]
    private Chart? BarChart { get; set; }

    [NotNull]
    private Chart? PieChart { get; set; }
    private DateTime DateTimePickerValue { get; set; } = DateTime.Today;

    [NotNull]
    private DashboardData? Data { get; set; }

    [Inject]
    [NotNull]
    private DashboardService? DashboardService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Data = await DashboardService.GetDashboardDataAsync(DateTimePickerValue);
    }

    /// <summary>
    /// 日期切换
    /// </summary>
    /// <returns></returns>
    private async Task OnDateTimePickerValueChanged(DateTime dt)
    {
        DateTimePickerValue = dt;
        Data = await DashboardService.GetDashboardDataAsync(dt);
        await BarChart.Reload();
        await PieChart.Reload();
        StateHasChanged();
    }

    /// <summary>
    /// 初始化柱形图
    /// </summary>
    /// <returns></returns>
    private Task<ChartDataSource> OnInitBarChartAsync()
    {
        var ds = new ChartDataSource();
        if (Data.TestDayGroupList.Any())
        {
            var set = new ChartDataset
            {
                Label = "样品数量",
                Data = Data.TestDayGroupList.Select(x => x.Count).Cast<object>()
            };
            ds.Options.Title = $"{DateTimePickerValue.Year} 年 {DateTimePickerValue.Month} 样品数据";
            ds.Options.X.Title = $"{DateTimePickerValue.Month}";
            ds.Options.Y.Title = "样品数量";
            ds.Options.ShowLegend = false;
            ds.Labels = Data.TestDayGroupList.Select(x => x.Key.ToString());
            ds.Data.Add(set);
        }

        return Task.FromResult(ds);
    }

    private Task<ChartDataSource> OnInitPieChartAsync()
    {
        var ds = new ChartDataSource();
        if (Data.TestKKSGroupList.Any())
        {
            var set = new ChartDataset
            {
                Label = $"{DateTimePickerValue.Month} 月数量",
                Data = Data.TestKKSGroupList.Select(x => x.Count).Cast<object>()
            };
            ds.Labels = Data.TestKKSGroupList.Select(x => $"{x.KKS} {x.NAM}");
            ds.Options.ShowLegend = false;
            ds.Options.ShowXScales = false;
            ds.Options.ShowYScales = false;
            ds.Data.Add(set);
        }

        return Task.FromResult(ds);
    }

    /// <summary>
    /// 根据数值大小获取颜色
    /// </summary>
    /// <param name = "value"></param>
    private static Color GetColor(double value) => value switch
    {
        100 => Color.Success,
        >= 50 => Color.Info,
        >= 25 => Color.Danger,
        _ => Color.Warning
    };
}
