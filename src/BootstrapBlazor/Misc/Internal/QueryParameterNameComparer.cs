// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET6_0_OR_GREATER
namespace Microsoft.AspNetCore.Components.Routing;

[ExcludeFromCodeCoverage]
internal sealed class QueryParameterNameComparer : IComparer<ReadOnlyMemory<char>>, IEqualityComparer<ReadOnlyMemory<char>>
{
    public static readonly QueryParameterNameComparer Instance = new();

    public int Compare(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        => x.Span.CompareTo(y.Span, StringComparison.OrdinalIgnoreCase);

    public bool Equals(ReadOnlyMemory<char> x, ReadOnlyMemory<char> y)
        => x.Span.Equals(y.Span, StringComparison.OrdinalIgnoreCase);

    public int GetHashCode([DisallowNull] ReadOnlyMemory<char> obj)
        => string.GetHashCode(obj.Span, StringComparison.OrdinalIgnoreCase);
}
#endif
