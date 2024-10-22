﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace Microsoft.AspNetCore.Components.Routing;

[ExcludeFromCodeCoverage]
internal static class RouteConstraint
{
    public static UrlValueConstraint Parse(string template, string segment, string constraint)
    {
        if (string.IsNullOrEmpty(constraint))
        {
            throw new ArgumentException($"Malformed segment '{segment}' in route '{template}' contains an empty constraint.");
        }

        var targetType = GetTargetType(constraint);
        if (targetType is null || !UrlValueConstraint.TryGetByTargetType(targetType, out var result))
        {
            throw new ArgumentException($"Unsupported constraint '{constraint}' in route '{template}'.");
        }

        return result;
    }

    private static Type? GetTargetType(string constraint) => constraint switch
    {
        "bool" => typeof(bool),
        "datetime" => typeof(DateTime),
        "decimal" => typeof(decimal),
        "double" => typeof(double),
        "float" => typeof(float),
        "guid" => typeof(Guid),
        "int" => typeof(int),
        "long" => typeof(long),
        _ => null,
    };
}
