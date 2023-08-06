// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class MenuTest : BootstrapBlazorTestBase
{
    private List<MenuItem> Items { get; set; }

    public MenuTest() => Items = new List<MenuItem>
    {
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
                                new MenuItem("Menu2321")
                                {
                                    Icon = "fa-solid fa-font-awesome",
                                    Url = "/Menu2321"
                                },
                                new MenuItem("Menu2322")
                                {
                                    Icon = "fa-solid fa-fw fa-font-awesome"
                                },
                                new MenuItem("Menu2323")
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
                        new MenuItem("Menu251")
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
                new MenuItem("Menu31")
            }
        },
        new("Menu4")
        {
            IsActive = true,
            Icon = "fa-solid fa-fw fa-font-awesome",
            Url = "https://www.blazor.zone"
        }
    };

    [Fact]
    public void Items_Ok()
    {
        // 未设置 Items
        var cut = Context.RenderComponent<Menu>();
        Assert.DoesNotContain("Menu1", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.Items, Items);
        });

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("Menu1", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.Items, null);
        });
        Assert.Contains("submenu", cut.Markup);
    }

    [Fact]
    public void DisableNavigation_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
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
        menuItems.Click(new MouseEventArgs());
    }

    [Fact]
    public void IsVertical_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsVertical, false);
        });
        Assert.DoesNotContain("is-vertical", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("is-vertical", cut.Markup);

        // 垂直布局时设置手风琴效果触发 ShouldInvoke
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsAccordion, true);
        });
        Assert.Contains("accordion", cut.Markup);
    }

    [Fact]
    public void IsBottom_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
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
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, items);
        });

        Assert.Contains("Test-Class", cut.Markup);
    }

    [Fact]
    public void IndentSize_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IndentSize, 32);
        });
        Assert.DoesNotContain("padding-left: 32px;", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("padding-left: 32px;", cut.Markup);
    }

    [Fact]
    public void IsCollapsed_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsCollapsed, true);
        });
        Assert.DoesNotContain("is-collapsed", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("is-collapsed", cut.Markup);
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
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, items);
        });
        Assert.Contains("disabled", cut.Markup);
        Assert.DoesNotContain("active", cut.Markup);
    }

    [Fact]
    public void IsAccordion_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsAccordion, true);
        });
        Assert.DoesNotContain("accordion", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
        });
        Assert.Contains("accordion", cut.Markup);
    }

    [Fact]
    public void IsExpandAll_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
            pb.Add(m => m.IsExpandAll, true);
        });
        Assert.DoesNotContain("data-bb-expand", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsExpandAll, false);
        });
        Assert.DoesNotContain("data-bb-expand", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsExpandAll, true);
        });
        Assert.Contains("data-bb-expand=\"true\"", cut.Markup);
    }

    [Fact]
    public void OnClick_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
        });

        // 子菜单 Click 触发
        var div = cut.Find(".nav-item");
        div.Click(new MouseEventArgs());

        // 查找第一个 li 节点
        var menuItems = cut.Find("li");
        menuItems.Click(new MouseEventArgs());

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.OnClick, item =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        menuItems.Click(new MouseEventArgs());
        Assert.True(clicked);

        // SubMenu Click
        var sub = cut.Find(".sub-menu div.nav-item");
        sub.Click(new MouseEventArgs());

        var subs = cut.FindAll(".sub-menu div.nav-item");
        sub = subs[subs.Count - 1];
        sub.Click(new MouseEventArgs());

        // 设置禁止导航 
        // 顶栏模式
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.DisableNavigation, true);
        });
        menuItems.Click(new MouseEventArgs());
        Assert.True(clicked);

        // 再次点击
        menuItems.Click(new MouseEventArgs());

        // 侧边栏模式
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(m => m.IsVertical, true);
            pb.Add(m => m.IsCollapsed, true);
        });
        menuItems.Click(new MouseEventArgs());
        Assert.True(clicked);

        // 再次点击
        menuItems.Click(new MouseEventArgs());
    }

    [Fact]
    public void SubMenu_ClassString_Ok()
    {
        var nav = Context.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/menu2321");
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.Items, Items);
        });
    }

    [Fact]
    public void IsScrollIntoView_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
        {
            pb.Add(m => m.IsVertical, false);
            pb.Add(m => m.IsScrollIntoView, true);
            pb.Add(m => m.Items, Items);
        });
        cut.DoesNotContain("data-bb-scroll-view");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsVertical, true);
        });
        cut.Contains("data-bb-scroll-view");
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
                        new MenuItem("Test11"),
                        new MenuItem("Test12")
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
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<MenuLink>());
    }

    [Fact]
    public void TopMenu_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<TopMenu>());
    }

    [Fact]
    public void SideMenu_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<SideMenu>());
    }

    [Fact]
    public void SubMenu_Error()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<SubMenu>());
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
}

public class MenuItemTest_Ok : DialogTestBase
{
    [Fact]
    public void ActiveItem_Ok()
    {
        var cut = Context.RenderComponent<Menu>(pb =>
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
        var nav = Context.Services.GetRequiredService<FakeNavigationManager>();
        nav.NavigateTo("/menu22");
        cut.SetParametersAndRender();
        cut.Contains("<a href=\"menu22\" aria-expanded=\"false\" class=\"nav-link active\">");

        nav.NavigateTo("/menu3");
        cut.SetParametersAndRender(pb =>
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
        cut.SetParametersAndRender(pb =>
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
        cut.SetParametersAndRender();
        cut.InvokeAsync(() =>
        {
            var link = cut.Find(".nav-link.active");
            Assert.Contains("href=\"menu2\"", link.OuterHtml);
        });
    }
}
