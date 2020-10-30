using BootstrapBlazor.Components;
using System.Collections.Generic;

namespace BootstrapBlazorServer.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private IEnumerable<MenuItem> Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // TODO: 菜单获取可以通过数据库获取，此处为示例直接拼装的菜单集合
            Menus = GetIconSideMenuItems();
        }

        private IEnumerable<MenuItem> GetIconSideMenuItems()
        {
            var ret = new List<MenuItem>
            {
                new MenuItem() { Text = "返回组件库", Icon = "fa fa-fw fa-home", Url = "https://blazor.sdgxgz.com/components" },
                new MenuItem() { Text = "Index", Icon = "fa fa-fw fa-fa", Url = "" },
                new MenuItem() { Text = "Counter", Icon = "fa fa-fw fa-check-square-o", Url = "counter" },
                new MenuItem() { Text = "FetchData", Icon = "fa fa-fw fa-database", Url = "fetchdata" },
                new MenuItem() { Text = "系统设置", Icon = "fa fa-fw fa-gears", Url = "#" },
                new MenuItem() { Text = "权限设置", Icon = "fa fa-fw fa-users", Url = "#" },
                new MenuItem() { Text = "日志设置", Icon = "fa fa-fw fa-database", Url = "#" }
            };

            ret[4].AddItem(new MenuItem() { Text = "网站设置", Icon = "fa fa-fw fa-fa" });
            ret[4].AddItem(new MenuItem() { Text = "任务设置", Icon = "fa fa-fw fa-tasks" });
            ret[4].AddItem(new MenuItem() { Text = "用户设置", Icon = "fa fa-fw fa-user" });
            ret[4].AddItem(new MenuItem() { Text = "菜单设置", Icon = "fa fa-fw fa-dashboard" });
            ret[4].AddItem(new MenuItem() { Text = "角色设置", Icon = "fa fa-fw fa-sitemap" });
            ret[4].AddItem(new MenuItem() { Text = "访问日志", Icon = "fa fa-fw fa-bars" });
            ret[4].AddItem(new MenuItem() { Text = "登录日志", Icon = "fa fa-fw fa-user-circle-o" });
            ret[4].AddItem(new MenuItem() { Text = "操作日志", Icon = "fa fa-fw fa-edit" });

            ret[5].AddItem(new MenuItem() { Text = "用户设置", Icon = "fa fa-fw fa-user" });
            ret[5].AddItem(new MenuItem() { Text = "菜单设置", Icon = "fa fa-fw fa-dashboard" });
            ret[5].AddItem(new MenuItem() { Text = "角色设置", Icon = "fa fa-fw fa-sitemap" });
            ret[5].AddItem(new MenuItem() { Text = "访问日志", Icon = "fa fa-fw fa-bars" });
            ret[5].AddItem(new MenuItem() { Text = "登录日志", Icon = "fa fa-fw fa-user-circle-o" });
            ret[5].AddItem(new MenuItem() { Text = "操作日志", Icon = "fa fa-fw fa-edit" });

            ret[6].AddItem(new MenuItem() { Text = "用户设置", Icon = "fa fa-fw fa-user" });
            ret[6].AddItem(new MenuItem() { Text = "菜单设置", Icon = "fa fa-fw fa-dashboard" });
            ret[6].AddItem(new MenuItem() { Text = "角色设置", Icon = "fa fa-fw fa-sitemap" });
            ret[6].AddItem(new MenuItem() { Text = "访问日志", Icon = "fa fa-fw fa-bars" });
            ret[6].AddItem(new MenuItem() { Text = "登录日志", Icon = "fa fa-fw fa-user-circle-o" });
            ret[6].AddItem(new MenuItem() { Text = "操作日志", Icon = "fa fa-fw fa-edit" });

            return ret;
        }
    }
}
