// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class TagTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Tag>(builder => builder.Add(a => a.Color, Color.Primary));

        Assert.Contains("alert-primary", cut.Markup);
    }

    [Fact]
    public void ShowDismiss_Ok()
    {
        var cut = Context.RenderComponent<Tag>(builder => builder.Add(a => a.ShowDismiss, true));

        Assert.Contains("button", cut.Markup);
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<Tag>(builder => builder.Add(a => a.Icon, "fa fa-fw fa-check-circle"));

        Assert.Contains("fa fa-fw fa-check-circle", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Tag>(builder => builder.Add(a => a.ChildContent, CreateComponent()));

        var button = cut.FindComponent<Button>();

        Assert.NotNull(button);
    }

    [Fact]
    public void OnDismiss_Ok()
    {
        var OnDismiss = false;
        var cut = Context.RenderComponent<Tag>(builder =>
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
