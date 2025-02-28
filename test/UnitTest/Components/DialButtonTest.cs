// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DialButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void DialButton_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.AddUnmatched("class", "dial-test");
        });
        cut.Contains("dial-button dial-test");
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<DialButton>();
        cut.Contains("btn btn-primary");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Color, Color.Success);
        });
        cut.Contains("btn btn-success");
    }

    [Fact]
    public void Placement_Ok()
    {
        var cut = Context.RenderComponent<DialButton>();
        cut.Contains("data-bb-placement=\"auto\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Placement, Placement.Bottom);
        });
        cut.Contains("data-bb-placement=\"bottom\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Placement, Placement.Left);
        });
        cut.Contains("data-bb-placement=\"left\"");
    }

    [Fact]
    public void Offset_Ok()
    {
        var cut = Context.RenderComponent<DialButton>();
        cut.Contains("data-bb-offset=\"8\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Offset, 4);
        });
        cut.Contains("data-bb-offset=\"4\"");
    }

    [Fact]
    public void Radius_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(a => a.Radius, 100);
        });
        cut.DoesNotContain("data-bb-radius");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DialMode, DialMode.Radial);
        });
        cut.Contains("data-bb-radius=\"100\"");
    }

    [Fact]
    public void Size_Ok()
    {
        var cut = Context.RenderComponent<DialButton>();
        cut.Contains("btn btn-primary");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Size, Size.Small);
        });
        cut.Contains("btn btn-primary btn-sm");
    }

    [Fact]
    public void Duration_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(a => a.Duration, 400);
        });
        cut.DoesNotContain("data-bb-duration");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Duration, 500);
        });
        cut.Contains("data-bb-duration=\"500\"");
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(i => i.Icon, "fa fa-flag");
        });
        cut.Contains("fa fa-flag");
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(i => i.IsDisabled, true);
        });
        cut.Contains("disabled=\"disabled\"");
    }

    [Fact]
    public void IsAutoClose_Ok()
    {
        var cut = Context.RenderComponent<DialButton>();
        cut.Contains("data-bb-auto-close=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.IsAutoClose, false);
        });
        cut.DoesNotContain("data-bb-auto-close");
    }

    [Fact]
    public void DialMode_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(a => a.DialMode, DialMode.Radial);
        });
        cut.Contains("dial-button is-radial");
    }

    [Fact]
    public void ButtonTemplate_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(i => i.ButtonTemplate, b => b.AddContent(10, new MarkupString("<div>ButtonTemplate-Test</div>")));
        });
        cut.Contains("<div>ButtonTemplate-Test</div>");
    }

    [Fact]
    public void OnClick_Ok()
    {
        DialButtonItem? dialItem = null;
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(i => i.ChildContent, new RenderFragment(pb =>
            {
                pb.OpenComponent<DialButtonItem>(0);
                pb.AddAttribute(1, "Icon", "fa-solid fa-cut");
                pb.AddAttribute(2, "Value", "cut");
                pb.CloseComponent();
                pb.OpenComponent<DialButtonItem>(10);
                pb.AddAttribute(11, "Icon", "fa-solid fa-copy");
                pb.AddAttribute(12, "Value", "copy");
                pb.CloseComponent();
                pb.OpenComponent<DialButtonItem>(20);
                pb.AddAttribute(21, "Icon", "fa-solid fa-paste");
                pb.AddAttribute(22, "Value", "paste");
                pb.CloseComponent();
            }));
            pb.Add(i => i.OnClick, EventCallback.Factory.Create<DialButtonItem>(this, item =>
            {
                dialItem = item;
            }));
        });
        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".dial-item");
            Assert.Equal(3, items.Count);
        });

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".dial-item");
            item.Click();
        });
        Assert.NotNull(dialItem);
        Assert.Equal("cut", dialItem.Value);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(i => i.ChildContent, new RenderFragment(pb =>
            {
                pb.OpenComponent<DialButtonItem>(0);
                pb.AddAttribute(1, "Icon", "fa-solid fa-cut");
                pb.AddAttribute(2, "Value", "cut");
                pb.CloseComponent();
            }));
            pb.Add(i => i.ItemTemplate, new RenderFragment<DialButtonItem>(item => pb =>
            {
                pb.AddContent(0, $"test-{item.Icon}-{item.Value}");
            }));

        });
        cut.Contains("test-fa-solid fa-cut-cut");
    }

    [Fact]
    public void Item_ChildContent_Ok()
    {
        var cut = Context.RenderComponent<DialButton>(pb =>
        {
            pb.Add(i => i.ChildContent, new RenderFragment(pb =>
            {
                pb.OpenComponent<DialButtonItem>(0);
                pb.AddAttribute(1, "ChildContent", new RenderFragment(pb =>
                {
                    pb.AddContent(0, "test-ChildContent");
                }));
                pb.CloseComponent();
            }));
        });
        cut.Contains("test-ChildContent");
    }

    [Fact]
    public void DialButtonItem_Ok()
    {
        var cut = Context.RenderComponent<DialButtonItem>(pb =>
        {
            pb.Add(a => a.Icon, "fa fa-test");
        });
        Assert.Empty(cut.Markup);
    }
}
