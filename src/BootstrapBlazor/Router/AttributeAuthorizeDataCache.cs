// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
