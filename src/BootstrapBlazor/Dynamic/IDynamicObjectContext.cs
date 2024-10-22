// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// 获得/设置 动态类型属性值变化时回调方法 默认为 null
    /// </summary>
    /// <returns></returns>
    Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 获得/设置 动态类型数据集过滤回调委托 默认为 null
    /// </summary>
    Func<QueryPageOptions, IEnumerable<IDynamicObject>, IEnumerable<IDynamicObject>>? OnFilterCallback { get; set; }

    /// <summary>
    /// 获得/设置 动态类型集合变化时回调方法 默认为 null
    /// </summary>
    Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }

    /// <summary>
    /// 获得/设置 选中行是否相等判断逻辑 默认为 null
    /// </summary>
    Func<IDynamicObject?, IDynamicObject?, bool>? EqualityComparer { get; set; }
}
