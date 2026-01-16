// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">动态类型上下文接口</para>
///  <para lang="en">动态type上下文接口</para>
/// </summary>
public interface IDynamicObjectContext
{
    /// <summary>
    ///  <para lang="zh">获取动态类型各列信息</para>
    ///  <para lang="en">获取动态type各列信息</para>
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITableColumn> GetColumns();

    /// <summary>
    ///  <para lang="zh">获得动态数据方法</para>
    ///  <para lang="en">Gets动态data方法</para>
    /// </summary>
    /// <returns></returns>
    IEnumerable<IDynamicObject> GetItems();

    /// <summary>
    ///  <para lang="zh">新建方法</para>
    ///  <para lang="en">新建方法</para>
    /// </summary>
    /// <param name="selectedItems"><para lang="zh">当前选中行</para><para lang="en">当前选中行</para></param>
    /// <returns></returns>
    Task AddAsync(IEnumerable<IDynamicObject> selectedItems);

    /// <summary>
    ///  <para lang="zh">删除方法</para>
    ///  <para lang="en">删除方法</para>
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(IEnumerable<IDynamicObject> items);

    /// <summary>
    ///  <para lang="zh">获得/设置 动态类型属性值变化时回调方法 默认为 null</para>
    ///  <para lang="en">Gets or sets 动态typeproperty值变化时callback method Default is为 null</para>
    /// </summary>
    /// <returns></returns>
    Func<IDynamicObject, ITableColumn, object?, Task>? OnValueChanged { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 动态类型数据集过滤回调委托 默认为 null</para>
    ///  <para lang="en">Gets or sets 动态typedata集过滤回调delegate Default is为 null</para>
    /// </summary>
    Func<QueryPageOptions, IEnumerable<IDynamicObject>, IEnumerable<IDynamicObject>>? OnFilterCallback { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 动态类型集合变化时回调方法 默认为 null</para>
    ///  <para lang="en">Gets or sets 动态typecollection变化时callback method Default is为 null</para>
    /// </summary>
    Func<DynamicObjectContextArgs, Task>? OnChanged { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 选中行是否相等判断逻辑 默认为 null</para>
    ///  <para lang="en">Gets or sets 选中行whether相等判断逻辑 Default is为 null</para>
    /// </summary>
    Func<IDynamicObject?, IDynamicObject?, bool>? EqualityComparer { get; set; }
}
