// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 动态类型上下文接口
    /// </summary>
    public interface IDynamicObjectContext
    {
        /// <summary>
        /// 获取动态类型各列信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<ITableColumn> GetColumns();

        /// <summary>
        /// 获得动态数据方法
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDynamicObject> GetItems();
    }
}
