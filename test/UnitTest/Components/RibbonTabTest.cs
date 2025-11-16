// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace UnitTest.Components;

public class RibbonTabTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ShowFloatButton_Ok()
    {
        var isFloat = false;
        RibbonTabItem? tabItem = null;
        var cut = Context.Render<RibbonTab>();
        Assert.DoesNotContain("ribbon-arrow", cut.Markup);
        cut.Render(pb =>
        {
            pb.Add(a => a.Items, GetItems());
            pb.Add(a => a.ShowFloatButton, true);
            pb.Add(a => a.RibbonArrowUpIcon, "test-up");
            pb.Add(a => a.RibbonArrowDownIcon, "test-down");
            pb.Add(a => a.RibbonArrowPinIcon, "test-pin");
            pb.Add(a => a.OnItemClickAsync, item =>
            {
                tabItem = item;
                return Task.CompletedTask;
            });
            pb.Add(a => a.OnFloatChanged, floating =>
            {
                isFloat = floating;
                return Task.CompletedTask;
            });
        });
        cut.Contains("class=\"ribbon-tab border\"");
        Assert.Contains("ribbon-arrow", cut.Markup);
        Assert.Contains("test-up", cut.Markup);

        var i = cut.Find(".ribbon-arrow > i");
        await cut.InvokeAsync(() => i.Click());
        Assert.True(isFloat);
        Assert.Contains("test-down", cut.Markup);

        // 点击 Tab 显示 pin 图标
        var tab = cut.Find(".tabs-item");
        await cut.InvokeAsync(() => tab.Click());
        Assert.Contains("test-pin", cut.Markup);

        var linkButton = cut.FindComponent<LinkButton>();
        await cut.InvokeAsync(() => linkButton.Instance.OnClick.InvokeAsync());
        Assert.NotNull(tabItem);

        await cut.InvokeAsync(() => i.Click());
        Assert.False(isFloat);
        Assert.Contains("test-up", cut.Markup);
    }

    [Fact]
    public async Task SetExpand_Ok()
    {
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.ShowFloatButton, true);
        });

        var i = cut.Find(".ribbon-arrow > i");
        await cut.InvokeAsync(() => i.Click());
        Assert.Contains("fa-angle-down", cut.Markup);

        await cut.InvokeAsync(() => cut.Instance.SetExpand());
        Assert.Contains("fa-angle-down", cut.Markup);
    }

    [Fact]
    public void RightButtonsTemplate_Ok()
    {
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.RightButtonsTemplate, builder =>
            {
                builder.AddContent(0, "test-content");
            });
        });
        Assert.Contains("test-content", cut.Markup);
    }

    [Fact]
    public void RibbonTabItem_Ok()
    {
        var parent = new RibbonTabItem() { Text = "parent", CssClass = "test-header-class" };
        var item = new RibbonTabItem()
        {
            Id = "10",
            ParentId = "1",
            Parent = parent,
            IsCollapsed = true,
            IsDisabled = false,
            CssClass = "test-class",
            Target = "_blank",
            Url = "https://blazor.zone",
            ImageUrl = "test-image-url",
            Command = "test-command"
        };
        parent.Items.Add(item);

        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, [parent]);
        });
        cut.Contains("href=\"https://blazor.zone\"");
        cut.Contains("target=\"_blank\"");
        cut.Contains("<img alt=\"img\" src=\"test-image-url\" />");
        cut.Contains("test-class");
        cut.Contains("test-header-class");

        Assert.True(item.IsCollapsed);
        Assert.Equal("1", item.ParentId);
        Assert.Equal("10", item.Id);
        Assert.Equal("test-command", item.Command);
        Assert.NotNull(item.Parent);
    }

    [Fact]
    public void RibbonTabItem_Template()
    {
        var item = new RibbonTabItem() { Text = "test" };
        item.Items.Add(new RibbonTabItem() { Text = "Item", Template = builder => builder.AddContent(0, "Test-Template") });
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, [item]);
        });
        Assert.Contains("Test-Template", cut.Markup);
    }

    [Fact]
    public void RibbonTabItem_Component()
    {
        var item = new RibbonTabItem() { Text = "test" };
        item.Items.Add(new RibbonTabItem() { Text = "Item", Component = BootstrapDynamicComponent.CreateComponent<MockCom>() });
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, [item]);
        });
        Assert.Contains("Test-Template", cut.Markup);
    }

    [Fact]
    public void ChildContent_Ok()
    {
        var item = new RibbonTabItem() { Text = "test" };
        item.Items.Add(new RibbonTabItem() { Text = "Item" });
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, [item]);
            pb.Add(a => a.ChildContent, builder =>
            {
                builder.AddContent(0, "test-child-content");
            });
        });
        Assert.Contains("test-child-content", cut.Markup);
        Assert.Contains("ribbon-body", cut.Markup);
    }

    [Fact]
    public void OnMenuClickAsync_Ok()
    {
        var item = new RibbonTabItem() { Text = "test 1" };
        item.Items.Add(new RibbonTabItem() { Text = "Item" });
        var clickedText = "";
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, [item]);
            pb.Add(a => a.OnMenuClickAsync, item =>
            {
                clickedText = item.Text;
                return Task.CompletedTask;
            });
        });

        var tab = cut.Find(".tabs-item");
        cut.InvokeAsync(() => tab.Click());
        Assert.Equal("test 1", clickedText);
    }

    [Fact]
    public void IsBoard_Ok()
    {
        var item = new RibbonTabItem() { Text = "test 1" };
        item.Items.Add(new RibbonTabItem() { Text = "Item" });
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, [item]);
            pb.Add(a => a.IsBorder, false);
        });
        cut.Contains("class=\"ribbon-tab\"");
    }

    [Fact]
    public void IsDefault_Ok()
    {
        var item = new RibbonTabItem() { IsDefault = true };
        Assert.True(item.IsDefault);
    }

    [Fact]
    public void Render_Ok()
    {
        var item1 = new RibbonTabItem() { Text = "test 1" };
        item1.Items.Add(new RibbonTabItem() { Text = "Item" });

        var items = new List<RibbonTabItem>() { item1 };
        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        items.Add(new RibbonTabItem() { Text = "Test2" });
        cut.InvokeAsync(() => cut.Instance.Render());
        cut.Contains("<span class=\"tabs-item-text\">Test2</span>");
    }

    private static IEnumerable<RibbonTabItem> GetItems()
    {
        var item1 = new RibbonTabItem() { Text = "文件" };
        item1.Items.AddRange(
        [
            new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组一" },
            new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组一" },
            new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组一" },
            new() { Text = "打开", Icon = "fa-solid fa-font-awesome", GroupName = "操作组二" },
            new() { Text = "保存", Icon = "fa-solid fa-font-awesome", GroupName = "操作组二" },
            new() { Text = "另存为", Icon = "fa-solid fa-font-awesome", GroupName = "操作组二" }
        ]);
        var item2 = new RibbonTabItem() { Text = "编辑" };
        item2.Items.AddRange(
        [
            new() { Text = "打开", Icon = "fa-solid fa-font-awesome", GroupName = "操作组三", IsDefault = true },
            new() { Text = "保存", Icon = "fa-solid fa-font-awesome", GroupName = "操作组三" },
            new() { Text = "另存为", Icon = "fa-solid fa-font-awesome", GroupName = "操作组三" },
            new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组四" },
            new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组四" },
            new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组四" }
        ]);
        return [item1, item2];
    }

    class MockCom : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, "Test-Template");
        }
    }
}
