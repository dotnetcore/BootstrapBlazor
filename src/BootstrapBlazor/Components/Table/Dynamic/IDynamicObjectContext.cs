// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// 新建方法
        /// </summary>
        /// <returns></returns>
        Task<IDynamicObject> AddAsync();

        /// <summary>
        /// 保存方法
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<bool> SaveAsync(IDynamicObject item);

        /// <summary>
        /// 删除方法
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

        /// <summary>
        /// 
        /// </summary>
        Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }
    }
}
