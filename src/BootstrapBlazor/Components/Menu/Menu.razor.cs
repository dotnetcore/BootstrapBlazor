// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Menu 组件基类
    /// </summary>
    public partial class Menu
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("menu")
            .AddClass("is-vertical", IsVertical)
            .AddClass("is-collapsed", IsVertical && IsCollapsed)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<MenuItem> Items { get; set; } = Enumerable.Empty<MenuItem>();

        /// <summary>
        /// 获得/设置 是否为手风琴效果 默认为 false
        /// </summary>
        [Parameter]
        public bool IsAccordion { get; set; }

        /// <summary>
        /// 获得/设置 侧栏垂直模式
        /// </summary>
        /// <value></value>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 获得/设置 侧栏是否收起 默认 false 未收起
        /// </summary>
        [Parameter]
        public bool IsCollapsed { get; set; }

        /// <summary>
        /// 获得/设置 是否禁止导航 默认为 false 允许导航
        /// </summary>
        [Parameter]
        public bool DisableNavigation { get; set; }

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;

#nullable disable
        /// <summary>
        /// 获得/设置 NavigationManager 实例
        /// </summary>
        [Inject]
        private NavigationManager Navigator { get; set; }
#nullable restore

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            var item = FindMenuItem(Items, Navigator.ToBaseRelativePath(Navigator.Uri));
            CascadingSetActive(item);
        }

        /// <summary>
        /// 根据当前路径设置菜单激活状态
        /// </summary>
        /// <param name="item"></param>
        private void CascadingSetActive(MenuItem? item)
        {
            // 重新设置菜单激活状态
            if (item != null)
            {
                MenuItem.CascadingCancelActive(Items);
                MenuItem.CascadingSetActive(item);
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
                else if (item.Url?.TrimStart('/').Equals(url, StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    ret = item;
                }

                if (ret != null) break;
            }
            return ret;
        }

        private async Task OnClickMenu(MenuItem item)
        {
            if (!item.IsDisabled)
            {
                // 回调委托
                await OnClick(item);
                if (!item.Items.Any())
                {
                    CascadingSetActive(item);
                    StateHasChanged();
                }
            }
        }
    }
}
