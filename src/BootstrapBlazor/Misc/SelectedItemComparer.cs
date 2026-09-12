// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

#if NET11_0_OR_GREATER
internal sealed class SelectedItemComparer : IEqualityComparer<SelectedItem>
{
    public bool Equals(SelectedItem? x, SelectedItem? y) => ReferenceEquals(x, y) || (x is not null && y is not null && x.Value == y.Value);

    public int GetHashCode(SelectedItem obj) => obj.Value.GetHashCode();
}

internal sealed class SelectedItemComparer<TValue>(IModelEqualityComparer<TValue> comparer) : IEqualityComparer<SelectedItem<TValue>>
{
    private readonly ModelHashSetComparer<TValue> _comparer = new(comparer);

    public bool Equals(SelectedItem<TValue>? x, SelectedItem<TValue>? y) => ReferenceEquals(x, y) || (x is not null && y is not null && _comparer.Equals(x.Value, y.Value));

    public int GetHashCode(SelectedItem<TValue> obj) => obj.Value is null ? 0 : _comparer.GetHashCode(obj.Value);
}
#endif
