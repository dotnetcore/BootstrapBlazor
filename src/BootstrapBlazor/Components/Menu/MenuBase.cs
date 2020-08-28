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
    public abstract class MenuBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 菜单组件 DOM 实例
        /// </summary>
        protected ElementReference MenuElement { get; set; }

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

        /// <summary>
        /// 获得/设置 NavigationManager 实例
        /// </summary>
        [Inject]
        protected NavigationManager? Navigator { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (!DisableNavigation && Navigator != null)
            {
                // 首次加载根据地址栏寻找当前菜单
                Items = Items.ToList();

                var url = Navigator.ToBaseRelativePath(Navigator.Uri);
                var menuItem = FindMenuItemByUrl(Items, url);
                if (menuItem != null) MenuItem.CascadingSetActive(menuItem, true);
            }
        }

        private MenuItem? FindMenuItemByUrl(IEnumerable<MenuItem> items, string url)
        {
            MenuItem? ret = null;
            foreach (var item in items)
            {
                if (item.Url?.TrimStart('/').Equals(url, StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    ret = item;
                    break;
                }
                if (item.Items.Any())
                {
                    ret = FindMenuItemByUrl(item.Items, url);
                    if (ret != null) break;
                }
            }
            return ret;
        }
    }
}
