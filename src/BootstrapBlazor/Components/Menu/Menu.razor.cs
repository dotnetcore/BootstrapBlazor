// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Menu 组件基类
    /// </summary>
    public partial class Menu
    {
        private ElementReference MenuElemenet { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("menu")
            .AddClass("is-bottom", IsBottom)
            .AddClass("is-vertical", IsVertical)
            .AddClass("is-collapsed", IsVertical && IsCollapsed)
            .AddClass("accordion", IsVertical && IsAccordion)
            .AddClass("expaned", IsVertical && IsExpandAll)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 用于提高性能存储当前 active 状态的菜单
        /// </summary>
        private MenuItem? ActiveMenu { get; set; }

        private IEnumerable<MenuItem>? _items;
        /// <summary>
        /// 菜单是否初始化
        /// </summary>
        private bool _init;
        /// <summary>
        /// 是否需要调用 JS
        /// </summary>
        private bool _invokeJs;
        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        [NotNull]
        public IEnumerable<MenuItem>? Items
        {
            get => _items ?? Enumerable.Empty<MenuItem>();
            set
            {
                if (_items != value)
                {
                    _items = value;
                    _init = false;
                    _invokeJs = true;
                }
            }
        }

        private bool _accordion;
        /// <summary>
        /// 获得/设置 是否为手风琴效果 默认为 false
        /// </summary>
        /// <remarks>启用此功能时 <see cref="IsExpandAll" /> 参数不生效</remarks>
        [Parameter]
        public bool IsAccordion
        {
            get => _accordion;
            set
            {
                if (_accordion != value)
                {
                    _accordion = value;
                    _invokeJs = true;
                }
            }
        }

        private bool _expand;
        private bool _invokeExpandJs;
        /// <summary>
        /// 获得/设置 是否全部展开 默认为 false
        /// </summary>
        /// <remarks>手风琴效果 <see cref="IsAccordion" /> 时此参数不生效</remarks>
        [Parameter]
        public bool IsExpandAll
        {
            get => _expand;
            set
            {
                if (_expand != value)
                {
                    _expand = value;
                    _invokeExpandJs = true;
                }
            }
        }

        /// <summary>
        /// 获得/设置 侧栏是否收起 默认 false 未收起
        /// </summary>
        [Parameter]
        public bool IsCollapsed { get; set; }

        /// <summary>
        /// 获得/设置 侧栏垂直模式 默认 false
        /// </summary>
        /// <value></value>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏垂直模式在底部 默认 false
        /// </summary>
        [Parameter]
        public bool IsBottom { get; set; }

        /// <summary>
        /// 获得/设置 缩进大小 默认为 16 单位 px
        /// </summary>
        [Parameter]
        public int IndentSize { get; set; } = 16;

        /// <summary>
        /// 获得/设置 是否禁止导航 默认为 false 允许导航
        /// </summary>
        [Parameter]
        public bool DisableNavigation { get; set; }

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task>? OnClick { get; set; }

        /// <summary>
        /// 获得/设置 NavigationManager 实例
        /// </summary>
        [Inject]
        [NotNull]
        private NavigationManager? Navigator { get; set; }

        [Inject]
        [NotNull]
        private TabItemTextOptions? Options { get; set; }

        /// <summary>
        /// OnParametersSet 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // 参数变化时重新整理菜单
            if (!_init && Items.Any())
            {
                InitMenus(null, Items, Navigator.ToBaseRelativePath(Navigator.Uri));
                if (!DisableNavigation)
                {
                    Options.Text = ActiveMenu?.Text;
                    Options.Icon = ActiveMenu?.Icon;
                    Options.IsActive = true;
                }
                _init = true;
            }
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (IsVertical)
            {
                if (_invokeJs)
                {
                    _invokeJs = false;
                    await JSRuntime.InvokeVoidAsync(MenuElemenet, "bb_side_menu");
                }
                if (_invokeExpandJs)
                {
                    _invokeExpandJs = false;
                    await JSRuntime.InvokeVoidAsync(MenuElemenet, "bb_side_menu_expand", IsExpandAll);
                }
            }
        }

        private void InitMenus(MenuItem? parent, IEnumerable<MenuItem> menus, string url)
        {
            foreach (var item in menus)
            {
                if (parent != null)
                {
                    // 设置当前菜单父菜单
                    item.Parent = parent;
                }

                // 设置当前菜单缩进
                item.SetIndent();

                if (!DisableNavigation)
                {
                    // 未禁用导航时设置 active = false 使用地址栏激活菜单
                    item.IsActive = false;
                }

                if (item.Items.Any())
                {
                    // 递归子菜单
                    InitMenus(item, item.Items, url);
                }
                else if (!DisableNavigation && (item.Url?.TrimStart('/').Equals(url, StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    // 未禁用导航时 使用地址栏激活菜单
                    item.IsActive = true;
                }

                if (item.IsActive)
                {
                    ActiveMenu = item;
                }
            }
        }

        private async Task OnClickMenu(MenuItem item)
        {
            if (!item.IsDisabled)
            {
                // 回调委托
                if (OnClick != null)
                {
                    await OnClick(item);
                }

                if (DisableNavigation)
                {
                    if (IsVertical)
                    {
                        if (ActiveMenu != null)
                        {
                            ActiveMenu.IsActive = false;
                        }
                        item.IsActive = true;
                        if (IsCollapsed)
                        {
                            item.CascadingSetActive();
                        }
                    }
                    else
                    {
                        // 顶栏模式重新级联设置 active
                        ActiveMenu?.CascadingSetActive(false);
                        item.CascadingSetActive();
                    }
                    ActiveMenu = item;

                    // 刷新 UI
                    StateHasChanged();
                }
                else if (!item.Items.Any())
                {
                    Options.Text = item.Text;
                    Options.Icon = item.Icon;
                    Options.IsActive = true;
                }
            }
        }
    }
}
