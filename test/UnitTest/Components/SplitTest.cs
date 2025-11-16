// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SplitTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SplitStyle_Ok()
    {
        var cut = Context.Render<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
            pb.Add(b => b.IsVertical, true);
            pb.Add(b => b.IsKeepOriginalSize, true);
        });
        Assert.Contains("I am Pane1", cut.Markup);
        Assert.Contains("I am Pane2", cut.Markup);
        Assert.Contains("is-vertical", cut.Markup);
        Assert.Contains("split-bar-handler", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.Basis, "90%");
        });
        Assert.Contains("90%", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(b => b.AdditionalAttributes, new Dictionary<string, object>() { ["tag"] = "tag" });
        });
        Assert.Contains("tag", cut.Markup);
    }

    [Fact]
    public void ShowBarHandle_Ok()
    {
        var cut = Context.Render<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
            pb.Add(b => b.ShowBarHandle, false);
            pb.Add(b => b.IsKeepOriginalSize, true);
        });
        Assert.DoesNotContain("split-bar-handler", cut.Markup);
    }

    [Fact]
    public void IsCollapsible_Ok()
    {
        var cut = Context.Render<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
        });
        cut.DoesNotContain("split-bar-arrow-left");
        cut.DoesNotContain("split-bar-arrow-right");

        cut.Render(pb =>
        {
            pb.Add(b => b.IsCollapsible, true);
        });
        cut.Contains("split-bar-arrow-left");
        cut.Contains("split-bar-arrow-right");
    }

    [Fact]
    public void Minimum_Ok()
    {
        var cut = Context.Render<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
            pb.Add(b => b.FirstPaneMinimumSize, "abc");
            pb.Add(b => b.SecondPaneMinimumSize, "12rem");
        });
        cut.Contains("data-bb-min=\"abc\"");
        cut.Contains("data-bb-min=\"12rem\"");
    }

    [Fact]
    public async Task OnResizedAsync_Ok()
    {
        SplitterResizedEventArgs? state = null;
        var cut = Context.Render<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
            pb.Add(b => b.IsCollapsible, true);
            pb.Add(b => b.OnResizedAsync, async args =>
            {
                state = args;
                await Task.CompletedTask;
            });
        });
        Assert.Null(state);
        await cut.InvokeAsync(() => cut.Instance.TriggerOnResize("0%"));
        Assert.NotNull(state);
        Assert.Equal("0%", state.FirstPanelSize);
        Assert.True(state.IsCollapsed);
        Assert.False(state.IsExpanded);

        await cut.InvokeAsync(() => cut.Instance.TriggerOnResize("100%"));
        Assert.Equal("100%", state.FirstPanelSize);
        Assert.True(state.IsExpanded);
        Assert.False(state.IsCollapsed);
    }

    [Fact]
    public async Task SetLeft_Ok()
    {
        var cut = Context.Render<Split>(pb =>
        {
            pb.Add(b => b.FirstPaneTemplate, RenderSplitView("I am Pane1"));
            pb.Add(b => b.SecondPaneTemplate, RenderSplitView("I am Pane2"));
        });
        await cut.InvokeAsync(() => cut.Instance.SetLeftWidth("25%"));
    }

    static RenderFragment RenderSplitView(string name = "I am Pane1") => builder =>
    {
        builder.OpenElement(1, "div");
        builder.AddContent(2, name);
        builder.CloseElement();
    };
}
