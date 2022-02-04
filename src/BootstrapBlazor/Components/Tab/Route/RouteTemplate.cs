// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/


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

#if NET5_0
        OptionalSegmentsCount = segments.Count(template => template.IsOptional);
        ContainsCatchAllSegment = segments.Any(template => template.IsCatchAll);
#else
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
#endif
    }

    public string TemplateText { get; }

    public TemplateSegment[] Segments { get; }

    public int OptionalSegmentsCount { get; }

    public bool ContainsCatchAllSegment { get; }
}
