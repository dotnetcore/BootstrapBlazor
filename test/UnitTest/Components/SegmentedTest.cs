// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;

namespace UnitTest.Components;

public class SegmentedTest : BootstrapBlazorTestBase
{
    [Fact]
    public void SegmentedItem_Dispose()
    {
        var cut = Context.Render<SegmentedItem<string>>(pb =>
        {
            pb.Add(a => a.Text, "Hello");
            pb.Add(a => a.Value, "1");
        });
        Assert.Equal("", cut.Markup);
    }

    [Fact]
    public void SegmentedItem_Ok()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
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
        cut.MarkupMatches("<div class=\"segmented\" id:ignore><div class=\"segmented-item mask\"></div><div class=\"segmented-item selected\">test-content</div></div>");
    }

    [Fact]
    public void SegmentedItem_Icon()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
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
        cut.MarkupMatches("<div class=\"segmented\" id:ignore><div class=\"segmented-item mask\"></div><div class=\"segmented-item selected\"><span class=\"segmented-item-icon\"><i class=\"fa-test\"></i></span><span class=\"segmented-item-text\">Hello</span></div></div>");
    }

    [Fact]
    public void SegmentedOption_Ok()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
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
        cut.MarkupMatches("<div class=\"segmented\" id:ignore><div class=\"segmented-item mask\"></div><div class=\"segmented-item selected\">test-content</div></div>");
    }

    [Fact]
    public void SegmentedOption_Icon()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
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
        cut.MarkupMatches("<div class=\"segmented\" id:ignore><div class=\"segmented-item mask\"></div><div class=\"segmented-item selected\"><span class=\"segmented-item-icon\"><i class=\"fa-test\"></i></span><span class=\"segmented-item-text\">Hello</span></div></div>");
    }

    [Theory]
    [InlineData(Size.Large, "segmented-lg")]
    [InlineData(Size.Small, "segmented-sm")]
    public void Segmented_Size(Size size, string expected)
    {
        var cut = Context.Render<Segmented<string>>(pb =>
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
        var cut = Context.Render<Segmented<string>>(pb =>
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
        Assert.Equal(3, items.Count);
        Assert.True(items[2].ClassList.Contains("selected"));

        cut.InvokeAsync(() => cut.Instance.TriggerClick(0));
        cut.WaitForAssertion(() =>
        {
            Assert.True(valueChanged);
            Assert.Equal("12", newVal);
        });

        newVal = "";
        valueChanged = false;
        cut.Render(pb =>
        {
            pb.Add(a => a.IsDisabled, true);
        });
        cut.WaitForState(() =>
        {
            items = cut.FindAll(".segmented-item.disabled");
            return items.Count == 2;
        });
        cut.InvokeAsync(() => cut.Instance.TriggerClick(1));
        Assert.False(valueChanged);
        Assert.Equal("", newVal);

        cut.InvokeAsync(() => cut.Instance.TriggerClick(3));
        Assert.False(valueChanged);
        Assert.Equal("", newVal);
    }

    [Fact]
    public void Segmented_ItemTemplate()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
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
        Assert.Equal("template-Daily", items[1].Text());
        Assert.Equal("template-Weekly", items[2].Text());
    }

    [Fact]
    public void Segmented_IsBlock()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
        {
            pb.Add(a => a.IsBlock, true);
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
        cut.Contains("segmented block");
        cut.DoesNotContain("data-bb-toggle");
    }

    [Fact]
    public void Segmented_ShowTooltip()
    {
        var cut = Context.Render<Segmented<string>>(pb =>
        {
            pb.Add(a => a.IsBlock, true);
            pb.Add(a => a.ShowTooltip, true);
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
        cut.Contains("segmented block");
        cut.Contains("data-bb-toggle=\"tooltip\"");
    }
}
