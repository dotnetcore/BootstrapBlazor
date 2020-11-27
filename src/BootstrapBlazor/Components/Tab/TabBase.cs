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
    /// Tab 组件基类
    /// </summary>
    public abstract class TabBase : BootstrapComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool FirstRender { get; private set; } = true;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string? GetContentClassString(TabItem item) => CssBuilder.Default("tabs-body-content")
            .AddClass("d-none", !item.IsActive)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="active"></param>
        /// <returns></returns>
        protected string? GetClassString(bool active) => CssBuilder.Default("tabs-item")
            .AddClass("is-active", active)
            .AddClass("is-closeable", ShowClose)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="icon"></param>
        /// <returns></returns>
        protected string? GetIconClassString(string icon) => CssBuilder.Default("fa fa-fw")
            .AddClass(icon)
            .Build();

        /// <summary>
        /// 获得/设置 Tab 组件 DOM 实例
        /// </summary>
        protected ElementReference TabElement { get; set; }

        /// <summary>
        /// 获得 Tab 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("tabs")
            .AddClass("tabs-card", IsCard)
            .AddClass("tabs-border-card", IsBorderCard)
            .AddClass($"tabs-{Placement.ToDescriptionString()}", Placement == Placement.Top || Placement == Placement.Right || Placement == Placement.Bottom || Placement == Placement.Left)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Tab 组件 Style
        /// </summary>
        protected string? StyleString => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height > 0)
            .Build();

        private readonly List<TabItem> _items = new List<TabItem>(50);

        /// <summary>
        /// 获得/设置 TabItem 集合
        /// </summary>
        public IEnumerable<TabItem> Items => _items;

        /// <summary>
        /// 获得/设置 是否为卡片样式
        /// </summary>
        [Parameter]
        public bool IsCard { get; set; }

        /// <summary>
        /// 获得/设置 是否为带边框卡片样式
        /// </summary>
        [Parameter]
        public bool IsBorderCard { get; set; }

        /// <summary>
        /// 获得/设置 组件高度 默认值为 0 高度自动
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 组件标签显示位置 默认显示在 Top 位置
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Top;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮 默认为 false 不显示
        /// </summary>
        [Parameter]
        public bool ShowClose { get; set; }

        /// <summary>
        /// 获得/设置 TabItems 模板
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 点击 TabItem 时回调方法
        /// </summary>
        [Parameter]
        public Func<TabItem, Task>? OnClickTab { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            FirstRender = false;

            await JSRuntime.InvokeVoidAsync(TabElement, "bb_tab");
        }

        /// <summary>
        /// 点击 TabItem 时回调此方法
        /// </summary>
        protected virtual async Task OnClickTabItem(TabItem item)
        {
            Items.ToList().ForEach(i => i.SetActive(false));
            item.SetActive(true);
            if (OnClickTab != null) await OnClickTab(item);
        }

        /// <summary>
        /// 点击上一个标签页时回调此方法
        /// </summary>
        protected virtual void ClickPrevTab()
        {
            var item = Items.FirstOrDefault(i => i.IsActive);
            if (item != null)
            {
                var index = _items.IndexOf(item);
                if (index > -1)
                {
                    item.SetActive(false);
                    index--;
                    if (index < 0) index = _items.Count - 1;
                    item = Items.ElementAt(index);
                    item.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 点击下一个标签页时回调此方法
        /// </summary>
        protected virtual void ClickNextTab()
        {
            var item = Items.FirstOrDefault(i => i.IsActive);
            if (item != null)
            {
                var index = _items.IndexOf(item);
                if (index < _items.Count)
                {
                    item.SetActive(false);
                    index++;
                    if (index + 1 > _items.Count) index = 0;
                    item = Items.ElementAt(index);
                    item.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 添加 TabItem 方法 由 TabItem 方法加载时调用
        /// </summary>
        /// <param name="item">TabItemBase 实例</param>
        internal void AddItem(TabItem item) => _items.Add(item);

        /// <summary>
        /// 添加 TabItem 方法
        /// </summary>
        /// <param name="item"></param>
        public virtual Task Add(TabItem item)
        {
            var check = _items.Contains(item);
            if (item.IsActive || !check) _items.ForEach(i => i.SetActive(false));
            if (!check)
            {
                _items.Add(item);
                item.SetActive(true);
            }
            StateHasChanged();
            return Task.CompletedTask;
        }

        /// <summary>
        /// 移除 TabItem 方法
        /// </summary>
        /// <param name="item"></param>
        public virtual Task Remove(TabItem item)
        {
            var index = _items.IndexOf(item);
            _items.Remove(item);
            var activeItem = _items.FirstOrDefault(i => i.IsActive);
            if (activeItem == null)
            {
                // 删除的 TabItem 是当前 Tab
                if (index < _items.Count)
                {
                    // 查找后面的 Tab
                    activeItem = _items[index];
                }
                else
                {
                    // 查找前面的 Tab
                    activeItem = _items.LastOrDefault();
                }
                if (activeItem != null) activeItem.SetActive(true);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// 设置指定 TabItem 为激活状态
        /// </summary>
        /// <param name="item"></param>
        public virtual Task ActiveTab(TabItem item)
        {
            _items.ForEach(i => i.SetActive(false));
            item.SetActive(true);

            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}
