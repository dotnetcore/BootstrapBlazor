using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Tab 组件基类
    /// </summary>
    public abstract class TabBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 Tab 组件样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("tabs tabs-top")
            .AddClass("tabs-card", IsCard)
            .AddClass("tabs-border-card", IsBorderCard)
            .Build();

        /// <summary>
        /// 获得/设置 TabItem 集合
        /// </summary>
        [Parameter] public IEnumerable<TabItemBase>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否为卡片样式
        /// </summary>
        [Parameter] public bool IsCard { get; set; }

        /// <summary>
        /// 获得/设置 是否为带边框卡片样式
        /// </summary>
        [Parameter] public bool IsBorderCard { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (Items != null)
            {
                if (!Items.Any(tab => tab.IsActive))
                {
                    var first = Items.FirstOrDefault();
                    if (first != null) first.SetActive(true);
                }
            }
        }

        /// <summary>
        /// 输出 TabItems 渲染
        /// </summary>
        /// <returns></returns>
        protected RenderFragment? RenderItems(TabItemBase item) => new RenderFragment(builder =>
        {
            var index = 0;
            builder.OpenComponent<TabItem>(index++);
            builder.AddAttribute(index++, nameof(TabItem.Text), item.Text);
            builder.AddAttribute(index++, nameof(TabItem.IsActive), item.IsActive);
            if (Items != null)
            {
                builder.AddAttribute(index++, nameof(TabItem.OnClick), new Action<TabItemBase>(tabItem =>
                {
                    foreach (var tab in Items)
                    {
                        tab.SetActive(tab.Text == tabItem.Text);
                    }
                    StateHasChanged();
                }));
            }
            builder.CloseComponent();
        });
    }
}
