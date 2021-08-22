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
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Charts
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Pie
    {
        private Random Randomer { get; } = new Random();
        private int PieDatasetCount = 1;
        private int PieDataCount = 5;

        [NotNull]
        private Chart? PieChart { get; set; }

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
                Logger.Log("Pie 正在加载数据 ...");
            }
        }

        private Task OnAfterInit()
        {
            Logger.Log("Pie 初始化完毕");
            return Task.CompletedTask;
        }

        private Task OnAfterUpdate(ChartAction action)
        {
            Logger.Log($"Pie 图更新数据操作完毕 -- {action}");
            return Task.CompletedTask;
        }

        private Task<ChartDataSource> OnInit()
        {
            var ds = new ChartDataSource();
            ds.Options.Title = "Pie 折线图";
            ds.Labels = Utility.Colors.Take(PieDataCount);
            for (var index = 0; index < PieDatasetCount; index++)
            {
                ds.Data.Add(new ChartDataset()
                {
                    Label = $"数据集 {index}",
                    Data = Enumerable.Range(1, PieDataCount).Select(i => Randomer.Next(20, 37)).Cast<object>()
                });
            }
            return Task.FromResult(ds);
        }
    }
}
