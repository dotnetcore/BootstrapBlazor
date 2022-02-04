// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace Microsoft.AspNetCore.Components.Routing;

[ExcludeFromCodeCoverage]
#if NET5_0
internal class RouteTable : IRouteTable
#else
internal class RouteTable
#endif
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
