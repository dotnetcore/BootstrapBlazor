// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Menus
    {
        [NotNull]
        private BlockLogger? Trace { get; set; }

        [NotNull]
        private BlockLogger? Trace2 { get; set; }

        [NotNull]
        private BlockLogger? TraceSideMenu { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? Items { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? BottomItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? IconItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? SideMenuItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? IconSideMenuItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? WidgetIconSideMenuItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? CollapsedIconSideMenuItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? DisabledMenuItems { get; set; }

        [NotNull]
        private IEnumerable<MenuItem>? DynamicSideMenuItems { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Menus>? Localizer { get; set; }

        private Task OnClickMenu(MenuItem item)
        {
            Trace.Log($"菜单点击项: {item.Text}");
            return Task.CompletedTask;
        }

        private string? ClickedMenuItemText { get; set; }

        private Task OnClickBottomMenu(MenuItem item)
        {
            ClickedMenuItemText = item.Text;
            StateHasChanged();
            return Task.CompletedTask;
        }

        private Task OnClick2(MenuItem item)
        {
            Trace2.Log($"菜单点击项: {item.Text}");
            return Task.CompletedTask;
        }

        private Task OnClickSideMenu(MenuItem item)
        {
            TraceSideMenu?.Log($"菜单点击项: {item.Text}");
            return Task.CompletedTask;
        }

        private bool IsCollapsed { get; set; }

        private string? ClassString => CssBuilder.Default("menu-demo-bar")
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

        private Task CollapseMenu()
        {
            IsCollapsed = !IsCollapsed;
            return Task.CompletedTask;
        }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            Items = await MenusDataGerator.GetTopItemsAsync(Localizer);
            IconItems = await MenusDataGerator.GetTopIconItemsAsync(Localizer);
            SideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
            IconSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
            WidgetIconSideMenuItems = await MenusDataGerator.GetWidgetIconSideMenuItemsAsync(Localizer);
            CollapsedIconSideMenuItems = await MenusDataGerator.GetCollapsedIconSideMenuItemsAsync(Localizer);
            DisabledMenuItems = await MenusDataGerator.GetDisabledMenuItemsAsync(Localizer);
            DynamicSideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
            BottomItems = await MenusDataGerator.GetBottomMenuItemsAsync(Localizer);
        }

        private async Task UpdateMenu()
        {
            DynamicSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
        }

        private async Task ResetMenu()
        {
            DynamicSideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
        }

        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            new AttributeItem()
            {
                Name = "Items",
                Description = "菜单组件数据集合",
                Type = "IEnumerable<MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "IsVertical",
                Description = "是否为侧栏",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "IsBottom",
                Description = "是否为底栏",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAccordion",
                Description = "是否手风琴效果",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "DisableNavigation",
                Description = "是否禁止地址栏导航",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnClick",
                Description = "菜单项被点击时回调此方法",
                Type = "Func<MenuItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

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

            private static BootstrapDynamicComponent BuildDynamicComponent() => BootstrapDynamicComponent.CreateComponent<Badge>(new KeyValuePair<string, object>[]
            {
                new(nameof(Badge.Color), Color.Danger),
                new(nameof(Badge.IsPill), true),
                new(nameof(Badge.ChildContent), new RenderFragment(builder =>
                {
                    var index = 0;
                    builder.AddContent(index++, "10");
                }))
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
                        Component = BuildDynamicComponent(),
                        Items = new List<MenuItem>
                        {
                            new(localizer["Access"].Value, icon: "fa fa-bars"),
                            new(localizer["Login"].Value, icon: "fa fa-user-circle-o"),
                            new(localizer["Operation"].Value, icon: "fa fa-edit")
                            {
                                Component = BuildDynamicComponent()
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
    }
}
