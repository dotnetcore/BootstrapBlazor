// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 图表组件数据集合实体类
    /// </summary>
    public class ChartDataset
    {
        /// <summary>
        /// 获得/设置 数据集合名称
        /// </summary>
        public string Label { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        public IEnumerable<object>? Data { get; set; }

        /// <summary>
        /// 获得/设置 是否填充 默认 false
        /// </summary>
        public bool Fill { get; set; }
    }
}
