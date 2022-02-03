// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

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
    /// <param name="selectedItems">当前选中行</param>
    /// <returns></returns>
    Task AddAsync(IEnumerable<IDynamicObject> selectedItems);

    /// <summary>
    /// 删除方法
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

    /// <summary>
    /// 动态类型属性值变化时回调方法
    /// </summary>
    /// <returns></returns>
    Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 动态类型集合变化时回调方法
    /// </summary>
    Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }
}
