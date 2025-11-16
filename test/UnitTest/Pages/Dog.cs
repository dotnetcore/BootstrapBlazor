// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Pages;

[Route("/Dog")]
[Authorize]
[TabItemOption(Icon = "fa-solid fa-font-awesome", Closable = false, Text = "Dog")]
public class Dog : ComponentBase
{
    [Parameter]
    public string? Parameter1 { get; set; }

    [Parameter]
    [SupplyParameterFromQuery]
    public string? Class { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", Class);
        builder.AddContent(2, Parameter1);
        builder.CloseElement();
    }
}
