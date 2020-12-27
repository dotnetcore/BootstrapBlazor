// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Filter 过滤条件项目
    /// </summary>
    public class FilterKeyValueAction
    {
        /// <summary>
        /// 获得/设置 Filter 项字段名称
        /// </summary>
        public string? FieldKey { get; set; }

        /// <summary>
        /// 获得/设置 Filter 项字段值
        /// </summary>
        public object? FieldValue { get; set; }

        /// <summary>
        /// 获得/设置 Filter 项与其他 Filter 逻辑关系
        /// </summary>
        public FilterLogic FilterLogic { get; set; }

        /// <summary>
        /// 获得/设置 Filter 条件行为
        /// </summary>
        public FilterAction FilterAction { get; set; }
    }
}
