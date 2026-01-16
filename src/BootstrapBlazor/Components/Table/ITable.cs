// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ITable 接口</para>
///  <para lang="en">ITable 接口</para>
/// </summary>
public interface ITable : IColumnCollection
{
    /// <summary>
    ///  <para lang="zh">获得 ITable 实例配置的可见列集合</para>
    ///  <para lang="en">Gets ITable instance配置的可见列collection</para>
    /// </summary>
    IEnumerable<ITableColumn> GetVisibleColumns();

    /// <summary>
    ///  <para lang="zh">获得 过滤条件集合</para>
    ///  <para lang="en">Gets 过滤条件collection</para>
    /// </summary>
    Dictionary<string, IFilterAction> Filters { get; }

    /// <summary>
    ///  <para lang="zh">获得 过滤异步回调方法</para>
    ///  <para lang="en">Gets 过滤异步callback method</para>
    /// </summary>
    Func<Task>? OnFilterAsync { get; }
}
