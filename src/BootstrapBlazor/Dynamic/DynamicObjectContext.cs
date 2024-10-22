﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态类型上下文基类 <see cref="IDynamicObjectContext" />
/// </summary>
public abstract class DynamicObjectContext : IDynamicObjectContext
{
    /// <summary>
    /// 获取动态类型各列信息
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerable<ITableColumn> GetColumns();

    /// <summary>
    /// 获得动态类数据方法
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerable<IDynamicObject> GetItems();

    /// <summary>
    /// 
    /// </summary>
    protected ConcurrentDictionary<string, List<CustomAttributeBuilder>> CustomerAttributeBuilderCache { get; } = new();

    /// <summary>
    /// 添加标签到指定列
    /// </summary>
    /// <param name="columnName">指定列名称</param>
    /// <param name="attributeType">Attribute 实例</param>
    /// <param name="types"></param>
    /// <param name="constructorArgs"></param>
    /// <param name="propertyInfos"></param>
    /// <param name="propertyValues"></param>
    public void AddAttribute(string columnName, Type attributeType, Type[] types, object?[] constructorArgs, PropertyInfo[]? propertyInfos = null, object?[]? propertyValues = null)
    {
        var attr = attributeType.GetConstructor(types);
        if (attr != null)
        {
            var cab = new CustomAttributeBuilder(attr, constructorArgs, namedProperties: propertyInfos ?? [], propertyValues: propertyValues ?? []);
            CustomerAttributeBuilderCache.AddOrUpdate(columnName, key => [cab], (key, builders) =>
            {
                builders.Add(cab);
                return builders;
            });
        }
    }

    /// <summary>
    /// 列创建回调方法 入口参数为 ITableColumn 实例 返回值为 CustomAttributeBuilder 集合
    /// </summary>
    protected internal virtual IEnumerable<CustomAttributeBuilder> OnColumnCreating(ITableColumn col) => CustomerAttributeBuilderCache.TryGetValue(col.GetFieldName(), out var builders)
        ? builders
        : Enumerable.Empty<CustomAttributeBuilder>();

    /// <summary>
    /// 动态类型新建回调委托
    /// </summary>
    /// <param name="selectedItems">当前选中行</param>
    /// <returns></returns>
    public abstract Task AddAsync(IEnumerable<IDynamicObject> selectedItems);

    /// <summary>
    /// 动态类型删除回调委托
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public abstract Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

    /// <summary>
    /// 动态类型集合变化时回调方法
    /// </summary>
    /// <returns></returns>
    public Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 动态类型属性值变化时回调方法
    /// </summary>
    /// <returns></returns>
    public Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }

    /// <summary>
    /// 获得选中行比对回调方法
    /// </summary>
    public Func<IDynamicObject?, IDynamicObject?, bool>? EqualityComparer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<QueryPageOptions, IEnumerable<IDynamicObject>, IEnumerable<IDynamicObject>>? OnFilterCallback { get; set; }
}
