// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 图表组件数据源
    /// </summary>
    public class ChartDataSource
    {
        /// <summary>
        /// 获得/设置 图表 X 轴显示数据标签集合
        /// </summary>
        public IEnumerable<string>? Labels { get; set; }

        /// <summary>
        /// 获得 图表 数据集
        /// </summary>
        public List<ChartDataset> Data { get; } = new List<ChartDataset>();

        /// <summary>
        /// 获得 组件配置项 设置标题 轴坐标等
        /// </summary>
        public ChartOptions Options { get; } = new ChartOptions();
    }
}
