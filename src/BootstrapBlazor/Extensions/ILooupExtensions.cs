// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="ILookup"/> 扩展方法</para>
/// <para lang="en"><see cref="ILookup"/> extension methods</para>
/// </summary>
public static class ILooupExtensions
{
    /// <summary>
    /// <para lang="zh">判断当前 <see cref="ILookup"/> 实例是否配置 Lookup 数据</para>
    /// <para lang="en">Determine whether the current <see cref="ILookup"/> instance is configured with Lookup data</para>
    /// </summary>
    /// <param name="lookup"></param>
    /// <returns></returns>
    public static bool IsLookup(this ILookup lookup) => lookup.Lookup != null || !string.IsNullOrEmpty(lookup.LookupServiceKey);

    /// <summary>
    /// <para lang="zh">获得 ILookupService 实例</para>
    /// <para lang="en">Get ILookupService instance</para>
    /// </summary>
    /// <param name="lookup"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static ILookupService GetLookupService(this ILookup lookup, ILookupService service) => lookup.LookupService ?? service;

    /// <summary>
    /// <para lang="zh">///</para>
    /// <para lang="en">///</para>
    /// </summary>
    /// <param name="lookup"></param>
    /// <param name="service"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<IEnumerable<SelectedItem>?> GetItemsAsync(this ILookup lookup, ILookupService service, string? key, object? data) => lookup.Lookup ?? await lookup.GetLookupService(service).GetItemsAsync(key, data);

    /// <summary>
    /// <para lang="zh">根据指定键值获取 Lookup 集合扩展方法，先调用同步方法，如果返回 null 则调用异步方法</para>
    /// <para lang="en">Get Lookup collection extension method based on specified key value, call synchronous method first, if return null then call asynchronous method</para>
    /// </summary>
    /// <param name="service"></param>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task<IEnumerable<SelectedItem>?> GetItemsAsync(this ILookupService service, string? key, object? data) => string.IsNullOrEmpty(key)
        ? null
        : service.GetItemsByKey(key, data) ?? await service.GetItemsByKeyAsync(key, data);
}
