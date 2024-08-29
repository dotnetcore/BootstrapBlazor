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
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
            pb.Add(b => b.IsVertical, true);
            pb.Add(b => b.IsKeepOriginalSize, true);
        });
        Assert.Contains("I am Pane1", cut.Markup);
        Assert.Contains("I am Pane2", cut.Markup);
        Assert.Contains("is-vertical", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.Basis, "90%");
        });
        Assert.Contains("90%", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.AdditionalAttributes, new Dictionary<string, object>() { ["tag"] = "tag" });
        });
        Assert.Contains("tag", cut.Markup);
    }

    [Fact]
    public void IsCollapsible_Ok()
    {
        var cut = Context.RenderComponent<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
        });
        cut.DoesNotContain("split-bar-arrow-left");
        cut.DoesNotContain("split-bar-arrow-right");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(b => b.IsCollapsible, true);
        });
        cut.Contains("split-bar-arrow-left");
        cut.Contains("split-bar-arrow-right");
    }

    [Fact]
    public async Task OnCollapsedAsync_Ok()
    {
        var state = false;
        var cut = Context.RenderComponent<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
            pb.Add(b => b.IsCollapsible, true);
            pb.Add(b => b.OnCollapsedAsync, async (collapsed) =>
            {
                state = collapsed;
                await Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerOnCollapsed(true));
        Assert.True(state);
        await cut.InvokeAsync(() => cut.Instance.TriggerOnCollapsed(false));
        Assert.False(state);
    }

    static RenderFragment RenderSplitView(string name = "I am Pane1") => builder =>
    {
        builder.OpenElement(1, "div");
        builder.AddContent(2, name);
        builder.CloseElement();
    };
}
