// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ToggleTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<Toggle>(builder =>
        {
            builder.Add(s => s.Color, Color.Success);
            builder.Add(s => s.Value, true);
        });

        Assert.Contains("bg-success", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.Render<Toggle>(builder =>
        {
            builder.Add(s => s.Width, 100);
            builder.Add(s => s.Value, true);
        });

        Assert.Equal(100, cut.Instance.Width);
    }

    [Fact]
    public void OnText_Ok()
    {
        var cut = Context.Render<Toggle>(builder =>
        {
            builder.Add(s => s.OnText, "On");
            builder.Add(s => s.Value, true);
        });

        Assert.Equal("On", cut.Instance.OnText);
    }

    [Fact]
    public void OffText_Ok()
    {
        var cut = Context.Render<Toggle>(builder =>
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
        var cut = Context.Render<Toggle>(builder =>
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
    public async Task ValueChanged_Ok()
    {
        var value = false;
        var cut = Context.Render<Toggle>(builder =>
        {
            builder.Add(s => s.ValueChanged, EventCallback.Factory.Create<bool>(this, (e) =>
            {
                value = e;
            }));
            builder.Add(s => s.Value, false);
        });

        await cut.InvokeAsync(() => cut.Find(".btn-toggle").Click());
        Assert.True(value);
    }

    [Fact]
    public void DisplayText_Ok()
    {
        var cut = Context.Render<Toggle>(builder =>
        {
            builder.Add(s => s.ShowLabel, true);
            builder.Add(s => s.DisplayText, "Toggle");
        });

        var value = cut.Find(".form-label").TextContent;

        Assert.NotNull(value);
    }
}
