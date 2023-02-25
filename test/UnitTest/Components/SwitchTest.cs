// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SwitchTest : BootstrapBlazorTestBase
{
    [Fact]
    public void OnColor_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, true);
            builder.Add(a => a.OnColor, Color.None);
        });

        Assert.DoesNotContain("bg-", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnColor, Color.Danger);
        });
        Assert.Contains("bg-danger", cut.Markup);
    }

    [Fact]
    public void OffColor_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.OffColor, Color.Danger);
        });

        Assert.Contains("bg-danger", cut.Markup);
    }

    [Fact]
    public void Width_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.Width, 100);
        });

        Assert.Contains("width: 100px;", cut.Markup);
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.Height, 20);
        });

        Assert.Contains("height: 20px;", cut.Markup);
    }

    [Fact]
    public void OnInnerText_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, true);
            builder.Add(a => a.OnInnerText, "On");
            builder.Add(a => a.ShowInnerText, true);
        });

        Assert.Contains("On", cut.Markup);
    }

    [Fact]
    public void OffInnerText_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.OffInnerText, "Off");
            builder.Add(a => a.ShowInnerText, true);
        });

        Assert.Contains("Off", cut.Markup);
    }


    [Fact]
    public void ShowInnerText_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.OffInnerText, "Off");
            builder.Add(a => a.ShowInnerText, true);
        });

        var text = cut.Find("span").GetAttribute("data-inner-text");

        Assert.Equal("Off", text);
    }

    [Fact]
    public void OnValueChanged_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.OnValueChanged, e => { value = e; return Task.CompletedTask; });
        });

        cut.Find("span").Click();
        Assert.True(value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, false);
        });
        Assert.True(value);
    }

    [Fact]
    public void ValueChanged_Ok()
    {
        var value = false;
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, false);
            builder.Add(a => a.ValueChanged, EventCallback.Factory.Create<bool>(this, e => { value = e; }));
        });

        cut.Find("span").Click();

        Assert.True(value);
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.IsDisabled, true);
        });

        Assert.Contains("disable", cut.Markup);
    }

    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.Value, true);
        });

        Assert.Contains("is-checked", cut.Markup);
    }

    [Fact]
    public void DisplayText_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.DisplayText, "custome label");
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.Value, true);
        });

        var text = cut.Find("label").TextContent;

        Assert.Equal("custome label", text);
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.DisplayText, "custome label");
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.Value, true);
        });

        var label = cut.Find("label");

        Assert.NotNull(label);
    }

    [Fact]
    public void OnText_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.OnText, "On");
            builder.Add(a => a.Value, true);
        });

        Assert.Equal("On", cut.Find(".switch-label").TextContent);
    }

    [Fact]
    public void Off_Ok()
    {
        var cut = Context.RenderComponent<Switch>(builder =>
        {
            builder.Add(a => a.OffText, "Off");
            builder.Add(a => a.Value, false);
        });

        Assert.Equal("Off", cut.Find(".switch-label").TextContent);
    }
}
