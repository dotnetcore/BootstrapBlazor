// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TooltipTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Content_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Content, "test_tooltip");
            pb.Add(a => a.Trigger, "test_trigger");
        });
    }

    [Fact]
    public void CssClass_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.CssClass, "test-custom-class");
        });
    }
}
