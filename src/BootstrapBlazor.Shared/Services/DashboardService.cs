// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Shared.Components.Samples.Tutorials;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// 仪表盘数据提供服务
/// </summary>
class DashboardService
{
    private static readonly Random _random = Random.Shared;

    /// <summary>
    /// 获取仪表盘数据
    /// </summary>
    /// <returns></returns>
    public Task<DashboardData> GetDashboardDataAsync()
    {
        //填充随机数据，仅做展示用
        var data = new DashboardData
        {
            TestDayCount = _random.Next(10, 99),
            TestMonthCount = _random.Next(100, 999),
            TestYearCount = _random.Next(1000, 4999),
            TestAllCount = _random.Next(5000, 9999),

            TestApprovedDayCount = _random.Next(10, 59),
            TestApprovedMonthCount = _random.Next(100, 499),
            TestApprovedYearCount = _random.Next(1000, 2999),
            TestApprovedAllCount = _random.Next(4000, 4999),

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
            result.Add(new TestDayGroupData() { Key = i, Count = _random.Next(1, 99) });
        }

        return [.. result.OrderBy(x => x.Key)];
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
                Count = _random.Next(10, 99),
                Percent = _random.Next(1, 99)
            });
        }

        foreach (var item in result)
        {
            item.ApprovedCount = item.Count * (int)item.Percent / 100;
        }

        return [.. result.OrderByDescending(x => x.Percent)];
    }
}
