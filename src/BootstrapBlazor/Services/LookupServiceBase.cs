// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// LookupService 基类
/// </summary>
public abstract class LookupServiceBase : ILookupService
{
    /// <summary>
    ///<inheritdoc/>
    /// </summary>
    public virtual IEnumerable<SelectedItem>? GetItemsByKey(string? key) => GetItemsByKey(key, null);

    /// <summary>
    ///<inheritdoc/>
    /// </summary>
    public abstract IEnumerable<SelectedItem>? GetItemsByKey(string? key, object? data);
}
