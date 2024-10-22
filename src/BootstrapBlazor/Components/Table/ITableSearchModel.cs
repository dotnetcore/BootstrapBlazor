// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件自定义搜索模型接口定义
/// </summary>
public interface ITableSearchModel
{
    /// <summary>
    /// 获得 搜索集合
    /// </summary>
    [Obsolete("This method is obsolete. Use GetSearches instead. 已过期，请使用 GetSearches 方法")]
    [ExcludeFromCodeCoverage]
    IEnumerable<IFilterAction> GetSearchs() => GetSearches();

    /// <summary>
    /// 获得 搜索集合
    /// </summary>
    IEnumerable<IFilterAction> GetSearches();

    /// <summary>
    /// 重置操作
    /// </summary>
    void Reset();
}
