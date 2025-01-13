// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ILookupService 接口
/// </summary>
public interface ILookupService
{
    /// <summary>
    /// 根据指定键值获取 Lookup 集合方法
    /// </summary>
    /// <param name="key">获得 Lookup 数据集合键值</param>
    [Obsolete("已弃用，请使用 data 参数重载方法；Deprecated, please use the data parameter method")]
    [ExcludeFromCodeCoverage]
    IEnumerable<SelectedItem>? GetItemsByKey(string? key);

    /// <summary>
    /// 根据指定键值获取 Lookup 集合方法
    /// </summary>
    /// <param name="key">获得 Lookup 数据集合键值</param>
    /// <param name="data">Lookup 键值附加数据</param>
    IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data);

    /// <summary>
    /// 根据指定键值获取 Lookup 集合异步方法
    /// </summary>
    /// <param name="key">获得 Lookup 数据集合键值</param>
    /// <param name="data">Lookup 键值附加数据</param>
    Task<IEnumerable<SelectedItem>?> GetItemsByKeyAsync(string? key, object? data);
}
