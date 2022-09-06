// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class CollapseTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Collapse_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<Collapse>(pb =>
        {
            pb.Add(a => a.CollapseItems, new RenderFragment(builder =>
            {
                builder.OpenComponent<CollapseItem>(0);
                builder.AddAttribute(1, nameof(CollapseItem.Text), "Item 1");
                builder.AddContent(2, "Content 1");
                builder.CloseComponent();
            }));
            pb.Add(a => a.OnCollapseChanged, item =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });

        var btn = cut.Find(".accordion-button");
        cut.InvokeAsync(() => btn.Click());
        Assert.True(clicked);
    }

    [Fact]
    public void Accordion_Ok()
    {
        var cut = Context.RenderComponent<Collapse>(pb =>
        {
            pb.Add(a => a.CollapseItems, new RenderFragment(builder =>
            {
                builder.OpenComponent<CollapseItem>(0);
                builder.AddAttribute(1, nameof(CollapseItem.Text), "Item 1");
                builder.AddContent(2, "Content 1");
                builder.CloseComponent();

                builder.OpenComponent<CollapseItem>(3);
                builder.AddAttribute(4, nameof(CollapseItem.Text), "Item 2");
                builder.AddContent(5, "Content 2");
                builder.CloseComponent();
            }));
            pb.Add(a => a.IsAccordion, true);
        });
        cut.Contains("is-accordion");

        var btn = cut.Find(".accordion-button");
        cut.InvokeAsync(() => btn.Click());
    }

    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.RenderComponent<Collapse>(pb =>
        {
            pb.Add(a => a.CollapseItems, new RenderFragment(builder =>
            {
                builder.OpenComponent<CollapseItem>(0);
                builder.AddAttribute(1, nameof(CollapseItem.Text), "Item 1");
                builder.AddAttribute(3, nameof(CollapseItem.Icon), "fa-solid fa-font-awesome");
                builder.AddContent(2, "Content 1");
                builder.CloseComponent();
            }));
        });
        cut.Contains("fa-solid fa-font-awesome");
    }

    [Fact]
    public void TitleColor_Ok()
    {
        var cut = Context.RenderComponent<Collapse>(pb =>
        {
            pb.Add(a => a.CollapseItems, new RenderFragment(builder =>
            {
                builder.OpenComponent<CollapseItem>(0);
                builder.AddAttribute(1, nameof(CollapseItem.Text), "Item 1");
                builder.AddAttribute(3, nameof(CollapseItem.TitleColor), Color.Secondary);
                builder.AddAttribute(2, nameof(CollapseItem.ChildContent), new RenderFragment(b => b.AddContent(0, "1")));
                builder.CloseComponent();
            }));
        });
        cut.Contains("btn-secondary");
    }

    [Fact]
    public void CollapseItem_Ok()
    {
        var cut = Context.RenderComponent<CollapseItem>(pb =>
        {
            pb.Add(a => a.Icon, "fa-solid fa-font-awesome");
        });
    }

    [Fact]
    public void CollapseItem_Class()
    {
        var cut = Context.RenderComponent<Collapse>(pb =>
        {
            pb.Add(a => a.CollapseItems, new RenderFragment(builder =>
            {
                builder.OpenComponent<CollapseItem>(0);
                builder.AddAttribute(1, nameof(CollapseItem.Text), "Item 1");
                builder.AddAttribute(3, nameof(CollapseItem.Class), "test-class");
                builder.AddAttribute(2, nameof(CollapseItem.ChildContent), new RenderFragment(b => b.AddContent(0, "1")));
                builder.CloseComponent();
            }));
        });
        cut.Contains("accordion-item test-class");
    }
}
