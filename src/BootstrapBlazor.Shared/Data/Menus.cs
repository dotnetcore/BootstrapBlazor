// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

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
                new(localizer["Menu1"].Value, icon:"fa fa-life-bouy"),
                new(localizer["Menu2"].Value, icon:"fa fa-fa")
                {
                    IsActive = true,
                    Items = new List<MenuItem>
                    {
                        new(localizer["SubMenu1"].Value, icon:"fa fa-fa"),
                        new(localizer["SubMenu2"].Value, icon:"fa fa-fa"),
                        new(localizer["SubMenu3"].Value, icon:"fa fa-fa"),
                    }
                },
                new(localizer["Menu3"].Value, icon:"fa fa-rebel fa-fw")
            };
    }

    public static async Task<IEnumerable<MenuItem>> GetSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);

        return new List<MenuItem>
            {
                new(localizer["Menu1"].Value, icon: "fa fa-fa"),
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
            var index = 0;
            builder.AddContent(index++, "10");
        })
    });

    public static async Task<IEnumerable<MenuItem>> GetIconSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
            {
                new(localizer["System"].Value, icon: "fa fa-gears")
                {
                    IsActive = true,
                    Items = new List<MenuItem>
                    {
                        new(localizer["Website"].Value, icon: "fa fa-fa"),
                        new(localizer["Task"].Value, icon: "fa fa-tasks")
                    }
                },
                new(localizer["Authorize"].Value, icon: "fa fa-users")
                {
                    Items = new List<MenuItem>
                    {
                        new(localizer["User"].Value, icon: "fa fa-user"),
                        new(localizer["Menu"].Value, icon: "fa fa-dashboard"),
                        new(localizer["Role"].Value, icon: "fa fa-sitemap")
                    }
                },
                new(localizer["Log"].Value, icon: "fa fa-database")
                {
                    Items = new List<MenuItem>
                    {
                        new(localizer["Access"].Value, icon: "fa fa-bars"),
                        new(localizer["Login"].Value, icon: "fa fa-user-circle-o"),
                        new(localizer["Operation"].Value, icon: "fa fa-edit")
                    }
                }
            };
    }

    public static async Task<IEnumerable<MenuItem>> GetWidgetIconSideMenuItemsAsync(IStringLocalizer localizer)
    {
        await Task.Delay(1);
        return new List<MenuItem>
            {
                new(localizer["System"].Value, icon: "fa fa-gears")
                {
                    IsActive = true,
                    Items = new List<MenuItem>
                    {
                        new(localizer["Website"].Value, icon: "fa fa-fa"),
                        new(localizer["Task"].Value, icon: "fa fa-tasks")
                    }
                },
                new(localizer["Authorize"].Value, icon: "fa fa-users")
                {
                    Items = new List<MenuItem>
                    {
                        new(localizer["User"].Value, icon: "fa fa-user"),
                        new(localizer["Menu"].Value, icon: "fa fa-dashboard"),
                        new(localizer["Role"].Value, icon: "fa fa-sitemap")
                    }
                },
                new(localizer["Log"].Value, icon: "fa fa-database")
                {
                    Template = BuildDynamicComponent().Render(),
                    Items = new List<MenuItem>
                    {
                        new(localizer["Access"].Value, icon: "fa fa-bars"),
                        new(localizer["Login"].Value, icon: "fa fa-user-circle-o"),
                        new(localizer["Operation"].Value, icon: "fa fa-edit")
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
                new(localizer["System"].Value, icon: "fa fa-gears")
                {
                    IsActive = true,
                    Items = new List<MenuItem>
                    {
                        new(localizer["Website"].Value, icon: "fa fa-fa"),
                        new(localizer["Task"].Value, icon: "fa fa-tasks")
                    }
                },
                new(localizer["Authorize"].Value, icon: "fa fa-users")
                {
                    IsCollapsed = false,
                    Items = new List<MenuItem>
                    {
                        new(localizer["User"].Value, icon: "fa fa-user"),
                        new(localizer["Menu"].Value, icon: "fa fa-dashboard"),
                        new(localizer["Role"].Value, icon: "fa fa-sitemap")
                    }
                },
                new(localizer["Log"].Value, icon: "fa fa-database")
                {
                    Items = new List<MenuItem>
                    {
                        new(localizer["Access"].Value, icon: "fa fa-bars"),
                        new(localizer["Login"].Value, icon: "fa fa-user-circle-o"),
                        new(localizer["Operation"].Value, icon: "fa fa-edit")
                    }
                }
            };
    }
}
