// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Bar
    {
        private Random Randomer { get; } = new Random();
        private int BarDatasetCount = 2;
        private int BarDataCount = 7;

        [NotNull]
        private Chart? BarChart { get; set; }

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
                Logger.Log("Bar 正在加载数据 ...");
            }
        }

        private Task OnAfterInit()
        {
            Logger.Log("Bar 初始化完毕");
            return Task.CompletedTask;
        }

        private Task OnAfterUpdate(ChartAction action) => InvokeAsync(() => Logger.Log($"Bar 图更新数据操作完毕 -- {action}"));

        private Task<ChartDataSource> OnInit(bool stacked)
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "Bar 折线图";
            ds.Options.X.Title = "天数";
            ds.Options.Y.Title = "数值";
            ds.Options.X.Stacked = stacked;
            ds.Options.Y.Stacked = stacked;
            ds.Labels = Enumerable.Range(1, BarDataCount).Select(i => i.ToString());
            for (var index = 0; index < BarDatasetCount; index++)
            {
                ds.Data.Add(new ChartDataset()
                {
                    Label = $"数据集 {index}",
                    Data = Enumerable.Range(1, BarDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
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
                    if (!_chartCancellationTokenSource.IsCancellationRequested) await Utility.RandomData(BarChart);
                }
            });
        }

        private void OnStopChart() => _chartCancellationTokenSource.Cancel();
    }
}
