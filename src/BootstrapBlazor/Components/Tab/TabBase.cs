using Microsoft.AspNetCore.Components;
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
        /// 获得/设置 Tab 组件 DOM 实例
        /// </summary>
        protected ElementReference TabElement { get; set; }

        /// <summary>
        /// 获得/设置 TabContent 实例
        /// </summary>
        protected TabContent? TabContent { get; set; }

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

        private readonly List<TabItem> _items = new List<TabItem>();

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
        public RenderFragment? TabItems { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await ReActiveTab();
        }

        /// <summary>
        /// 点击 TabItem 时回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnTabClick(TabItem item)
        {
            ActiveTab(item);
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
                    TabContent?.Render(item);
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
                    TabContent?.Render(item);
                }
            }
        }

        /// <summary>
        /// 添加 TabItem 方法 由 TabItem 方法加载时调用
        /// </summary>
        /// <param name="item">TabItemBase 实例</param>
        internal void AddItem(TabItem item) => _items.Add(item);

        /// <summary>
        /// TabItem 切换后回调此方法
        /// </summary>
        public virtual async Task ReActiveTab()
        {
            if (JSRuntime != null) await JSRuntime.Invoke(TabElement, "tab");
        }

        /// <summary>
        /// 添加 TabItem 方法
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(TabItem item)
        {
            _items.Add(item);
            if (item.IsActive)
            {
                _items.ForEach(t =>
                {
                    if (t != item) t.SetActive(false);
                });
                OnTabClick(item);
            }
        }

        /// <summary>
        /// 移除 TabItem 方法
        /// </summary>
        /// <param name="item"></param>
        public virtual void Remove(TabItem item)
        {
            _items.Remove(item);
            var activeItem = _items.FirstOrDefault(i => i.IsActive);
            if (activeItem == null)
            {
                activeItem = _items.LastOrDefault();
                if (activeItem != null)
                {
                    activeItem.SetActive(true);
                    OnTabClick(activeItem);
                }
                else
                {
                    // no TabItem
                    TabContent?.Clear();
                }
            }
        }

        /// <summary>
        /// 设置指定 TabItem 为激活状态
        /// </summary>
        /// <param name="item"></param>
        public virtual void ActiveTab(TabItem item)
        {
            foreach (var tab in Items)
            {
                var isActive = tab.Text == item.Text;
                tab.SetActive(isActive);
            }
            TabContent?.Render(item);
        }
    }
}
