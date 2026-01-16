// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Authorization;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
internal static class AttributeAuthorizeDataCache
{
    private static readonly ConcurrentDictionary<Type, IAuthorizeData[]?> _cache = new();

    public static IAuthorizeData[]? GetAuthorizeDataForType(Type type)
    {
        if (!_cache.TryGetValue(type, out var result))
        {
            result = ComputeAuthorizeDataForType(type);
            _cache[type] = result; // Safe race - doesn't matter if it overwrites
        }

        return result;
    }

    private static IAuthorizeData[]? ComputeAuthorizeDataForType(Type type)
    {
        // Allow Anonymous skips all authorization
        var allAttributes = type.GetCustomAttributes(inherit: true);
        if (allAttributes.OfType<IAllowAnonymous>().Any())
        {
            return null;
        }

        var authorizeDataAttributes = allAttributes.OfType<IAuthorizeData>().ToArray();
        return authorizeDataAttributes.Length > 0 ? authorizeDataAttributes : null;
    }
}
