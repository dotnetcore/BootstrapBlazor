// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITableFitler 接口
    /// </summary>
    public interface IFilterAction
    {
        /// <summary>
        /// 获得 IFilter 实例中的过滤条件集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<FilterKeyValueAction> GetFilterConditions();

        /// <summary>
        /// 重置过滤条件方法
        /// </summary>
        void Reset() { }
    }
}
