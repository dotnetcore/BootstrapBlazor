// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SplitTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SplitStyle_Ok()
    {
        var cut = Context.RenderComponent<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, BuildeComponent("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, BuildeComponent("I am Pane2"));
            pb.Add(b => b.IsVertical, true);
        });
        Assert.Contains("I am Pane1", cut.Markup);
        Assert.Contains("I am Pane2", cut.Markup);
        Assert.DoesNotContain("is-horizontal", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.Basis, "90%");
        });
        Assert.Contains("90%", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.AdditionalAttributes, new Dictionary<string, object>() { ["tag"] = "tagok" });
        });
        Assert.Contains("tagok", cut.Markup);

        RenderFragment BuildeComponent(string name = "I am Pane1") => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, name);
            builder.CloseElement();
        };
    }
}
