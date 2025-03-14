// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TreeViewToolbarContext class
/// </summary>
/// <param name="from"></param>
/// <param name="to"></param>
public class TreeViewToolbarContext<TItem>(TItem from, TItem to)
{
    /// <summary>
    /// Gets or sets the origin tree view item
    /// </summary>
    public TItem? From { get; set; } = from;

    /// <summary>
    /// Gets or sets the new tree view item
    /// </summary>
    public TItem? To { get; set; } = to;
}
