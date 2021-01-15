// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Routing;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// MenuItem 组件
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<MenuItem> _items = new List<MenuItem>();

        /// <summary>
        /// 获得 父级菜单
        /// </summary>
        protected MenuItem? Parent { get; set; }

        /// <summary>
        /// 获得/设置 组件数据源
        /// </summary>
        public IEnumerable<MenuItem> Items => _items;

        /// <summary>
        /// 获得/设置 导航菜单文本内容
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 导航菜单链接地址
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 获得/设置 是否激活
        /// </summary>
        /// <value></value>
        public bool IsActive { get; set; }

        /// <summary>
        /// 获得/设置 是否收缩 默认收缩
        /// </summary>
        public bool IsCollapsed { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否禁用 默认 false 未禁用
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 图标字符串
        /// </summary>
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 匹配方式 默认 NavLinkMatch.Prefix
        /// </summary>
        public NavLinkMatch Match { get; set; }

        /// <summary>
        /// 获得/设置 菜单内子组件
        /// </summary>
        public DynamicComponent? Component { get; set; }

        /// <summary>
        /// 添加 Menutem 方法 由 MenuItem 方法加载时调用
        /// </summary>
        /// <param name="item">Menutem 实例</param>
        public virtual void AddItem(MenuItem item)
        {
            item.Parent = this;
            _items.Add(item);
        }

        /// <summary>
        /// 级联设置菜单 active=true 方法
        /// </summary>
        /// <param name="item"></param>
        /// <param name="active"></param>
        public static void CascadingSetActive(MenuItem item, bool active = true)
        {
            item.IsActive = active;
            var current = item;
            while (current.Parent != null)
            {
                current.Parent.IsActive = active;
                current.Parent.IsCollapsed = false;
                current = current.Parent;
            }
        }

        /// <summary>
        /// 级联设置菜单 Active=false 方法
        /// </summary>
        /// <param name="items"></param>
        public static void CascadingCancelActive(IEnumerable<MenuItem> items)
        {
            foreach (var item in items)
            {
                item.IsActive = false;
                if (item.Items.Any()) CascadingCancelActive(item.Items);
            }
        }

        /// <summary>
        /// 获得 所有子项集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MenuItem> GetAllSubItems() => Items.Concat(GetSubItems(Items));

        private static IEnumerable<MenuItem> GetSubItems(IEnumerable<MenuItem> items) => items.SelectMany(i => i.Items.Any() ? i.Items.Concat(GetSubItems(i.Items)) : i.Items);
    }
}
