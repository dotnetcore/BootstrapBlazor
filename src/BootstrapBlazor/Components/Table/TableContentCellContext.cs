// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

readonly record struct TableContentCellContext<TItem>
{
    public TItem Item { get; init; }

    public int ColSpan { get; init; }

    public ITableColumn Col { get; init; }

    public string? CellClass { get; init; }

    public bool HasTreeChildren { get; init; }

    public bool IsInCell { get; init; }

    public int Degree { get; init; }

    public bool IsExpand { get; init; }

    public bool IsFirstColOfTree { get; init; }

    public RenderFragment? ValueTemplate { get; init; }

    public string? Value { get; init; }
}
