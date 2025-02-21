// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Charts;

/// <summary>
/// Chart 工具类
/// </summary>
internal static class Utility
{
    public static IEnumerable<string> Colors { get; } = new List<string>() { "Blue", "Green", "Red", "Orange", "Yellow", "Tomato", "Pink", "Violet" };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chart"></param>
    public static Task RandomData(Chart chart) => chart.Update(ChartAction.Update);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="dsCount"></param>
    public static void AddDataSet(Chart chart, ref int dsCount)
    {
        if (dsCount < Colors.Count())
        {
            dsCount++;
            _ = chart.Update(ChartAction.AddDataset);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="dsCount"></param>
    public static void RemoveDataSet(Chart chart, ref int dsCount)
    {
        if (dsCount > 1)
        {
            dsCount--;
            _ = chart.Update(ChartAction.RemoveDataset);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="daCount"></param>
    public static void AddData(Chart chart, ref int daCount)
    {
        var limit = chart.ChartType switch
        {
            ChartType.Line => 14,
            ChartType.Bar => 14,
            ChartType.Bubble => 14,
            _ => Colors.Count()
        };

        if (daCount < limit)
        {
            daCount++;
            _ = chart.Update(ChartAction.AddData);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chart"></param>
    /// <param name="daCount"></param>
    public static void RemoveData(Chart chart, ref int daCount)
    {
        var limit = chart.ChartType switch
        {
            ChartType.Line => 7,
            ChartType.Bar => 7,
            ChartType.Bubble => 4,
            _ => 2
        };
        if (daCount > limit)
        {
            daCount--;
            _ = chart.Update(ChartAction.RemoveData);
        }
    }
}
