// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">动态类型上下文基类 <see cref="IDynamicObjectContext" /></para>
/// <para lang="en">动态type上下文基类 <see cref="IDynamicObjectContext" /></para>
/// </summary>
public abstract class DynamicObjectContext : IDynamicObjectContext
{
    /// <summary>
    /// <para lang="zh">获取动态类型各列信息</para>
    /// <para lang="en">获取动态type各列信息</para>
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerable<ITableColumn> GetColumns();

    /// <summary>
    /// <para lang="zh">获得动态类数据方法</para>
    /// <para lang="en">Gets动态类data方法</para>
    /// </summary>
    /// <returns></returns>
    public abstract IEnumerable<IDynamicObject> GetItems();

    /// <summary>
    /// <para lang="zh"></para>
    /// <para lang="en"></para>
    /// </summary>
    protected ConcurrentDictionary<string, List<CustomAttributeBuilder>> CustomerAttributeBuilderCache { get; } = new();

    /// <summary>
    /// <para lang="zh">添加标签到指定列</para>
    /// <para lang="en">添加标签到指定列</para>
    /// </summary>
    /// <param name="columnName"><para lang="zh">指定列名称</para><para lang="en">指定列name</para></param>
    /// <param name="attributeType"><para lang="zh">Attribute 实例</para><para lang="en">Attribute instance</para></param>
    /// <param name="types"></param>
    /// <param name="constructorArgs"></param>
    /// <param name="propertyInfos"></param>
    /// <param name="propertyValues"></param>
    public void AddAttribute(string columnName, Type attributeType, Type[] types, object?[] constructorArgs, PropertyInfo[]? propertyInfos = null, object?[]? propertyValues = null)
    {
        var attr = attributeType.GetConstructor(types);
        if (attr != null)
        {
            CustomerAttributeBuilderCache.AddOrUpdate(columnName, key => [CreateCustomAttributeBuilder()], (key, builders) =>
            {
                builders.Add(CreateCustomAttributeBuilder());
                return builders;
            });
        }

        CustomAttributeBuilder CreateCustomAttributeBuilder() => new(attr, constructorArgs, propertyInfos ?? [], propertyValues ?? []);
    }

    /// <summary>
    /// <para lang="zh">列创建回调方法 入口参数为 ITableColumn 实例 返回值为 CustomAttributeBuilder 集合</para>
    /// <para lang="en">列创建callback method 入口参数为 ITableColumn instance 返回值为 CustomAttributeBuilder collection</para>
    /// </summary>
    protected internal virtual IEnumerable<CustomAttributeBuilder> OnColumnCreating(ITableColumn col) => CustomerAttributeBuilderCache.TryGetValue(col.GetFieldName(), out var builders)
        ? builders
        : Enumerable.Empty<CustomAttributeBuilder>();

    /// <summary>
    /// <para lang="zh">动态类型新建回调委托</para>
    /// <para lang="en">动态type新建回调delegate</para>
    /// </summary>
    /// <param name="selectedItems"><para lang="zh">当前选中行</para><para lang="en">当前选中行</para></param>
    /// <returns></returns>
    public abstract Task AddAsync(IEnumerable<IDynamicObject> selectedItems);

    /// <summary>
    /// <para lang="zh">动态类型删除回调委托</para>
    /// <para lang="en">动态type删除回调delegate</para>
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public abstract Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

    /// <summary>
    /// <para lang="zh">动态类型集合变化时回调方法</para>
    /// <para lang="en">动态typecollection变化时callback method</para>
    /// </summary>
    /// <returns></returns>
    public Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">动态类型属性值变化时回调方法</para>
    /// <para lang="en">动态typeproperty值变化时callback method</para>
    /// </summary>
    /// <returns></returns>
    public Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得选中行比对回调方法</para>
    /// <para lang="en">Gets选中行比对callback method</para>
    /// </summary>
    public Func<IDynamicObject?, IDynamicObject?, bool>? EqualityComparer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<QueryPageOptions, IEnumerable<IDynamicObject>, IEnumerable<IDynamicObject>>? OnFilterCallback { get; set; }
}
