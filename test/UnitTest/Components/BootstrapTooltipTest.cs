// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class BootstrapTooltipTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Title_Ok()
    {
        var cut = Context.RenderComponent<BootstrapTooltip>(pb =>
        {
            pb.Add(a => a.Title, "test-title");
            pb.Add(a => a.IsHtml, false);
            pb.Add(a => a.Placement, Placement.Top);
            pb.Add(a => a.CustomClass, "test-class-custom");
            pb.Add(a => a.Trigger, "focus");
            pb.AddChildContent(a => a.AddContent(0, "Test"));
        });
        cut.Contains("<span class=\"d-inline-block\" tabindex=\"0\"");
    }

    [Fact]
    public void Title_Null()
    {
        var cut = Context.RenderComponent<BootstrapTooltip>();
        cut.Contains("<span class=\"d-inline-block\" tabindex=\"0\"");
    }
}
