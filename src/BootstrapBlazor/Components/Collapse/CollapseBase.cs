// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Collapse 折叠组件基类
    /// </summary>
    public abstract class CollapseBase : BootstrapComponentBase
    {

        /// <summary>
        /// 获得 按钮样式集合
        /// </summary>
        protected virtual string? ClassString => CssBuilder.Default("accordion")
            .AddClass("is-accordion", IsAccordion)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        private readonly List<CollapseItem> _items = new List<CollapseItem>();

        /// <summary>
        /// 获得/设置 组件 DOM 实例
        /// </summary>
        protected ElementReference CollapseElement { get; set; }

        /// <summary>
        /// 获得/设置 CollapseItem 集合
        /// </summary>
        public IEnumerable<CollapseItem> Items => _items;

        /// <summary>
        /// 获得/设置 指示箭头 默认不显示
        /// </summary>
        [Parameter]
        public bool ShowArrow { get; set; }

        /// <summary>
        /// 获得/设置 是否为手风琴效果 默认为 false
        /// </summary>
        [Parameter]
        public bool IsAccordion { get; set; }

        /// <summary>
        /// 获得/设置 CollapseItems 模板
        /// </summary>
        [Parameter]
        public RenderFragment? CollapseItems { get; set; }

        /// <summary>
        /// 添加 TabItem 方法 由 TabItem 方法加载时调用
        /// </summary>
        /// <param name="item">TabItemBase 实例</param>
        internal void AddItem(CollapseItem item) => _items.Add(item);

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await JSRuntime.InvokeVoidAsync(CollapseElement, "bb_collapse");
        }

        /// <summary>
        /// 点击 TabItem 时回调此方法
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnItemClick(CollapseItem item)
        {
            foreach (var collapseItem in Items)
            {
                var isActive = collapseItem.Text == item.Text;
                collapseItem.SetCollapsed(isActive);
            }
        }
    }
}
