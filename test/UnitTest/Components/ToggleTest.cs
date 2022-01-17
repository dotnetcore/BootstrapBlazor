// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Components;

public class ToggleTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.Color, Color.Success);
            builder.Add(s => s.Value, true);
        });

        Assert.Contains("bg-success", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.Width, 100);
            builder.Add(s => s.Value, true);
        });

        Assert.Equal(100, cut.Instance.Width);
    }

    [Fact]
    public void OnText_Ok()
    {
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.OnText, "On");
            builder.Add(s => s.Value, true);
        });

        Assert.Equal("On", cut.Instance.OnText);
    }

    [Fact]
    public void OffText_Ok()
    {
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.OffText, "Off");
            builder.Add(s => s.Value, true);
        });

        Assert.Equal("Off", cut.Instance.OffText);
    }

    [Fact]
    public void OnValueChanged_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.OnValueChanged, (e) =>
            {
                value = e;
                return Task.CompletedTask;
            });
            builder.Add(s => s.Value, false);
        });

        cut.Find(".btn-toggle").Click();

        Assert.True(value);
    }

    [Fact]
    public void ValueChanged_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.ValueChanged, EventCallback.Factory.Create<bool>(this, (e) =>
            {
                value = e;
            }));
            builder.Add(s => s.Value, false);
        });

        cut.Find(".btn-toggle").Click();

        Assert.True(value);
    }

    [Fact]
    public void DisplayText_Ok()
    {
        var cut = Context.RenderComponent<Toggle>(builder =>
        {
            builder.Add(s => s.ShowLabel, true);
            builder.Add(s => s.DisplayText, "Toggle");
        });

        var value = cut.Find(".form-label").TextContent;

        Assert.NotNull(value);
    }
}
