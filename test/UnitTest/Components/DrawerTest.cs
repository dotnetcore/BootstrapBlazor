﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class DrawerTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder => builder.Add(a => a.Width, "100px"));
        Assert.Contains("width: 100px", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Width, "");
        });
        Assert.DoesNotContain("width:", cut.Markup);
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.Height, "100px");
            builder.Add(a => a.Placement, Placement.Top);
        });
        Assert.Contains("height: 100px", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Height, "");
        });
        Assert.DoesNotContain("height:", cut.Markup);
    }

    [Fact]
    public void IsOpen_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsOpen, true);
            builder.Add(a => a.Placement, Placement.Top);
        });
    }

    [Fact]
    public void AllowResize_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.AllowResize, true);
        });
        cut.Contains("<div class=\"drawer-bar\"><div class=\"drawer-bar-body\"></div></div>");
    }

    [Fact]
    public void IsOpenChanged_Ok()
    {
        var isOpen = true;
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsBackdrop, true);
            builder.Add(a => a.IsOpen, true);
            builder.Add(a => a.IsOpenChanged, EventCallback.Factory.Create<bool>(this, e =>
            {
                isOpen = e;
            }));
        });

        cut.Find(".drawer-backdrop").Click();
        Assert.False(isOpen);
    }

    [Fact]
    public void OnClickBackdrop_Ok()
    {
        var isOpen = true;
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.IsBackdrop, true);
            builder.Add(a => a.IsOpen, true);
            builder.Add(a => a.OnClickBackdrop, () => { isOpen = false; return Task.CompletedTask; });
        });

        cut.Find(".drawer-backdrop").Click();
        Assert.False(isOpen);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });

        var button = cut.FindComponent<Button>();
        Assert.NotNull(button);
    }

    [Fact]
    public void ShowBackdrop_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.ShowBackdrop, true);
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });
        cut.Contains("drawer-backdrop");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowBackdrop, false);
        });
        cut.DoesNotContain("drawer-backdrop");
    }

    [Fact]
    public void Position_Ok()
    {
        var cut = Context.RenderComponent<Drawer>(builder =>
        {
            builder.Add(a => a.Position, "absolute");
            builder.Add(a => a.ChildContent, s =>
            {
                s.OpenComponent<Button>(0);
                s.CloseComponent();
            });
        });
        cut.Contains("--bb-drawer-position: absolute;");
    }
}
