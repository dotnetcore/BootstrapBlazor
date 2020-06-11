using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class Layouts
    {

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

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Header",
                Description = "页头组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Side",
                Description = "侧边栏组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SideWidth",
                Description = "侧边栏宽度，支持百分比，设置 0 时关闭宽度功能",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "300px"
            },
            new AttributeItem() {
                Name = "Main",
                Description = "内容组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Footer",
                Description = "页脚组件模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Menus",
                Description = "整页面布局时侧边栏菜单数据集合",
                Type = "IEnumerable<MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsFullSide",
                Description = "侧边栏是否占满整个左边",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsPage",
                Description = "是否为整页面布局",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFixedFooter",
                Description = "是否固定 Footer 组件",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsDark",
                Description = "是否为暗黑模式",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsFixedHeader",
                Description = "是否固定 Header 组件",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowCollapseBar",
                Description = "是否显示收缩展开 Bar",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowFooter",
                Description = "是否显示 Footer 模板",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowGotoTop",
                Description = "是否显示返回顶端按钮",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnCollapsed",
                Description = "收缩展开回调委托",
                Type = "Func<bool, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
