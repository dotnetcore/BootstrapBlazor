// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TagTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Tag>(builder => builder.Add(a => a.Color, Color.Primary));

        Assert.Contains("alert-primary", cut.Markup);
    }

    [Fact]
    public void ShowDismiss_Ok()
    {
        var cut = Context.Render<Tag>(builder => builder.Add(a => a.ShowDismiss, true));

        Assert.Contains("button", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<Tag>(builder => builder.Add(a => a.Icon, "fa-fw fa-solid fa-circle-check"));

        Assert.Contains("fa-fw fa-solid fa-circle-check", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.Render<Tag>(builder => builder.Add(a => a.ChildContent, CreateComponent()));

        var button = cut.FindComponent<Button>();

        Assert.NotNull(button);
    }

    [Fact]
    public void OnDismiss_Ok()
    {
        var OnDismiss = false;
        var cut = Context.Render<Tag>(builder =>
        {
            builder.Add(a => a.ShowDismiss, true);
            builder.Add(a => a.OnDismiss, () => { OnDismiss = true; return Task.CompletedTask; });
        });

        var button = cut.Find(".btn-close");
        button.Click();

        Assert.True(OnDismiss);
    }

    private static RenderFragment CreateComponent() => builder =>
    {
        builder.OpenComponent<Button>(0);
        builder.CloseComponent();
    };
}
