// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared;

/// <summary>
/// 
/// </summary>
internal static class MenusDataGerator
{
    public static async Task<IEnumerable<MenuItem>> GetTopItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);

        return new List<MenuItem>
        {
            new(localizer["Menu1"].Value),
            new(localizer["Menu2"].Value)
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu1"].Value)
                    {
                        Items = new List<MenuItem>
                        {
                            new(localizer["SubMenu11"].Value),
                            new(localizer["SubMenu12"].Value)
                        }
                    },
                    new(localizer["SubMenu2"].Value)
                    {
                        Items = new List<MenuItem>
                        {
                            new(localizer["SubMenu21"].Value),
                            new(localizer["SubMenu22"].Value)
                            {
                                Items = new List<MenuItem>
                                {
                                    new(localizer["SubMenu31"].Value),
                                    new(localizer["SubMenu32"].Value)
                                    {
                                        Items = new List<MenuItem>
                                        {
                                            new(localizer["SubMenu41"].Value),
                                            new(localizer["SubMenu42"].Value)
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new(localizer["SubMenu3"].Value)
                }
            },
            new(localizer["Menu3"].Value)
        };
    }

    public static async Task<IEnumerable<MenuItem>> GetBottomMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);

        return new List<MenuItem>
        {
            new(localizer["Menu1"].Value),
            new(localizer["Menu2"].Value)
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu1"].Value),
                    new(localizer["SubMenu2"].Value),
                    new(localizer["SubMenu3"].Value)
                }
            },
            new(localizer["Menu3"].Value)
        };
    }

    public static async Task<IEnumerable<MenuItem>> GetTopIconItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
        {
            new(localizer["Menu1"].Value, icon:"fa-solid fa-life-ring"),
            new(localizer["Menu2"].Value, icon:"fa-solid fa-font-awesome")
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu1"].Value, icon:"fa-solid fa-font-awesome"),
                    new(localizer["SubMenu2"].Value, icon:"fa-solid fa-font-awesome"),
                    new(localizer["SubMenu3"].Value, icon:"fa-solid fa-font-awesome"),
                }
            },
            new(localizer["Menu3"].Value, icon:"fa-brands fa-rebel fa-fw")
        };
    }

    public static async Task<IEnumerable<MenuItem>> GetSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);

        return new List<MenuItem>
        {
            new(localizer["Menu1"].Value, icon: "fa-solid fa-font-awesome"),
            new(localizer["Menu2"].Value)
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu1"].Value)
                    {
                        Items = new List<MenuItem>
                        {
                            new(localizer["SubMenu11"].Value),
                            new(localizer["SubMenu12"].Value)
                        }
                    },
                    new(localizer["SubMenu2"].Value)
                    {
                        Items = new List<MenuItem>
                        {
                            new(localizer["SubMenu21"].Value),
                            new(localizer["SubMenu22"].Value)
                            {
                                Items = new List<MenuItem>
                                {
                                    new(localizer["SubMenu31"].Value),
                                    new(localizer["SubMenu32"].Value)
                                    {
                                        Items = new List<MenuItem>
                                        {
                                            new(localizer["SubMenu41"].Value),
                                            new(localizer["SubMenu42"].Value)
                                        }
                                    }
                                }
                            }
                        }
                    },
                    new(localizer["SubMenu3"].Value)
                }
            },
            new(localizer["Menu3"].Value)
        };
    }

    public static async Task<IEnumerable<MenuItem>> GetDisabledMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
        {
            new(localizer["Menu1"].Value)
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu1"].Value)
                }
            },
            new(localizer["Menu2"].Value)
            {
                IsDisabled = true,
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu2"].Value)
                }
            },
            new(localizer["Menu3"].Value)
            {
                Items = new List<MenuItem>
                {
                    new(localizer["SubMenu3"].Value)
                }
            }
        };
    }

    private static BootstrapDynamicComponent BuildDynamicComponent() => BootstrapDynamicComponent.CreateComponent<Badge>(new Dictionary<string, object?>
    {
        [nameof(Badge.Color)] = Color.Danger,
        [nameof(Badge.IsPill)] = true,
        [nameof(Badge.ChildContent)] = new RenderFragment(builder =>
        {
            builder.AddContent(0, "10");
        })
    });

    public static async Task<IEnumerable<MenuItem>> GetIconSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
        {
            new(localizer["System"].Value, icon: "fa-solid fa-gears")
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["Website"].Value, icon: "fa-solid fa-font-awesome"),
                    new(localizer["Task"].Value, icon: "fa-solid fa-bars-progress")
                }
            },
            new(localizer["Authorize"].Value, icon: "fa-solid fa-users")
            {
                Items = new List<MenuItem>
                {
                    new(localizer["User"].Value, icon: "fa-solid fa-user"),
                    new(localizer["Menu"].Value, icon: "fa-solid fa-gauge-high"),
                    new(localizer["Role"].Value, icon: "fa-solid fa-sitemap")
                }
            },
            new(localizer["Log"].Value, icon: "fa-solid fa-database")
            {
                Items = new List<MenuItem>
                {
                    new(localizer["Access"].Value, icon: "fa-solid fa-bars"),
                    new(localizer["Login"].Value, icon: "fa-regular fa-circle-user"),
                    new(localizer["Operation"].Value, icon: "fa-solid fa-pen")
                }
            }
        };
    }

    public static async Task<IEnumerable<MenuItem>> GetWidgetIconSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
        {
            new(localizer["System"].Value, icon: "fa-solid fa-gears")
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["Website"].Value, icon: "fa-solid fa-font-awesome"),
                    new(localizer["Task"].Value, icon: "fa-solid fa-bars-progress")
                }
            },
            new(localizer["Authorize"].Value, icon: "fa-solid fa-users")
            {
                Items = new List<MenuItem>
                {
                    new(localizer["User"].Value, icon: "fa-solid fa-user"),
                    new(localizer["Menu"].Value, icon: "fa-solid fa-gauge-high"),
                    new(localizer["Role"].Value, icon: "fa-solid fa-sitemap")
                }
            },
            new(localizer["Log"].Value, icon: "fa-solid fa-database")
            {
                Template = BuildDynamicComponent().Render(),
                Items = new List<MenuItem>
                {
                    new(localizer["Access"].Value, icon: "fa-solid fa-bars"),
                    new(localizer["Login"].Value, icon: "fa-regular fa-circle-user"),
                    new(localizer["Operation"].Value, icon: "fa-solid fa-pen")
                    {
                        Template = BuildDynamicComponent().Render()
                    }
                }
            }
        };
    }

    public static async Task<IEnumerable<MenuItem>> GetCollapsedIconSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
        {
            new(localizer["System"].Value, icon: "fa-solid fa-gears")
            {
                IsActive = true,
                Items = new List<MenuItem>
                {
                    new(localizer["Website"].Value, icon: "fa-solid fa-font-awesome"),
                    new(localizer["Task"].Value, icon: "fa-solid fa-bars-progress")
                }
            },
            new(localizer["Authorize"].Value, icon: "fa-solid fa-users")
            {
                IsCollapsed = false,
                Items = new List<MenuItem>
                {
                    new(localizer["User"].Value, icon: "fa-solid fa-user"),
                    new(localizer["Menu"].Value, icon: "fa-solid fa-gauge-high"),
                    new(localizer["Role"].Value, icon: "fa-solid fa-sitemap")
                }
            },
            new(localizer["Log"].Value, icon: "fa-solid fa-database")
            {
                Items = new List<MenuItem>
                {
                    new(localizer["Access"].Value, icon: "fa-solid fa-bars"),
                    new(localizer["Login"].Value, icon: "fa-regular fa-circle-user"),
                    new(localizer["Operation"].Value, icon: "fa-solid fa-pen")
                }
            }
        };
    }
}
