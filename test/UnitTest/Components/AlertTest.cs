// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class AlertTest : TestBase
{
    [Fact]
    public void ShowDismiss_Ok()
    {
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowDismiss, true));
        Assert.Contains("button", cut.Markup);

        cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowDismiss, false));
        Assert.DoesNotContain("button", cut.Markup);
    }

    [Fact]
    public void ShowBar_Ok()
    {
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowBar, true));
        Assert.Contains("alert-bar", cut.Markup);

        cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowBar, false));
        Assert.DoesNotContain("alert-bar", cut.Markup);
    }

    [Fact]
    public void ShowBorder_Ok()
    {
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowBorder, true));
        Assert.Contains("border-primary", cut.Markup);

        cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowBorder, false));
        Assert.DoesNotContain("border-primary", cut.Markup);
    }

    [Fact]
    public void ShowShadow_Ok()
    {
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowShadow, true));
        Assert.Contains("shadow", cut.Markup);

        cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ShowBar, false));
        Assert.DoesNotContain("shadow", cut.Markup);
    }

    [Fact]
    public void OnDismissHandle_Ok()
    {
        string message = "";
        var cut = Context.RenderComponent<Alert>(builder =>
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
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.ChildContent, BuildeComponent()));
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
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.Color, Color.Primary));
        Assert.Contains("alert-primary", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<Alert>(builder => builder.Add(a => a.Icon, "fa fa-check-circle"));
        Assert.Contains("fa fa-check-circle", cut.Markup);
    }
}
