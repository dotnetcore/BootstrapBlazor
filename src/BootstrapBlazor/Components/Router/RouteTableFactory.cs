// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    internal static class RouteTableFactory
    {
        [NotNull]
        private static Routing.IRouteTable? Routes { get; set; }
        private static readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assemblies"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static RouteContext Create(IEnumerable<Assembly> assemblies, string url)
        {
            RefreshRouteTable(assemblies);

            var routeContext = new Routing.RouteContext(url);
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
                Routes = Routing.RouteTableFactory.Create(assemblies);
                _assemblies.Clear();
                _assemblies.UnionWith(assembliesSet);
            }
        }

#if SSR
        #region SSR 模式可用
        static readonly char[] _queryOrHashStartChar = new[] { '?', '#' };

        public static RouteContext Create(IEnumerable<Assembly> assemblies, string url)
        {
            var routerAssembly = typeof(Microsoft.AspNetCore.Components.Routing.Router).Assembly;
            var routerTableFactoryType = routerAssembly.GetType("Microsoft.AspNetCore.Components.RouteTableFactory");

            // call RouteTableFactory.Create()
            var createMethodInfo = routerTableFactoryType?.GetMethod("Create");
            var routeTableInstance = createMethodInfo?.Invoke(null, new object[] { assemblies });

            var locationPath = url;
            locationPath = StringUntilAny(locationPath, _queryOrHashStartChar);

            // new RouteContext
            var contextType = routerAssembly.GetType("Microsoft.AspNetCore.Components.Routing.RouteContext");
            var context = Activator.CreateInstance(contextType!, new object[] { locationPath });

            // Call RouteTable.Route(RouteContext)
            var routeMethodInfo = routeTableInstance?.GetType().GetMethod("Route");
            routeMethodInfo!.Invoke(routeTableInstance, new object[] { context! });

            // Handler
            var segments = contextType?.GetProperty("Segments")?.GetValue(context) as string[];
            var handler = contextType?.GetProperty("Handler")?.GetValue(context) as Type;
            var parameters = contextType?.GetProperty("Parameters")?.GetValue(context) as IReadOnlyDictionary<string, object>;

            return new RouteContext() { Handler = handler, Parameters = parameters, Segments = segments };
        }

        private static string StringUntilAny(string str, char[] chars)
        {
            var firstIndex = str.IndexOfAny(chars);
            return firstIndex < 0
                ? str
                : str.Substring(0, firstIndex);
        }
        #endregion
#endif
    }
}
