// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

internal class InternalSelectObjectContext<TItem> : ISelectObjectContext<TItem>
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [NotNull]
    public SelectObject<TItem>? Component { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task CloseAsync() => Component.CloseAsync();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    public void SetValue(TItem value) => Component.SetValue(value);
}
