// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件自定义搜索模型接口定义</para>
/// <para lang="en">Table component custom search model interface definition</para>
/// </summary>
public interface ITableSearchModel
{
    /// <summary>
    /// <para lang="zh">获得 搜索集合</para>
    /// <para lang="en">Gets search collection</para>
    /// </summary>
    [Obsolete("This method is obsolete. Use GetSearches instead. 已过期，请使用 GetSearches 方法")]
    [ExcludeFromCodeCoverage]
    IEnumerable<IFilterAction> GetSearchs() => GetSearches();

    /// <summary>
    /// <para lang="zh">获得 搜索集合</para>
    /// <para lang="en">Gets search collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    IEnumerable<IFilterAction> GetSearches();

    /// <summary>
    /// <para lang="zh">重置操作</para>
    /// <para lang="en">Reset operation</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    void Reset();
}
