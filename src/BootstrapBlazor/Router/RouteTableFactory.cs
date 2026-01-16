// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
internal static class RouteTableFactory
{
    [NotNull]
    private static Microsoft.AspNetCore.Components.Routing.RouteTable? Routes { get; set; }
    private static readonly HashSet<Assembly> _assemblies = new();

    /// <summary>
    /// <para lang="zh">///</para>
    /// <para lang="en">///</para>
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static RouteContext Create(IEnumerable<Assembly> assemblies, string url)
    {
        RefreshRouteTable(assemblies);
        var len = url.IndexOf("?", StringComparison.OrdinalIgnoreCase);
        if (len > 0) url = url[..len];
        var routeContext = new Microsoft.AspNetCore.Components.Routing.RouteContext(url);
        Routes.Route(routeContext);
        return new RouteContext()
        {
            Handler = routeContext.Handler,
            Parameters = routeContext.Parameters,
            Segments = routeContext.Segments
        };
    }

    private static void RefreshRouteTable(IEnumerable<Assembly> assemblies)
    {
        var assembliesSet = new HashSet<Assembly>(assemblies);
        if (!_assemblies.SetEquals(assembliesSet))
        {
            Routes = Microsoft.AspNetCore.Components.Routing.RouteTableFactory.Create(new Microsoft.AspNetCore.Components.Routing.RouteKey(null, assemblies));
            _assemblies.Clear();
            _assemblies.UnionWith(assembliesSet);
        }
    }
}
