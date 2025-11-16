// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AlertTest : TestBase
{
    [Fact]
    public void ShowDismiss_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowDismiss, true));
        Assert.Contains("button", cut.Markup);

        cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowDismiss, false));
        Assert.DoesNotContain("button", cut.Markup);
    }

    [Fact]
    public void ShowBar_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowBar, true));
        Assert.Contains("alert-bar", cut.Markup);

        cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowBar, false));
        Assert.DoesNotContain("alert-bar", cut.Markup);
    }

    [Fact]
    public void ShowBorder_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowBorder, true));
        Assert.Contains("border-primary", cut.Markup);

        cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowBorder, false));
        Assert.DoesNotContain("border-primary", cut.Markup);
    }

    [Fact]
    public void ShowShadow_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowShadow, true));
        Assert.Contains("shadow", cut.Markup);

        cut = Context.Render<Alert>(builder => builder.Add(a => a.ShowBar, false));
        Assert.DoesNotContain("shadow", cut.Markup);
    }

    [Fact]
    public void OnDismissHandle_Ok()
    {
        string message = "";
        var cut = Context.Render<Alert>(builder =>
        {
            builder.Add(a => a.ShowDismiss, true);
            builder.Add(a => a.OnDismiss, () =>
            {
                message = "Alert Dismissed";
                return Task.CompletedTask;
            });
        });
        cut.Find("button").Click();
        Assert.Equal("Alert Dismissed", message);
        //判断是否关闭
        Assert.Contains("d-none", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.ChildContent, BuildeComponent()));
        Assert.Contains("I am Alert", cut.Markup);

        RenderFragment BuildeComponent() => builder =>
        {
            builder.OpenElement(1, "div");
            builder.AddContent(2, "I am Alert");
            builder.CloseElement();
        };
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("alert-primary", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<Alert>(builder => builder.Add(a => a.Icon, "fa-solid fa-circle-check"));
        Assert.Contains("fa-solid fa-circle-check", cut.Markup);
    }
}
