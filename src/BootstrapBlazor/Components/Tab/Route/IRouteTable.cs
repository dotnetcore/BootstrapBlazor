// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace Microsoft.AspNetCore.Components.Routing;

#if NET5_0
/// <summary>
/// Provides an abstraction over <see cref="RouteTable"/>.
/// the legacy route matching logic is removed.
/// </summary>
internal interface IRouteTable
{
    void Route(RouteContext routeContext);
}
#endif
