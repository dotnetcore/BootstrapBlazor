// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 图表组件配置项实体类
    /// </summary>
    public class ChartOptions
    {
        /// <summary>
        /// 获得/设置 表格 Title
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 获得 X 坐标轴实例集合
        /// </summary>
        public ChartAxes X { get; } = new ChartAxes();

        /// <summary>
        /// 获得 X 坐标轴实例集合
        /// </summary>

        public ChartAxes Y { get; } = new ChartAxes();

        /// <summary>
        /// 获得/设置 是否 适配移动端 默认为 true
        /// </summary>
        public bool Responsive { get; set; } = true;
    }
}
