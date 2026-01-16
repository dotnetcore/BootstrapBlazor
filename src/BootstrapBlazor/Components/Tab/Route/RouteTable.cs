// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace Microsoft.AspNetCore.Components.Routing;

[ExcludeFromCodeCoverage]
internal class RouteTable
{
    public RouteTable(RouteEntry[] routes)
    {
        Routes = routes;
    }

    public RouteEntry[] Routes { get; }

    public void Route(RouteContext routeContext)
    {
        for (var i = 0; i < Routes.Length; i++)
        {
            Routes[i].Match(routeContext);
            if (routeContext.Handler != null)
            {
                return;
            }
        }
    }
}
