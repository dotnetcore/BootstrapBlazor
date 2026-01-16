// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Diagnostics;

namespace Microsoft.AspNetCore.Components.Routing;

[DebuggerDisplay("{TemplateText}")]
[ExcludeFromCodeCoverage]
internal class RouteTemplate
{
    public RouteTemplate(string templateText, TemplateSegment[] segments)
    {
        TemplateText = templateText;
        Segments = segments;

        for (var i = 0; i < segments.Length; i++)
        {
            var segment = segments[i];
            if (segment.IsOptional)
            {
                OptionalSegmentsCount++;
            }
            if (segment.IsCatchAll)
            {
                ContainsCatchAllSegment = true;
            }
        }
    }

    public string TemplateText { get; }

    public TemplateSegment[] Segments { get; }

    public int OptionalSegmentsCount { get; }

    public bool ContainsCatchAllSegment { get; }
}
