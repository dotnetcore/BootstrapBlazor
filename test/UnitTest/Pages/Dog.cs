// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
