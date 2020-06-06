using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class LayoutPages
    {
        private IEnumerable<SelectedItem> SideBarItems { get; set; } = new SelectedItem[]
        {
            new SelectedItem("left-right", "左右结构"),
            new SelectedItem("top-bottom", "上下结构")
        };

        private string? StyleString => CssBuilder.Default()
            .AddClass($"height: {Height * 100}px", Height > 0)
            .Build();

        private bool IsFixedHeader { get; set; }

        private bool IsFixedFooter { get; set; }

        private int Height { get; set; }

        [CascadingParameter]
        private PageLayout? RootPage { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (RootPage != null)
            {
                var isFullSide = RootPage.IsFullSide;
                SideBarItems.ElementAt(0).Active = isFullSide;
                SideBarItems.ElementAt(1).Active = !isFullSide;
            }
        }

        private async Task OnHeaderFooterStateChanged(CheckboxState state, bool val)
        {
            if (RootPage != null)
            {
                await RootPage.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
                {
                    [nameof(RootPage.FixedFooter)] = IsFixedFooter,
                    [nameof(RootPage.FixedHeader)] = IsFixedHeader,
                }));
                RootPage.Refresh();
            }
        }

        private async Task OnStateChanged(CheckboxState state, SelectedItem item)
        {
            var isfullSidebar = item.Value == "left-right";
            if (RootPage != null)
            {
                await RootPage.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
                {
                    [nameof(RootPage.IsFullSide)] = isfullSidebar
                }));
                RootPage.Refresh();
            }
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
    }
}
