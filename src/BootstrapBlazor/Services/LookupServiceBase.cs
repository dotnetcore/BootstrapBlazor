// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">LookupService 基类</para>
/// <para lang="en">LookupService Base Class</para>
/// </summary>
public abstract class LookupServiceBase : ILookupService
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Obsolete("已弃用，请使用 data 参数重载方法；Deprecated, please use the data parameter method")]
    [ExcludeFromCodeCoverage]
    public virtual IEnumerable<SelectedItem>? GetItemsByKey(string? key) => GetItemsByKey(key, null);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public abstract IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual Task<IEnumerable<SelectedItem>?> GetItemsByKeyAsync(string? key, object? data) => Task.FromResult(GetItemsByKey(key, data));
}
