// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Routing;

namespace UnitTest.Components;

public class MenuTest : BootstrapBlazorTestBase
{
    private List<MenuItem> Items { get; set; }

    public MenuTest() => Items =
    [
        new("Menu1")
        {
            IsActive = true,
            Icon = "fa-solid fa-font-awesome",
            Url = "https://www.blazor.zone"
        },
        new("Menu2")
        {
            Icon = "fa-solid fa-fw fa-font-awesome",
            Items = new List<MenuItem>
            {
                new("Menu21")
                {
                    Icon = "fa-solid fa-font-awesome",
                    IsDisabled = true
                },
                new("Menu22")
                {
                    Url = "/menu22",
                    Icon = "fa-solid fa-fw fa-font-awesome"
                },
                new("Menu23")
                {
                    Icon = "fa-solid fa-fw fa-font-awesome",
                    Items = new List<MenuItem>
                    {
                        new("Menu231"),
                        new("Menu232")
                        {
                            Template = BootstrapDynamicComponent.CreateComponent<Button>().Render(),
                            Items = new List<MenuItem>()
                            {
                                new("Menu2321")
                                {
                                    Icon = "fa-solid fa-font-awesome",
                                    Url = "/Menu2321"
                                },
                                new("Menu2322")
                                {
                                    Icon = "fa-solid fa-fw fa-font-awesome"
                                },
                                new("Menu2323")
                            }
                        }
                    }
                },
                new("Menu24")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Target = "_blank",
                    Match = NavLinkMatch.All
                },
                new("Menu25")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Items = new List<MenuItem>
                    {
                        new("Menu251")
                        {
                            Icon = "fa-solid fa-font-awesome"
                        }
                    }
                }
            }
        },
        new("Menu3")
        {
            Icon = "fa-solid fa-font-awesome",
            Items = new List<MenuItem>
            {
                new("Menu31")
            }
        },
        new("Menu4")
        {
            IsActive = true,
            Icon = "fa-solid fa-fw fa-font-awesome",
            Url = "https://www.blazor.zone"
        }
    ];

    [Fact]
    public void Items_Ok()
    {
        // 未设置 Items
        var cut = Context.Render<Menu>();
        Assert.DoesNotContain("Menu1", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(m => m.Items, Items);
        });

        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        cut.WaitForAssertion(() => cut.Contains("Menu1"));

        cut.Render(pb =>
        {
            pb.Add(m => m.Items, null);
        });
        cut.WaitForAssertion(() => cut.Contains("submenu"));
    }

    [Fact]
    public void DisableNavigation_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new MenuItem[]
            {
                new("Menu1")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "https://www.blazor.zone"
                },
                new("Menu2")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "https://www.blazor.zone"
                }
            });
            pb.Add(m => m.DisableNavigation, true);
        });

        // 无 Active 菜单 触发点击事件
        // 子菜单 Click 触发
        var menuItems = cut.Find("li");
        menuItems.Click();
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsVertical, false);
        });
        Assert.DoesNotContain("is-vertical", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("is-vertical", cut.Markup);

        // 垂直布局时设置手风琴效果触发 ShouldInvoke
        cut.Render(pb =>
        {
            pb.Add(a => a.IsAccordion, true);
        });
        cut.WaitForState(() => cut.Markup.Contains("accordion"));
        Assert.Contains("accordion", cut.Markup);
    }

    [Theory]
    [InlineData(false, true, true, false)]
    [InlineData(true, false, true, false)]
    [InlineData(true, true, false, false)]
    [InlineData(true, true, true, true)]
    public void CollapsedSubMenuPopup_RequiresAllParameters(bool isVertical, bool isCollapsed, bool enablePopup, bool expectedPopup)
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new[] { new MenuItem("Parent") { Items = [new("Child")] } });
            pb.Add(m => m.IsVertical, isVertical);
            pb.Add(m => m.IsCollapsed, isCollapsed);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, enablePopup);
        });

        Assert.Equal(expectedPopup, cut.Find("nav").ClassList.Contains("is-collapsed"));
        Assert.Equal(expectedPopup, cut.FindAll("[popover='manual']").Count > 0);
        if (isVertical && !expectedPopup)
        {
            var link = cut.Find("a.nav-link");
            Assert.Equal("collapse", link.GetAttribute("data-bs-toggle"));
            Assert.Null(link.GetAttribute("data-bb-menu-popup"));
            var target = link.GetAttribute("data-bs-target");
            Assert.NotNull(target);
            Assert.StartsWith("#", target);
            Assert.True(cut.Find(target).ClassList.Contains("collapse"));
        }
    }

    [Fact]
    public void CollapsedSubMenuPopup_DefaultKeepsCollapseBehavior()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new[] { new MenuItem("Parent") { Items = [new("Child")] } });
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
        });

        var link = cut.Find("a.nav-link");
        Assert.False(cut.Find("nav").ClassList.Contains("is-collapsed"));
        Assert.Equal("collapse", link.GetAttribute("data-bs-toggle"));
        Assert.Null(link.GetAttribute("data-bb-menu-popup"));
        Assert.Empty(cut.FindAll("[popover]"));
    }

    [Theory]
    [InlineData("Menu", "M")]
    [InlineData("菜单", "菜")]
    [InlineData("A", "A")]
    [InlineData("", "")]
    [InlineData(null, "")]
    public void CollapsedSubMenuPopup_UsesFirstCharacterForMissingIcon(string? text, string expected)
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new[] { new MenuItem { Text = text } });
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, true);
        });

        Assert.Equal(expected, cut.Find("span.menu-icon").TextContent);
    }

    [Fact]
    public void CollapsedSubMenuPopup_FirstCharacterOnlyForTopLevelWithoutIcon()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new[]
            {
                new MenuItem("Parent") { Items = [new("Child")] },
                new MenuItem("With icon", icon: "fa-solid fa-user")
            });
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, true);
        });

        Assert.Equal("P", cut.Find("a[aria-label='Parent'] span.menu-icon").TextContent);
        Assert.NotNull(cut.Find("a[aria-label='Child'] i.menu-icon"));
        Assert.Contains("fa-user", cut.Find("a[aria-label='With icon'] i.menu-icon").ClassList);

        cut.Render(pb => pb.Add(m => m.IsCollapsed, false));
        Assert.Empty(cut.FindAll("span.menu-icon"));
        Assert.Equal(3, cut.FindAll("i.menu-icon").Count);
    }

    [Fact]
    public void CollapsedSubMenuPopup_RendersNestedAndDisabledItems()
    {
        var items = new[]
        {
            new MenuItem("Single") { Items = [new("Only child")] },
            new MenuItem("Multiple")
            {
                Items =
                [
                    new("Nested") { Items = [new("Nested child")] },
                    new("Other child")
                ]
            },
            new MenuItem("Disabled") { IsDisabled = true, Items = [new("Hidden child")] },
            new MenuItem("Leaf"),
            new MenuItem("Disabled leaf") { IsDisabled = true }
        };
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Id, "test-menu");
            pb.Add(m => m.Items, items);
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
            pb.Add(m => m.IsAccordion, true);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, true);
        });

        var root = cut.Find("ul.submenu");
        Assert.Equal("menu", root.GetAttribute("role"));
        var singleLink = cut.Find("a[aria-label='Single']");
        var singleId = singleLink.GetAttribute("data-bb-menu-popup");
        Assert.NotNull(singleId);
        Assert.StartsWith("test-menu_", singleId);
        Assert.Equal(singleId, singleLink.GetAttribute("aria-controls"));
        Assert.Equal("menu", singleLink.GetAttribute("aria-haspopup"));
        Assert.Equal("false", singleLink.GetAttribute("aria-expanded"));
        Assert.Equal("none", singleLink.Closest("li")?.GetAttribute("role"));
        Assert.Null(singleLink.GetAttribute("data-bs-toggle"));
        Assert.Null(singleLink.GetAttribute("data-bs-target"));
        Assert.Equal("S", singleLink.QuerySelector(".menu-icon")?.TextContent);

        var singlePopup = cut.Find($"div.menu-popup-single[id='{singleId}']");
        Assert.Equal("manual", singlePopup.GetAttribute("popover"));
        Assert.Equal("Single", singlePopup.QuerySelector("ul[role='menu']")?.GetAttribute("aria-label"));
        Assert.NotNull(singlePopup.QuerySelector(".menu-popup-arrow"));

        var multipleLink = cut.Find("a[aria-label='Multiple']");
        var multipleId = multipleLink.GetAttribute("aria-controls");
        var multiplePopup = cut.Find($"ul.menu-popup[id='{multipleId}']");
        Assert.Equal("manual", multiplePopup.GetAttribute("popover"));
        Assert.Equal("Multiple", multiplePopup.GetAttribute("aria-label"));
        Assert.False(multiplePopup.ClassList.Contains("collapse"));
        Assert.Null(multiplePopup.GetAttribute("data-bs-parent"));

        var nestedLink = multiplePopup.QuerySelector("a[aria-label='Nested']");
        Assert.NotNull(nestedLink);
        var nestedId = nestedLink.GetAttribute("aria-controls");
        Assert.Equal(nestedId, nestedLink.GetAttribute("data-bb-menu-popup"));
        Assert.NotEqual(singleId, nestedId);
        Assert.NotNull(cut.Find($"div.menu-popup-single[id='{nestedId}']"));

        var disabled = cut.Find(".nav-link.disabled[aria-label='Disabled']");
        Assert.Equal("true", disabled.GetAttribute("aria-disabled"));
        Assert.Null(disabled.GetAttribute("aria-controls"));
        Assert.DoesNotContain("Hidden child", cut.Markup);
        Assert.Equal("Leaf", cut.Find("a[aria-label='Leaf']").GetAttribute("data-bb-menu-tooltip"));
        Assert.Null(cut.Find(".nav-link.disabled[aria-label='Disabled leaf']").GetAttribute("data-bb-menu-tooltip"));
        Assert.Null(cut.Find("a[aria-label='Other child']").GetAttribute("data-bb-menu-tooltip"));
    }

    [Fact]
    public void CollapsedSubMenuPopup_UsesUniqueTargetsForEachMenu()
    {
        var items = new[] { new MenuItem("Parent") { Items = [new("Child")] } };
        var first = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Id, "first");
            pb.Add(m => m.Items, items);
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, true);
        });
        var second = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Id, "second");
            pb.Add(m => m.Items, items);
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, true);
        });

        var firstId = first.Find("a[aria-label='Parent']").GetAttribute("aria-controls");
        var secondId = second.Find("a[aria-label='Parent']").GetAttribute("aria-controls");
        Assert.NotEqual(firstId, secondId);
        Assert.NotNull(first.Find($"[id='{firstId}'][popover='manual']"));
        Assert.NotNull(second.Find($"[id='{secondId}'][popover='manual']"));
    }

    [Fact]
    public void CollapsedSubMenuPopup_ToggleAndClick()
    {
        var child = new MenuItem("Child");
        var disabled = new MenuItem("Disabled") { IsDisabled = true };
        var parent = new MenuItem("Parent") { IsCollapsed = false, Items = [child, disabled] };
        MenuItem? clicked = null;
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new[] { parent });
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsAccordion, true);
            pb.Add(m => m.DisableNavigation, true);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, true);
            pb.Add(m => m.OnClick, item =>
            {
                clicked = item;
                return Task.CompletedTask;
            });
        });

        var expandedLink = cut.Find("a.nav-link");
        var target = expandedLink.GetAttribute("data-bs-target");
        Assert.NotNull(target);
        Assert.True(cut.Find(target).ClassList.Contains("show"));
        Assert.NotNull(cut.Find(target).GetAttribute("data-bs-parent"));

        cut.Render(pb => pb.Add(m => m.IsCollapsed, true));
        var popupLink = cut.Find("a[aria-label='Parent']");
        Assert.Equal(target[1..], popupLink.GetAttribute("aria-controls"));
        Assert.Null(cut.Find(target).GetAttribute("data-bs-parent"));
        Assert.False(cut.Find(target).ClassList.Contains("show"));
        cut.Find(".nav-link.disabled[aria-label='Disabled']").Closest("li")!.Click();
        Assert.Null(clicked);
        cut.Find("a[aria-label='Child']").Closest("li")!.Click();
        Assert.Same(child, clicked);
        Assert.True(child.IsActive);
        Assert.False(parent.IsActive);

        cut.Render(pb => pb.Add(m => m.IsCollapsed, false));
        expandedLink = cut.Find("a.nav-link");
        Assert.Equal(target, expandedLink.GetAttribute("data-bs-target"));
        Assert.Equal("collapse", expandedLink.GetAttribute("data-bs-toggle"));
        Assert.True(cut.Find(target).ClassList.Contains("show"));
        Assert.NotNull(cut.Find(target).GetAttribute("data-bs-parent"));
        Assert.Empty(cut.FindAll("[popover]"));
    }

    [Theory]
    [InlineData(false, false, false)]
    [InlineData(false, true, false)]
    [InlineData(true, false, true)]
    [InlineData(true, true, false)]
    public void VerticalMenu_ClickCascadesActiveOnlyWhenCollapsedWithoutPopup(bool isCollapsed, bool enablePopup, bool cascades)
    {
        var child = new MenuItem("Child");
        var parent = new MenuItem("Parent") { Items = [child] };
        var grandparent = new MenuItem("Grandparent") { Items = [parent] };
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new[] { grandparent });
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, isCollapsed);
            pb.Add(m => m.EnableCollapsedSubMenuPopup, enablePopup);
            pb.Add(m => m.DisableNavigation, true);
        });

        cut.FindAll("li").Last().Click();

        Assert.True(child.IsActive);
        Assert.Equal(cascades, parent.IsActive);
        Assert.Equal(cascades, grandparent.IsActive);
        Assert.Equal(!cascades, parent.IsCollapsed);
        Assert.Equal(!cascades, grandparent.IsCollapsed);
    }

    [Fact]
    public void IsBottom_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsBottom, true);
        });

        Assert.Contains("is-bottom", cut.Markup);
    }

    [Fact]
    public void CssClass_Ok()
    {
        var items = new List<MenuItem>
        {
            new() { Text = "Menu", CssClass="Test-Class" }
        };
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, items);
        });

        Assert.Contains("Test-Class", cut.Markup);
    }

    [Fact]
    public void IndentSize_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IndentSize, 32);
        });
        Assert.DoesNotContain("padding-left: 32px;", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("padding-left: 32px;", cut.Markup);
    }

    [Fact]
    public void IsDisabled_Ok()
    {
        var items = new List<MenuItem>()
        {
            new("Menu1")
            {
                Icon = "fa-solid fa-font-awesome",
                Url = "https://www.blazor.zone",
                Items = new List<MenuItem>()
                {
                    new("Menu2")
                    {
                        Icon = "fa-solid fa-fw fa-font-awesome",
                        Items = new List<MenuItem>()
                        {
                            new("Menu3")
                            {
                                IsActive = true,
                                IsDisabled = true,
                                Icon = "fa-solid fa-fw fa-font-awesome"
                            }
                        }
                    }
                }
            },
        };
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, items);
        });
        Assert.Contains("disabled", cut.Markup);
        Assert.DoesNotContain("active", cut.Markup);
    }

    [Fact]
    public void IsAccordion_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsAccordion, true);
        });
        Assert.DoesNotContain("accordion", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        cut.WaitForAssertion(() => cut.Contains("accordion"));
    }

    [Fact]
    public void IsExpandAll_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsExpandAll, true);
        });
        Assert.DoesNotContain("data-bb-expand", cut.Markup);

        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsExpandAll, false);
        });
        cut.WaitForAssertion(() => cut.DoesNotContain("data-bb-expand"));

        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsExpandAll, true);
        });
        cut.WaitForAssertion(() => cut.Contains("data-bb-expand=\"true\""));
    }

    [Fact]
    public void OnClick_Ok()
    {
        var clicked = false;
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
        });

        // 子菜单 Click 触发
        var div = cut.Find(".nav-item");
        div.Click();
        cut.WaitForAssertion(() => div.ClassList.Contains("active"));

        // 查找第一个 li 节点
        var li = cut.Find("li");
        li.Click();
        cut.WaitForAssertion(() => li.ClassList.Contains("active"));

        cut.Render(pb =>
        {
            pb.Add(m => m.OnClick, item =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });

        li = cut.Find("li");
        li.Click();
        cut.WaitForAssertion(() => Assert.True(clicked));

        // SubMenu Click
        var sub = cut.Find(".sub-menu div.nav-item");
        sub.Click();

        var subs = cut.FindAll(".sub-menu div.nav-item");
        sub = subs[subs.Count - 1];
        sub.Click();

        // 设置禁止导航 
        // 顶栏模式
        cut.Render(pb =>
        {
            pb.Add(m => m.DisableNavigation, true);
        });

        li = cut.Find("li");
        li.Click();
        cut.WaitForAssertion(() => Assert.True(clicked));

        // 再次点击
        li = cut.Find("li");
        li.Click();

        // 侧边栏模式
        cut.Render(pb =>
        {
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
        });

        // 再次点击
        li = cut.Find("li");
        li.Click();
        cut.WaitForAssertion(() => Assert.True(clicked));

        // 再次点击
        li = cut.Find("li");
        li.Click();
        cut.WaitForAssertion(() => li.ClassList.Contains("active"));
    }

    [Fact]
    public void SubMenu_ClassString_Ok()
    {
        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        nav.NavigateTo("/menu2321");
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
        });
        var item = cut.Find("[href=\"Menu2321\"]");
        Assert.NotNull(item);
        var li = item.Closest("li");
        Assert.NotNull(li);
    }

    [Fact]
    public void IsScrollIntoView_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.IsVertical, false);
            pb.Add(m => m.IsScrollIntoView, true);
            pb.Add(m => m.Items, Items);
        });
        cut.DoesNotContain("data-bb-scroll-view");

        cut.Render(pb =>
        {
            pb.Add(a => a.IsVertical, true);
        });
        cut.WaitForAssertion(() => cut.Contains("data-bb-scroll-view"));
    }

    [Fact]
    public void GetAllSubItems_Ok()
    {
        var item = new MenuItem("Test");
        var subs = item.GetAllSubItems();
        Assert.Empty(subs.ToList());

        item = new MenuItem("Test")
        {
            Items = new[]
            {
                new MenuItem()
                {
                    Text = "Test1",
                    Items = new List<MenuItem>()
                    {
                        new("Test11"),
                        new("Test12")
                    }
                }
            }
        };
        subs = item.GetAllSubItems();
        Assert.NotEmpty(subs.ToList());
    }

    [Fact]
    public void MenuLink_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<MenuLink>());
    }

    [Fact]
    public void TopMenu_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<TopMenu>());
    }

    [Fact]
    public void SideMenu_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<SideMenu>());
    }

    [Fact]
    public void SubMenu_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.Render<SubMenu>());
    }

    [Fact]
    public void MenuItem_Parent()
    {
        var parent = new MenuItem()
        {
            Id = "01",
            Text = "Test01"
        };

        var item = new MenuItem()
        {
            Id = "01",
            Text = "Test01",
            Parent = parent
        };

        item.CascadingSetActive(true);
        Assert.True(item.IsActive);
        Assert.True(parent.IsActive);
    }

    [Fact]
    public void ActiveItem_Ok()
    {
        var cut = Context.Render<Menu>(pb =>
        {
            pb.Add(m => m.Items, new MenuItem[]
            {
                new("Menu1")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "/menu22"
                },
                new("Menu2")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "/menu23"
                }
            });
        });
        cut.DoesNotContain("<a href=\"menu22\" class=\"nav-link active\">");

        // 设置 后通过菜单激活 ActiveItem 不为空
        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        nav.NavigateTo("/menu22");
        cut.Render();
        cut.Contains("href=\"menu22\"");

        nav.NavigateTo("/menu3");
        cut.Render(pb =>
        {
            pb.Add(a => a.IsVertical, true);
            pb.Add(m => m.Items, new MenuItem[]
            {
                new("Menu1")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "/menu1",
                    IsCollapsed = false,
                    Items = new MenuItem[]
                    {
                        new("Menu2")
                        {
                            Icon = "fa-solid fa-font-awesome",
                            Url = "/menu2",
                            Items = new MenuItem[]
                            {
                                new("Menu3")
                                {
                                    Icon = "fa-solid fa-fa",
                                    Url = "/menu3",
                                }
                            }
                        }
                    }
                },
            });
        });
        var menus = cut.FindAll("[aria-expanded=\"true\"]");
        Assert.Equal(2, menus.Count);

        nav.NavigateTo("/menu1#Normal");
        cut.Render(pb =>
        {
            pb.Add(m => m.Items, new MenuItem[]
            {
                new("Menu1")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "/menu1",
                },
                 new("Menu2")
                {
                    Icon = "fa-solid fa-font-awesome",
                    Url = "/menu2",
                },
           });
        });
        cut.InvokeAsync(() =>
        {
            var link = cut.Find(".nav-link.active");
            Assert.Contains("href=\"menu1\"", link.OuterHtml);
        });

        nav.NavigateTo("/menu2?id=Normal");
        cut.Render();
        cut.InvokeAsync(() =>
        {
            var link = cut.Find(".nav-link.active");
            Assert.Contains("href=\"menu2\"", link.OuterHtml);
        });
    }
}
