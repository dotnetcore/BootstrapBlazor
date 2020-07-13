using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SideMenu
    {
        /// <summary>
        /// 获得 MenuItemLink 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetMenuItemLinkClassString(MenuItem item) => CssBuilder.Default("nav-link show collapse")
            .AddClass("collapsed", !item.IsActive || item.IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 MenuItem 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetMenuItemClassString(MenuItem item) => CssBuilder.Default("collapse-item collapse")
            .AddClass("show", item.IsActive || !item.IsCollapsed)
            .AddClass("collapsed", item.IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 是否展开字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetExpandedString(MenuItem item) => item.IsActive || !item.IsCollapsed ? "true" : "false";

        /// <summary>
        /// 获得/设置 是否点击侧边栏菜单
        /// </summary>
        private bool MenuClicked { get; set; }

        /// <summary>
        /// 获得/设置 是否禁止导航 默认为 false 允许导航
        /// </summary>
        [Parameter]
        public bool DisableNavigation { get; set; }

        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public new IEnumerable<MenuItem> Items { get; set; } = new MenuItem[0];

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Navigator.LocationChanged += Navigator_LocationChanged;
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            MenuClicked = false;
        }

        private void Navigator_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            if (!MenuClicked)
            {
                // 地址栏变化
                var url = Navigator.ToBaseRelativePath(Navigator.Uri);

                // 重新设置菜单激活状态
                MenuItem.CascadingCancelActive(Items);

                var item = FindMenuItem(Items, url);
                if (item != null)
                {
                    MenuItem.CascadingSetActive(item);
                    StateHasChanged();
                }
            }
        }

        private static MenuItem? FindMenuItem(IEnumerable<MenuItem> menus, string url)
        {
            MenuItem? ret = null;
            foreach (var item in menus)
            {
                if (item.Items.Any())
                {
                    ret = FindMenuItem(item.Items, url);
                }
                else if (item.Url?.Equals(url, System.StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    ret = item;
                }

                if (ret != null) break;
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task OnClickMenu(MenuItem item)
        {
            MenuClicked = true;

            // 回调委托
            await OnClick(item);
        }
    }
}
