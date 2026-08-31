// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;
using System.Collections;

namespace UnitTest.Components;

public class RouteTableFactoryTest
{
    [Fact]
    public void CompareRoutes_ReturnsZeroForSameRoute()
    {
        var route = CreateRouteDefinition("/{id}");
        var method = GetFactoryType().GetMethod("CompareRoutes", BindingFlags.NonPublic | BindingFlags.Static)!;

        var result = method.Invoke(null, [route, route]);

        Assert.Equal(0, result);
    }

    [Fact]
    public void DetectAmbiguousRoutes_ThrowsForDifferentAmbiguousRoutes()
    {
        var routeDefinitionType = GetFactoryType().GetNestedType("RouteDefinition", BindingFlags.NonPublic)!;
        var routes = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(routeDefinitionType))!;
        routes.Add(CreateRouteDefinition("/{id}"));
        routes.Add(CreateRouteDefinition("/{name}"));
        var method = GetFactoryType().GetMethod("DetectAmbiguousRoutes", BindingFlags.NonPublic | BindingFlags.Static)!;

        var exception = Assert.Throws<TargetInvocationException>(() => method.Invoke(null, [routes]));

        var innerException = Assert.IsType<InvalidOperationException>(exception.InnerException);
        Assert.Contains("The following routes are ambiguous", innerException.Message);
    }

    private static object CreateRouteDefinition(string template)
    {
        var factoryType = GetFactoryType();
        var parsedTemplate = factoryType
            .GetMethod("ParseTemplate", BindingFlags.NonPublic | BindingFlags.Static)!
            .Invoke(null, [template])!;
        var segments = parsedTemplate.GetType().GetProperty("Segments")!.GetValue(parsedTemplate)!;
        var routeDefinitionType = factoryType.GetNestedType("RouteDefinition", BindingFlags.NonPublic)!;
        var constructor = routeDefinitionType.GetConstructors().Single();
        return constructor.Invoke([template, segments, typeof(ComponentBase), Array.Empty<string>()]);
    }

    private static Type GetFactoryType()
        => Type.GetType("BootstrapBlazor.Components.RouteTableFactory, BootstrapBlazor")!;
}
