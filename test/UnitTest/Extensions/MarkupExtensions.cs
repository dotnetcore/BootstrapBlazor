// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

internal static class MarkupExtensions
{
    public static void Contains<TComponent>(this IRenderedComponent<TComponent> component, string expected) where TComponent : IComponent
    {
        Assert.Contains(expected, component.Markup);
    }

    public static void DoesNotContain<TComponent>(this IRenderedComponent<TComponent> component, string expected) where TComponent : IComponent
    {
        Assert.DoesNotContain(expected, component.Markup);
    }
}
