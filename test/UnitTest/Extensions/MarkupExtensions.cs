// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp;
using AngleSharp.Html;

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

    public static string ToMarkup(this IMarkupFormattable markupFormattable)
    {
        if (markupFormattable is null)
            throw new ArgumentNullException(nameof(markupFormattable));

        using var sw = new StringWriter();
        var formatter = new PrettyMarkupFormatter
        {
            NewLine = Environment.NewLine,
            Indentation = "  ",
        };
        markupFormattable.ToHtml(sw, formatter);
        return sw.ToString();
    }
}
