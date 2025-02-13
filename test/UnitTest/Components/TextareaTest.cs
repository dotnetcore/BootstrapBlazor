// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TextareaTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsShowLabel_OK()
    {
        var cut = Context.RenderComponent<Textarea>(builder => builder.Add(s => s.ShowLabel, true));

        var component = cut.FindComponent<BootstrapLabel>();
        Assert.NotNull(component);

        component.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.LabelWidth, 120);
        });
        cut.Contains("style=\"--bb-row-label-width: 120px;\"");
    }

    [Fact]
    public void ShowLabelTooltip_OK()
    {
        var cut = Context.RenderComponent<BootstrapLabel>(pb =>
        {
            pb.Add(a => a.ShowLabelTooltip, null);
        });
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowLabelTooltip, false);
        });
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, "Test");
            pb.Add(a => a.ShowLabelTooltip, true);
        });
    }

    [Fact]
    public void AutoScrollString_OK()
    {
        var cut = Context.RenderComponent<Textarea>(builder =>
        {
            builder.Add(a => a.IsAutoScroll, true);
        });
        Assert.Contains("data-bb-scroll=\"auto\"", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAutoScroll, false);
        });
        Assert.DoesNotContain("data-bb-scroll=\"auto\"", cut.Markup);

        cut.Instance.ScrollTo(10);
        cut.Instance.ScrollToTop();
        cut.Instance.ScrollToBottom();
    }

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        var blur = false;
        var cut = Context.RenderComponent<Textarea>(builder =>
        {
            builder.Add(a => a.OnBlurAsync, v =>
            {
                blur = true;
                return Task.CompletedTask;
            });
        });
        var input = cut.Find("textarea");
        await cut.InvokeAsync(() => { input.Blur(); });
        Assert.True(blur);
    }

    [Fact]
    public void UseShiftEnter_Ok()
    {
        var cut = Context.RenderComponent<Textarea>(builder =>
        {
            builder.Add(a => a.UseShiftEnter, true);
        });
        cut.Contains("data-bb-shift-enter=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.UseShiftEnter, false);
        });
        cut.DoesNotContain("data-bb-shift-enter=\"true\"");
    }
}
