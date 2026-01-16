// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace Microsoft.AspNetCore.Components.Routing;

[ExcludeFromCodeCoverage]
internal class RouteContext
{
    private static readonly char[] Separator = new[] { '/' };

    public RouteContext(string path)
    {
        // This is a simplification. We are assuming there are no paths like /a//b/. A proper routing
        // implementation would be more sophisticated.
        Segments = path.Trim('/').Split(Separator, StringSplitOptions.RemoveEmptyEntries);
        // Individual segments are URL-decoded in order to support arbitrary characters, assuming UTF-8 encoding.
        for (int i = 0; i < Segments.Length; i++)
        {
            Segments[i] = Uri.UnescapeDataString(Segments[i]);
        }
    }

    public string[] Segments { get; }

    [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
    public Type? Handler { get; set; }

    public IReadOnlyDictionary<string, object>? Parameters { get; set; }
}
