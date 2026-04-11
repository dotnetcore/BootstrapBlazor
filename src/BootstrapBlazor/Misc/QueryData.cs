// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">表格查询数据类</para>
/// <para lang="en">Table query data class</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class QueryData<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 要显示页码的数据集合</para>
    /// <para lang="en">Gets or sets data collection to display page number</para>
    /// </summary>
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据集合总数</para>
    /// <para lang="en">Gets or sets total data count</para>
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据是否被过滤 默认为 false 未被过滤，设置为 true 表示数据已过滤组件内部不做任何处理，设置为 false 表示数据未过滤组件内部将进行过滤操作</para>
    /// <para lang="en">Gets or sets whether data is filtered. Default is false. Setting to true indicates data is already filtered and the component will not perform any filtering. Setting to false indicates data is not filtered and the component will perform filtering.</para>
    /// </summary>
    public bool IsFiltered { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据是否被排序 默认为 false 未被排序，设置为 true 表示数据已排序组件内部不做任何处理，设置为 false 表示数据未排序组件内部将进行排序操作</para>
    /// <para lang="en">Gets or sets whether data is sorted. Default is false. Setting to true indicates data is already sorted and the component will not perform any sorting. Setting to false indicates data is not sorted and the component will perform sorting.</para>
    /// </summary>
    public bool IsSorted { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据是否被搜索 默认为 false 未被搜索，设置为 true 表示数据已搜索组件内部不做任何处理，设置为 false 表示数据未搜索组件内部将进行搜索操作</para>
    /// <para lang="en">Gets or sets whether data is searched. Default is false. Setting to true indicates data is already searched and the component will not perform any searching. Setting to false indicates data is not searched and the component will perform searching.</para>
    /// </summary>
    public bool IsSearch { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数据是否为已处理自定义高级搜索 <see cref="Table{TItem}.CustomerSearchTemplate"/> 默认为 false</para>
    /// <para lang="en">Gets or sets whether data has processed custom advanced search <see cref="Table{TItem}.CustomerSearchTemplate"/> default false</para>
    /// </summary>
    /// <remarks>
    ///   <para lang="zh">设置本属性为 true 时，Table 组件的高级搜索按钮改变颜色</para>
    ///   <para lang="en">When this property is set to true, the advanced search button of the Table component changes color</para>
    ///</remarks>
    public bool IsAdvanceSearch { get; set; }
}
