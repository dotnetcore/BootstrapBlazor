// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ILookupService 接口</para>
/// <para lang="en">ILookupService Interface</para>
/// </summary>
public interface ILookupService
{
    /// <summary>
    /// <para lang="zh">根据指定键值获取 Lookup 集合方法</para>
    /// <para lang="en">Get Lookup Collection by Key Method</para>
    /// </summary>
    /// <param name="key"><para lang="zh">获得 Lookup 数据集合键值</para><para lang="en">Lookup Data Key</para></param>
    [Obsolete("已弃用，请使用 data 参数重载方法；Deprecated, please use the data parameter method")]
    [ExcludeFromCodeCoverage]
    IEnumerable<SelectedItem>? GetItemsByKey(string? key);

    /// <summary>
    /// <para lang="zh">根据指定键值获取 Lookup 集合方法</para>
    /// <para lang="en">Get Lookup Collection by Key Method</para>
    /// </summary>
    /// <param name="key"><para lang="zh">获得 Lookup 数据集合键值</para><para lang="en">Lookup Data Key</para></param>
    /// <param name="data"><para lang="zh">Lookup 键值附加数据</para><para lang="en">Lookup Key Additional Data</para></param>
    IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data);

    /// <summary>
    /// <para lang="zh">根据指定键值获取 Lookup 集合异步方法</para>
    /// <para lang="en">Get Lookup Collection by Key Async Method</para>
    /// </summary>
    /// <param name="key"><para lang="zh">获得 Lookup 数据集合键值</para><para lang="en">Lookup Data Key</para></param>
    /// <param name="data"><para lang="zh">Lookup 键值附加数据</para><para lang="en">Lookup Key Additional Data</para></param>
    Task<IEnumerable<SelectedItem>?> GetItemsByKeyAsync(string? key, object? data);
}
