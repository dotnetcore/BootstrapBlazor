// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 图表坐标轴实体类
    /// </summary>
    public class ChartAxes
    {
        /// <summary>
        /// 获得/设置 坐标轴显示名称
        /// </summary>
        public string LabelString { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 是否显示 默认为 true
        /// </summary>
        public bool Display { get; set; } = true;
    }
}
