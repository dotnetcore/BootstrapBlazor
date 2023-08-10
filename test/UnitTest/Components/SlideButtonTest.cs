// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class SlideButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SlideButton_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.AddUnmatched("class", "slide-test");
        });
        cut.Contains("slide-button slide-test");
        cut.Contains("slide-list d-none");
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>();
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
        var cut = Context.RenderComponent<SlideButton>();
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
        cut.Contains("slide-list d-none is-horizontal");
    }

    [Fact]
    public void Offset_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>();
        cut.Contains("data-bb-offset=\"8\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Offset, 4);
        });
        cut.Contains("data-bb-offset=\"4\"");
    }

    [Fact]
    public void Size_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>();
        cut.Contains("btn btn-primary");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.Size, Size.Small);
        });
        cut.Contains("btn btn-primary btn-sm");
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.Icon, "fa fa-flag");
        });
        cut.Contains("fa fa-flag");
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.Text, "button-text");
        });
        cut.Contains("button-text");
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.IsDisabled, true);
        });
        cut.Contains("disabled=\"disabled\"");
    }

    [Fact]
    public void ShowHeader_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>();
        cut.DoesNotContain("slide-header");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.ShowHeader, true);
            pb.Add(i => i.HeaderText, "header-text");
        });
        cut.Contains("slide-header");
        cut.Contains("header-text");
    }

    [Fact]
    public void IsAutoClose_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>();
        cut.Contains("data-bb-auto-close=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(i => i.IsAutoClose, false);
        });
        cut.DoesNotContain("data-bb-auto-close");
    }

    [Fact]
    public void SlideButtonItems_Ok()
    {
        SelectedItem? item = null;
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.SlideButtonItems, b =>
            {
                b.OpenComponent<SlideButtonItem>(10);
                b.AddAttribute(11, "Value", "1");
                b.AddAttribute(12, "Text", "Test 1");
                b.CloseComponent();
                b.OpenComponent<SlideButtonItem>(20);
                b.AddAttribute(21, "Value", "2");
                b.AddAttribute(22, "Text", "Test 2");
                b.CloseComponent();
            });
            pb.Add(i => i.IsAutoClose, true);
            pb.Add(i => i.OnClick, _item =>
            {
                item = _item;
            });
        });

        cut.Contains("slide-item");
        cut.Contains("Test 1");
        cut.Contains("Test 2");

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".slide-item");
            item.Click();
        });
        Assert.NotNull(item);
        Assert.Equal("1", item.Value);
    }

    [Fact]
    public void ButtonTemplate_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.ButtonTemplate, b => b.AddContent(10, new MarkupString("<div>ButtonTemplate-Test</div>")));
        });
        cut.Contains("<div>ButtonTemplate-Test</div>");
    }

    [Fact]
    public void Items_Ok()
    {
        SelectedItem? item = null;
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.Items, new SelectedItem[]
            {
                new("1", "Test 1"),
                new("2", "Test 2")
            });
            pb.Add(i => i.OnClick, _item =>
            {
                item = _item;
            });
        });
        cut.Contains("Test 1");

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".slide-item");
            item.Click();
        });
        Assert.NotNull(item);
        Assert.Equal("1", item.Value);
    }

    [Fact]
    public void BodyTemplate_Ok()
    {
        var cut = Context.RenderComponent<SlideButton>(pb =>
        {
            pb.Add(i => i.Items, new SelectedItem[]
            {
                new("1", "Test 1"),
                new("2", "Test 2")
            });
            pb.Add(i => i.BodyTemplate, b => b.AddContent(1, """
                <div class="slide-button-custom-group">
                    <div class="slide-item-custom">
                        <i class="fa-solid fa-copy"></i>
                    </div>
                    <div class="slide-item-custom">
                        <i class="fa-solid fa-cut"></i>
                    </div>
                    <div class="slide-item-custom">
                        <i class="fa-solid fa-paste"></i>
                    </div>
                </div>
                <div class="slide-button-custom-group">
                    <div class="slide-item-custom">
                        <i class="fa-solid fa-copy"></i>
                    </div>
                    <div class="slide-item-custom">
                        <i class="fa-solid fa-cut"></i>
                    </div>
                    <div class="slide-item-custom">
                        <i class="fa-solid fa-paste"></i>
                    </div>
                </div>
                """));
        });
        cut.InvokeAsync(() =>
        {
            var items = cut.FindAll(".slide-item-custom");
            Assert.Equal(3, items.Count);
        });
    }
}
