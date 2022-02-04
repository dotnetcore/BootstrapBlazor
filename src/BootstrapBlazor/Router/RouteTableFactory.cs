// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
#if NET5_0
internal static class RouteTableFactory
{
    [NotNull]
    private static Microsoft.AspNetCore.Components.Routing.IRouteTable? Routes { get; set; }
    private static readonly HashSet<Assembly> _assemblies = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static RouteContext Create(IEnumerable<Assembly>? assemblies, string url)
    {
        RefreshRouteTable(assemblies);
        if (url.IndexOf("?") > 0) url = url[..url.IndexOf("?")];
        var routeContext = new Microsoft.AspNetCore.Components.Routing.RouteContext(url);
        Routes.Route(routeContext);
        return new RouteContext()
        {
            Handler = routeContext.Handler,
            Parameters = routeContext.Parameters,
            Segments = routeContext.Segments
        };
    }

    private static void RefreshRouteTable(IEnumerable<Assembly>? assemblies)
    {
        assemblies ??= Enumerable.Empty<Assembly>();
        var assembliesSet = new HashSet<Assembly>(assemblies);
        if (!_assemblies.SetEquals(assembliesSet))
        {
            Routes = Microsoft.AspNetCore.Components.Routing.RouteTableFactory.Create(assemblies);
            _assemblies.Clear();
            _assemblies.UnionWith(assembliesSet);
        }
    }
}
#else
internal static class RouteTableFactory
{
    [NotNull]
    private static Microsoft.AspNetCore.Components.Routing.RouteTable? Routes { get; set; }
    private static readonly HashSet<Assembly> _assemblies = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="assemblies"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public static RouteContext Create(IEnumerable<Assembly> assemblies, string url)
    {
        RefreshRouteTable(assemblies);
        if (url.IndexOf("?") > 0) url = url[..url.IndexOf("?")];
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
#endif
