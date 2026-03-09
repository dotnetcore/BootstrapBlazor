// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态类型上下文接口</para>
/// <para lang="en">Dynamic type context interface</para>
/// </summary>
public interface IDynamicObjectContext
{
    /// <summary>
    /// <para lang="zh">获取动态类型各列信息</para>
    /// <para lang="en">Gets the information of each column in the dynamic type</para>
    /// </summary>
    IEnumerable<ITableColumn> GetColumns();

    /// <summary>
    /// <para lang="zh">获得动态数据方法</para>
    /// <para lang="en">Gets the dynamic data</para>
    /// </summary>
    IEnumerable<IDynamicObject> GetItems();

    /// <summary>
    /// <para lang="zh">新建方法</para>
    /// <para lang="en">Creates new items</para>
    /// </summary>
    /// <param name="selectedItems"><para lang="zh">当前选中行</para><para lang="en">Currently selected rows</para></param>
    Task AddAsync(IEnumerable<IDynamicObject> selectedItems);

    /// <summary>
    /// <para lang="zh">删除方法</para>
    /// <para lang="en">Deletes items</para>
    /// </summary>
    /// <param name="items"><para lang="zh">要删除的行</para><para lang="en">Rows to be deleted</para></param>
    Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

    /// <summary>
    /// <para lang="zh">获得/设置 动态类型属性值变化时回调方法 默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when the value of a dynamic type property changes. Default is null</para>
    /// </summary>
    Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 动态类型数据集过滤回调委托 默认为 null</para>
    /// <para lang="en">Gets or sets the callback delegate for filtering the dynamic data set. Default is null</para>
    /// </summary>
    Func<QueryPageOptions, IEnumerable<IDynamicObject>, IEnumerable<IDynamicObject>>? OnFilterCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 动态类型集合变化时回调方法 默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when the dynamic type collection changes. Default is null</para>
    /// </summary>
    Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 选中行是否相等判断逻辑 默认为 null</para>
    /// <para lang="en">Gets or sets the logic for determining whether selected rows are equal. Default is null</para>
    /// </summary>
    Func<IDynamicObject?, IDynamicObject?, bool>? EqualityComparer { get; set; }
}
