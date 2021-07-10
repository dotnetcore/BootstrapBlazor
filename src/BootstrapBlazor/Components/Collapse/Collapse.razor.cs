// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Collapse
    {
        private ElementReference CollapseElement { get; set; }

        private static string? GetButtonClassString(CollapseItem item) => CssBuilder.Default("accordion-button")
            .AddClass("collapsed", item.IsCollapsed)
            .AddClass($"btn-{item.TitleColor.ToDescriptionString()}", item.TitleColor != Color.None)
            .AddClass($"accordion-button-{item.TitleColor.ToDescriptionString()}", item.TitleColor != Color.None)
            .Build();

        private static string? GetClassString(bool collpased) => CssBuilder.Default("accordion-collapse collapse")
            .AddClass("show", !collpased)
            .Build();

        private static string? TitleClassString(Color color) => CssBuilder.Default("accordion-header")
            .AddClass($"bg-{color.ToDescriptionString()}", color != Color.None)
            .Build();

        private string? ClassString => CssBuilder.Default("accordion")
            .AddClass("is-accordion", IsAccordion)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        private readonly List<CollapseItem> _items = new();

        /// <summary>
        /// 获得/设置 CollapseItem 集合
        /// </summary>
        public IEnumerable<CollapseItem> Items => _items;

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
        /// 获得/设置 CollapseItem 展开收缩时回调方法
        /// </summary>
        [Parameter]
        public Func<CollapseItem, Task>? OnCollapseChanged { get; set; }

        private async Task OnClickItem(CollapseItem item)
        {
            item.SetCollapsed(!item.IsCollapsed);
            if (OnCollapseChanged != null)
            {
                await OnCollapseChanged(item);
            }
        }

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

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(CollapseElement, "bb_collapse");
            }
        }
    }
}
