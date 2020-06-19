using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Menus
    {
        /// <summary>
        /// 
        /// </summary>
        private Logger? Trace { get; set; }

        private Task OnClickMenu(MenuItem item)
        {
            Trace?.Log($"菜单点击项: {item.Text}");
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        private Logger? TraceSideMenu { get; set; }

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

        private IEnumerable<MenuItem> GetItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "导航一" },
                new MenuItem() { Text = "导航二", IsActive = true },
                new MenuItem() { Text = "导航三" }
            };

            ret[1].AddItem(new MenuItem() { Text = "子菜单一" });
            ret[1].AddItem(new MenuItem() { Text = "子菜单二" });
            ret[1].AddItem(new MenuItem() { Text = "子菜单三" });

            ret[1].Items.ElementAt(0).AddItem(new MenuItem() { Text = "孙菜单1一" });
            ret[1].Items.ElementAt(0).AddItem(new MenuItem() { Text = "孙菜单1二" });

            ret[1].Items.ElementAt(1).AddItem(new MenuItem() { Text = "孙菜单2一" });
            ret[1].Items.ElementAt(1).AddItem(new MenuItem() { Text = "孙菜单2二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new MenuItem() { Text = "曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).AddItem(new MenuItem() { Text = "曾孙菜单二" });

            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new MenuItem() { Text = "曾曾孙菜单一" });
            ret[1].Items.ElementAt(1).Items.ElementAt(1).Items.ElementAt(1).AddItem(new MenuItem() { Text = "曾曾孙菜单二" });

            return ret;
        }

        private IEnumerable<MenuItem> GetIconItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "导航一", Icon = "fa fa-life-bouy fa-fw" },
                new MenuItem() { Text = "导航二", Icon = "fa fa-fa fa-fw", IsActive = true },
                new MenuItem() { Text = "导航三", Icon = "fa fa-rebel fa-fw" }
            };

            ret[1].AddItem(new MenuItem() { Text = "子菜单一", Icon = "fa fa-fa fa-fw" });
            ret[1].AddItem(new MenuItem() { Text = "子菜单二", Icon = "fa fa-fa fa-fw" });
            ret[1].AddItem(new MenuItem() { Text = "子菜单三", Icon = "fa fa-fa fa-fw" });

            return ret;
        }

        private IEnumerable<MenuItem> GetSideMenuItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "导航一" },
                new MenuItem() { Text = "导航二" },
                new MenuItem() { Text = "导航三" },
                new MenuItem() { Text = "导航四" }
            };

            ret[1].AddItem(new MenuItem() { Text = "子菜单一" });
            ret[1].AddItem(new MenuItem() { Text = "子菜单二" });
            ret[1].AddItem(new MenuItem() { Text = "子菜单三" });

            ret[3].AddItem(new MenuItem() { Text = "子菜单一" });
            ret[3].AddItem(new MenuItem() { Text = "子菜单二" });
            ret[3].AddItem(new MenuItem() { Text = "子菜单三" });

            ret[1].Items.ElementAt(0).AddItem(new MenuItem() { Text = "孙菜单1一" });
            ret[1].Items.ElementAt(0).AddItem(new MenuItem() { Text = "孙菜单1二" });

            ret[1].Items.ElementAt(1).AddItem(new MenuItem() { Text = "孙菜单2一" });
            ret[1].Items.ElementAt(1).AddItem(new MenuItem() { Text = "孙菜单2二" });

            ret[1].Items.ElementAt(0).Items.ElementAt(0).AddItem(new MenuItem() { Text = "曾孙菜单一" });
            ret[1].Items.ElementAt(0).Items.ElementAt(0).AddItem(new MenuItem() { Text = "曾孙菜单二" });

            ret[1].Items.ElementAt(0).Items.ElementAt(0).Items.ElementAt(0).AddItem(new MenuItem() { Text = "曾曾孙菜单一" });
            ret[1].Items.ElementAt(0).Items.ElementAt(0).Items.ElementAt(0).AddItem(new MenuItem() { Text = "曾曾孙菜单二" });

            return ret;
        }

        private IEnumerable<MenuItem> GetIconSideMenuItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "系统设置", IsActive = true, Icon = "fa fa-fw fa-gears" },
                new MenuItem() { Text = "权限设置", Icon = "fa fa-fw fa-users" },
                new MenuItem() { Text = "日志设置", Icon = "fa fa-fw fa-database" }
            };

            ret[0].AddItem(new MenuItem() { Text = "网站设置", Icon = "fa fa-fw fa-fa" });
            ret[0].AddItem(new MenuItem() { Text = "任务设置", Icon = "fa fa-fw fa-tasks" });

            ret[1].AddItem(new MenuItem() { Text = "用户设置", Icon = "fa fa-fw fa-user" });
            ret[1].AddItem(new MenuItem() { Text = "菜单设置", Icon = "fa fa-fw fa-dashboard" });
            ret[1].AddItem(new MenuItem() { Text = "角色设置", Icon = "fa fa-fw fa-sitemap" });

            ret[2].AddItem(new MenuItem() { Text = "访问日志", Icon = "fa fa-fw fa-bars" });
            ret[2].AddItem(new MenuItem() { Text = "登录日志", Icon = "fa fa-fw fa-user-circle-o" });
            ret[2].AddItem(new MenuItem() { Text = "操作日志", Icon = "fa fa-fw fa-edit" });

            return ret;
        }

        private DynamicComponent BuildDynamicComponent()
        {
            return DynamicComponent.CreateComponent<Badge>(new KeyValuePair<string, object>[]
            {
                new KeyValuePair<string, object>(nameof(Badge.Color), Color.Danger),
                new KeyValuePair<string, object>(nameof(Badge.IsPill), true),
                new KeyValuePair<string, object>(nameof(Badge.ChildContent), new RenderFragment(builder =>
                {
                    var index = 0;
                    builder.AddContent(index++, "10");
                }))
            });
        }

        private IEnumerable<MenuItem> GetWidgetIconSideMenuItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "系统设置", Icon = "fa fa-fw fa-gears" },
                new MenuItem() { Text = "权限设置", Icon = "fa fa-fw fa-users" },
                new MenuItem() {
                    Text = "日志设置",
                    IsActive = true,
                    Icon = "fa fa-fw fa-database",
                    Component = BuildDynamicComponent()
                }
            };

            ret[0].AddItem(new MenuItem() { Text = "网站设置", Icon = "fa fa-fw fa-fa" });
            ret[0].AddItem(new MenuItem() { Text = "任务设置", Icon = "fa fa-fw fa-tasks" });

            ret[1].AddItem(new MenuItem() { Text = "用户设置", Icon = "fa fa-fw fa-user" });
            ret[1].AddItem(new MenuItem() { Text = "菜单设置", Icon = "fa fa-fw fa-dashboard" });
            ret[1].AddItem(new MenuItem() { Text = "角色设置", Icon = "fa fa-fw fa-sitemap" });

            ret[2].AddItem(new MenuItem() { Text = "访问日志", Icon = "fa fa-fw fa-bars" });
            ret[2].AddItem(new MenuItem() { Text = "登录日志", Icon = "fa fa-fw fa-user-circle-o" });
            ret[2].AddItem(new MenuItem()
            {
                Text = "操作日志",
                Icon = "fa fa-fw fa-edit",
                Component = BuildDynamicComponent()
            });

            return ret;
        }

        private IEnumerable<MenuItem> GetCollapsedIconSideMenuItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "系统设置", Icon = "fa fa-fw fa-gears" },
                new MenuItem() { Text = "权限设置", IsActive = true, Icon = "fa fa-fw fa-users" , IsCollapsed = false },
                new MenuItem() { Text = "日志设置", Icon = "fa fa-fw fa-database" }
            };

            ret[0].AddItem(new MenuItem() { Text = "网站设置", Icon = "fa fa-fw fa-fa" });
            ret[0].AddItem(new MenuItem() { Text = "任务设置", Icon = "fa fa-fw fa-tasks" });

            ret[1].AddItem(new MenuItem() { Text = "用户设置", Icon = "fa fa-fw fa-user" });
            ret[1].AddItem(new MenuItem() { Text = "菜单设置", Icon = "fa fa-fw fa-dashboard" });
            ret[1].AddItem(new MenuItem() { Text = "角色设置", Icon = "fa fa-fw fa-sitemap" });

            ret[2].AddItem(new MenuItem() { Text = "访问日志", Icon = "fa fa-fw fa-bars" });
            ret[2].AddItem(new MenuItem() { Text = "登录日志", Icon = "fa fa-fw fa-user-circle-o" });
            ret[2].AddItem(new MenuItem() { Text = "操作日志", Icon = "fa fa-fw fa-edit" });

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
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
                new AttributeItem() {
                    Name = "IsAccordion",
                    Description = "是否手风琴效果",
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
        }
    }
}
