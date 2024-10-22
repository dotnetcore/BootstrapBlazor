// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格查询数据类
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class QueryData<TItem>
{
    /// <summary>
    /// 获得/设置 要显示页码的数据集合
    /// </summary>
    public IEnumerable<TItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 数据集合总数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 获得/设置 数据是否被过滤 默认为 false 未被过滤
    /// </summary>
    /// <remarks>组件内部通过此属性判断，如果外部未进行数据过滤，内部将进行数据过滤操作</remarks>
    public bool IsFiltered { get; set; }

    /// <summary>
    /// 获得/设置 数据是否被排序 默认为 false 未被排序
    /// </summary>
    public bool IsSorted { get; set; }

    /// <summary>
    /// 获得/设置 数据是否已处理 SearchText <see cref="Table{TItem}.SearchText"/> 默认为 false
    /// </summary>
    /// <remarks>设置本属性为 true 时，Table 组件的高级搜索按钮改变颜色</remarks>
    public bool IsSearch { get; set; }

    /// <summary>
    /// 获得/设置 数据是否为已处理自定义高级搜索 <see cref="Table{TItem}.CustomerSearchTemplate"/> 默认为 false
    /// </summary>
    /// <remarks>设置本属性为 true 时，Table 组件的高级搜索按钮改变颜色</remarks>
    public bool IsAdvanceSearch { get; set; }
}
