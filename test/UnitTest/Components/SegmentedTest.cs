// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Dom;

namespace UnitTest.Components;

public class SegmentedTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SegmentedItem_Dispose()
    {
        var cut = Context.RenderComponent<SegmentedItem<string>>(pb =>
        {
            pb.Add(a => a.Text, "Hello");
            pb.Add(a => a.Value, "1");
        });
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void SegmentedItem_Ok()
    {
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Hello");
                pb.Add(a => a.Value, "1");
                pb.Add(a => a.Icon, "fa-test");
                pb.Add(a => a.IsActive, true);
                pb.Add(a => a.IsDisabled, false);
                pb.Add(a => a.ChildContent, builder =>
                {
                    builder.AddContent(0, "test-content");
                });
            });
        });
        cut.MarkupMatches("<div class=\"segmented\"><div class=\"segmented-item selected\">test-content</div></div>");
    }

    [Fact]
    public void SegmentedItem_Icon()
    {
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Hello");
                pb.Add(a => a.Value, "1");
                pb.Add(a => a.Icon, "fa-test");
                pb.Add(a => a.IsActive, true);
                pb.Add(a => a.IsDisabled, false);
            });
        });
        cut.MarkupMatches("<div class=\"segmented\"><div class=\"segmented-item selected\"><span class=\"segmented-item-icon\"><i class=\"fa-test\"></i></span><span>Hello</span></div></div>");
    }

    [Fact]
    public void SegmentedOption_Ok()
    {
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SegmentedOption<string>>()
            {
                new()
                {
                    Text = "Hello",
                    Value = "1",
                    Icon = "fa-test",
                    Active = true,
                    IsDisabled = false,
                    ChildContent = builder =>
                    {
                        builder.AddContent(0, "test-content");
                    }
                }
            });
        });
        cut.MarkupMatches("<div class=\"segmented\"><div class=\"segmented-item selected\">test-content</div></div>");
    }

    [Fact]
    public void SegmentedOption_Icon()
    {
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SegmentedOption<string>>()
            {
                new()
                {
                    Text = "Hello",
                    Value = "1",
                    Icon = "fa-test",
                    Active = true,
                    IsDisabled = false
                }
            });
        });
        cut.MarkupMatches("<div class=\"segmented\"><div class=\"segmented-item selected\"><span class=\"segmented-item-icon\"><i class=\"fa-test\"></i></span><span>Hello</span></div></div>");
    }

    [Theory]
    [InlineData(Size.Large, "segmented-lg")]
    [InlineData(Size.Small, "segmented-sm")]
    public void Segmented_Size(Size size, string expected)
    {
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.Add(a => a.Size, size);
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Daily");
                pb.Add(a => a.Value, "1");
            });
        });
        cut.Contains(expected);
    }

    [Fact]
    public void Segmented_IsDisabled()
    {
        string? newVal = "";
        bool valueChanged = false;
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.Add(a => a.Value, "34");
            pb.Add(a => a.ValueChanged, EventCallback.Factory.Create<string>(this, v =>
            {
                newVal = v;
            }));
            pb.Add(a => a.OnValueChanged, v =>
            {
                valueChanged = true;
                return Task.CompletedTask;
            });
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Daily");
                pb.Add(a => a.Value, "12");
            });
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Weekly");
                pb.Add(a => a.Value, "34");
            });
        });
        var items = cut.FindAll(".segmented-item");
        Assert.Equal(2, items.Count);
        Assert.True(items[1].ClassList.Contains("selected"));

        cut.InvokeAsync(() => items[0].Click());
        cut.WaitForAssertion(() =>
        {
            Assert.True(valueChanged);
            Assert.Equal("12", newVal);
        });

        newVal = "";
        valueChanged = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.WaitForState(() =>
        {
            items = cut.FindAll(".segmented-item.disabled");
            return items.Count == 2;
        });
        cut.InvokeAsync(() => items[1].Click());
        Assert.False(valueChanged);
        Assert.Equal("", newVal);
    }

    [Fact]
    public void Segmented_ItemTemplate()
    {
        var cut = Context.RenderComponent<Segmented<string>>(pb =>
        {
            pb.Add(a => a.Value, "34");
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Daily");
                pb.Add(a => a.Value, "12");
            });
            pb.AddChildContent<SegmentedItem<string>>(pb =>
            {
                pb.Add(a => a.Text, "Weekly");
                pb.Add(a => a.Value, "34");
            });
            pb.Add(a => a.ItemTemplate, op => builder =>
            {
                builder.AddContent(0, $"template-{op.Text}");
            });
        });

        var items = cut.FindAll(".segmented-item");
        Assert.Equal("template-Daily", items[0].Text());
        Assert.Equal("template-Weekly", items[1].Text());
    }
}
