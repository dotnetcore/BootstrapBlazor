// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Practices.Dashboard;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// 仪表盘数据提供服务
/// </summary>
class DashboardService
{
    private Random Random { get; } = new Random();

    /// <summary>
    /// 获取仪表盘数据
    /// </summary>
    /// <returns></returns>
    public Task<DashboardData> GetDashboardDataAsync(DateTime dateTime)
    {
        //填充随机数据，仅做展示用
        var data = new DashboardData
        {
            TestDayCount = Random.Next(10, 99),
            TestMonthCount = Random.Next(100, 999),
            TestYearCount = Random.Next(1000, 4999),
            TestAllCount = Random.Next(5000, 9999),

            TestApprovedDayCount = Random.Next(10, 59),
            TestApprovedMonthCount = Random.Next(100, 499),
            TestApprovedYearCount = Random.Next(1000, 2999),
            TestApprovedAllCount = Random.Next(4000, 4999),

            TestDayGroupList = GetDayOfMonthGroup(),
            TestKKSGroupList = GetTestKKSGroup()
        };

        //日签发占比
        var dayScale = Math.Round(data.TestApprovedDayCount / (double)data.TestDayCount * 100, 1);
        data.TestApprovedDayScale = dayScale is double.NaN ? 0 : dayScale;

        //月签发占比
        var monthScale = Math.Round(data.TestApprovedMonthCount / (double)data.TestMonthCount * 100, 1);
        data.TestApprovedMonthScale = monthScale is double.NaN ? 0 : monthScale;

        //年签发占比
        var yearScale = Math.Round(data.TestApprovedYearCount / (double)data.TestYearCount * 100, 1);
        data.TestApprovedYearScale = yearScale is double.NaN ? 0 : yearScale;

        //全部签发占比
        var allScale = Math.Round(data.TestApprovedAllCount / (double)data.TestAllCount * 100, 1);
        data.TestApprovedAllScale = allScale is double.NaN ? 0 : allScale;

        return Task.FromResult(data);
    }

    /// <summary>
    /// 按年月日分组统计
    /// </summary>
    /// <returns></returns>
    private List<TestDayGroupData> GetDayOfMonthGroup()
    {
        var result = new List<TestDayGroupData>();

        //按照当前月的每一天填充数据
        for (var i = 1; i <= 30; i++)
        {
            result.Add(new TestDayGroupData() { Key = i, Count = Random.Next(1, 99) });
        }

        return result.OrderBy(x => x.Key).ToList();
    }

    /// <summary>
    /// KKS分类分组统计
    /// </summary>
    private List<TestKKSGroupData> GetTestKKSGroup()
    {
        var result = new List<TestKKSGroupData>();

        for (var i = 0; i < 20; i++)
        {
            result.Add(new TestKKSGroupData()
            {
                NAM = $"Blazor",
                KKS = $"Bootstrap",
                Count = Random.Next(10, 99),
                Percent = Random.Next(1, 99)
            });
        }

        foreach (var item in result)
        {
            item.ApprovedCount = item.Count * (int)item.Percent / 100;
        }

        return result.OrderByDescending(x => x.Percent).ToList();
    }
}
