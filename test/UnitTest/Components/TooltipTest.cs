// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TooltipTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Title_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
        });
        Assert.Equal("test_tooltip", cut.Instance.Title);
        Assert.Contains("data-bs-toggle=\"tooltip\"", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
            pb.Add(a => a.ChildContent, builder => builder.AddContent(0, "test-childcontent"));
        });
        Assert.Contains("test-childcontent", cut.Markup);
    }

    [Fact]
    public void SetParameters_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
        });
        var tooltip = cut.Instance;
        cut.InvokeAsync(() => tooltip.SetParameters("title", Placement.Top, "trigger", "custom-class", true, false, "10", ".selector"));
        Assert.Equal("title", tooltip.Title);
        Assert.Contains("data-bs-placement=\"top\"", cut.Markup);
        Assert.Contains("data-bs-trigger=\"trigger\"", cut.Markup);
        Assert.Contains("data-bs-custom-class=\"custom-class\"", cut.Markup);
        Assert.Contains("data-bs-html=\"true\"", cut.Markup);
        Assert.Contains("data-bs-sanitize=\"false\"", cut.Markup);
        Assert.Contains("data-bs-delay=\"10\"", cut.Markup);
        Assert.Contains("data-bs-selector=\".selector\"", cut.Markup);
    }

    [Fact]
    public void Sanitize_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
        });
        Assert.DoesNotContain("data-bs-sanitize", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Sanitize, false);
        });
        Assert.Contains("data-bs-sanitize=\"false\"", cut.Markup);
    }

    [Fact]
    public void Delay_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
        });
        Assert.DoesNotContain("data-bs-delay", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Delay, "test-delay");
        });
        Assert.Contains("data-bs-delay=\"test-delay\"", cut.Markup);
    }

    [Fact]
    public void Html_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
        });
        Assert.DoesNotContain("data-bs-html", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsHtml, true);
        });
        Assert.Contains("data-bs-html=\"true\"", cut.Markup);
    }

    [Fact]
    public void Trigger_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
        });
        Assert.Contains("data-bs-trigger=\"focus hover\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Trigger, "test_trigger");
        });
        Assert.Contains("data-bs-trigger=\"test_trigger\"", cut.Markup);
    }

    [Fact]
    public void CustomClass_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
            pb.Add(a => a.CustomClass, null);
        });
        Assert.DoesNotContain("data-bs-custom-class", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.CustomClass, "test-custom-class");
        });
        Assert.Contains("data-bs-custom-class=\"test-custom-class\"", cut.Markup);
    }

    [Fact]
    public void Selector_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
            pb.Add(a => a.Selector, null);
        });
        Assert.DoesNotContain("data-bs-selector", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Selector, "test-selector");
        });
        Assert.Contains("data-bs-selector=\"test-selector\"", cut.Markup);
    }

    [Fact]
    public void Placement_Ok()
    {
        var cut = Context.RenderComponent<Tooltip>(pb =>
        {
            pb.Add(a => a.Title, "test_tooltip");
            pb.Add(a => a.Placement, Placement.Auto);
            pb.Add(a => a.CustomClass, "test-custom-class");
            pb.Add(a => a.Selector, "test-selector");
        });
        Assert.DoesNotContain("data-bs-placement", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Placement, Placement.Top);
        });
        cut.WaitForAssertion(() => Assert.Contains("data-bs-placement=\"top\"", cut.Markup));
    }
}
