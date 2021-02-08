// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal class InternalSearchAction : IFilterAction
    {
        /// <summary>
        /// 获得/设置 搜索值
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// 获得/设置 搜索属性名称
        /// </summary>
        public string? FieldKey { get; set; }

        /// <summary>
        /// 获得/设置 逻辑关系 默认为 FilterAction.Contains
        /// </summary>
        public FilterAction FilterAction { get; set; } = FilterAction.Contains;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FilterKeyValueAction> GetFilterConditions()
        {
            var filters = new List<FilterKeyValueAction>();
            if (Value != null) filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = Value,
                FilterAction = FilterAction
            });
            return filters;
        }
    }
}
