// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Components;

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
            Logger.Log("Line 正在加载数据 ...");
        }
    }

    private Task OnAfterInit()
    {
        Logger.Log("Line 初始化完毕");
        return Task.CompletedTask;
    }

    private Task OnAfterUpdate(ChartAction action)
    {
        Logger.Log($"Line 图更新数据操作完毕 -- {action}");
        return Task.CompletedTask;
    }

    private Task<ChartDataSource> OnInit(float tension, bool hasNull)
    {
        var ds = new ChartDataSource();
        ds.Options.Title = "Line 折线图";
        ds.Options.X.Title = "天数";
        ds.Options.Y.Title = "数值";
        ds.Labels = Enumerable.Range(1, LineDataCount).Select(i => i.ToString());
        for (var index = 0; index < LineDatasetCount; index++)
        {
            ds.Data.Add(new ChartDataset()
            {
                Tension = tension,
                Label = $"数据集 {index}",
                Data = Enumerable.Range(1, LineDataCount).Select((i, index) => (index == 2 && hasNull) ? null! : (object)Randomer.Next(20, 37))
            });
        }
        return Task.FromResult(ds);
    }
}
