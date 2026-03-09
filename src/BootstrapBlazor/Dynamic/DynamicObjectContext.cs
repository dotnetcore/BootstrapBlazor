// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="IDynamicObjectContext"/> 实现类</para>
/// <para lang="en"><see cref="IDynamicObjectContext"/> implementation class</para>
/// </summary>
public abstract class DynamicObjectContext : IDynamicObjectContext
{
    /// <summary>
    /// <para lang="zh">获取动态类型各列信息</para>
    /// <para lang="en">Gets the column information of the dynamic type</para>
    /// </summary>
    public abstract IEnumerable<ITableColumn> GetColumns();

    /// <summary>
    /// <para lang="zh">获得动态类数据方法</para>
    /// <para lang="en">Gets the data of the dynamic type</para>
    /// </summary>
    public abstract IEnumerable<IDynamicObject> GetItems();

    /// <summary>
    /// <para lang="zh">获取自定义属性构建器缓存</para>
    /// <para lang="en">Gets the custom attribute builder cache</para>
    /// </summary>
    protected ConcurrentDictionary<string, List<CustomAttributeBuilder>> CustomerAttributeBuilderCache { get; } = new();

    /// <summary>
    /// <para lang="zh">添加标签到指定列</para>
    /// <para lang="en">Adds an attribute to the specified column</para>
    /// </summary>
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
    /// <para lang="en">Column creation callback method. The input parameter is an ITableColumn instance, and the return value is a collection of CustomAttributeBuilder.</para>
    /// </summary>
    protected internal virtual IEnumerable<CustomAttributeBuilder> OnColumnCreating(ITableColumn col) => CustomerAttributeBuilderCache.TryGetValue(col.GetFieldName(), out var builders)
        ? builders
        : Enumerable.Empty<CustomAttributeBuilder>();

    /// <summary>
    /// <para lang="zh">动态类型新建回调委托</para>
    /// <para lang="en">Dynamic type creation callback delegate</para>
    /// </summary>
    /// <param name="selectedItems"><para lang="zh">当前选中行</para><para lang="en">Currently selected rows</para></param>
    public abstract Task AddAsync(IEnumerable<IDynamicObject> selectedItems);

    /// <summary>
    /// <para lang="zh">动态类型删除回调委托</para>
    /// <para lang="en">Dynamic type deletion callback delegate</para>
    /// </summary>
    /// <param name="items"><para lang="zh">要删除的行</para><para lang="en">Rows to be deleted</para></param>
    public abstract Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

    /// <summary>
    /// <para lang="zh">动态类型集合变化时回调方法</para>
    /// <para lang="en">Dynamic type collection change callback method</para>
    /// </summary>
    public Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// <para lang="zh">动态类型属性值变化时回调方法</para>
    /// <para lang="en">Dynamic type property value change callback method</para>
    /// </summary>
    public Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }

    /// <summary>
    /// <para lang="zh">获得选中行比对回调方法</para>
    /// <para lang="en">Gets the selected row comparison callback method</para>
    /// </summary>
    public Func<IDynamicObject?, IDynamicObject?, bool>? EqualityComparer { get; set; }

    /// <summary>
    /// <para lang="zh">过滤回调方法</para>
    /// <para lang="en">Filter callback method</para>
    /// </summary>
    public Func<QueryPageOptions, IEnumerable<IDynamicObject>, IEnumerable<IDynamicObject>>? OnFilterCallback { get; set; }
}
