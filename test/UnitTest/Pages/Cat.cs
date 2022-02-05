// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Pages;

[Route("/Cat")]
[TabItemOption(Icon = "fa fa-fa", Closable = true, Text = "Cat")]
public class Cat : ComponentBase
{
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(0, "Cat");
    }
}
