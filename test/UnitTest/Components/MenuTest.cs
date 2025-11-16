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
