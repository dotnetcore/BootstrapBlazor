// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Pages;

[Route("/route-test/{id:int}")]
public class ConstrainedRoute : ComponentBase
{
    [Parameter]
    public int Id { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, $"int:{Id}");
}

[Route("/route-test/{value}")]
public class ParameterRoute : ComponentBase
{
    [Parameter]
    public string? Value { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, $"string:{Value}");
}

[Route("/optional/{value?}")]
public class OptionalRoute : ComponentBase
{
    [Parameter]
    public string? Value { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, $"optional:{Value ?? "null"}");
}

[Route("/catch-all/{*path}")]
public class CatchAllRoute : ComponentBase
{
    [Parameter]
    public string? Path { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, $"catch:{Path}");
}

[Route("/multiple")]
[Route("/multiple/{id:int}")]
public class MultipleRoute : ComponentBase
{
    [Parameter]
    public int? Id { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder) => builder.AddContent(0, $"multiple:{Id?.ToString() ?? "null"}");
}
