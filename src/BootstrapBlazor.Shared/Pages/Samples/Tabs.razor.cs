// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Tabs
    {
        [NotNull]
        private Tab? TabSet { get; set; }

        [NotNull]
        private Tab? TabSet2 { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                var menuItem = TabMenu?.Items.FirstOrDefault();
                if (menuItem != null)
                {
                    await InvokeAsync(() =>
                    {
                        var _ = TabMenu?.OnClick?.Invoke(menuItem);
                    });
                }
            }
        }

        private static Task AddTab(Tab tabset)
        {
            var text = $"Tab {tabset.Items.Count() + 1}";
            tabset.AddTab(new Dictionary<string, object?>
            {
                [nameof(TabItem.Text)] = text,
                [nameof(TabItem.IsActive)] = true,
                [nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
                {
                    var index = 0;
                    builder.OpenElement(index++, "div");
                    builder.AddContent(index++, $"我是新建的 Tab 名称是 {text}");
                    builder.CloseElement();
                })
            });
            return Task.CompletedTask;
        }

        private static Task Active(Tab tabset)
        {
            tabset.ActiveTab(0);
            return Task.CompletedTask;
        }

        private bool RemoveEndable => (TabSet?.Items.Count() ?? 4) < 4;

        private static Task RemoveTab(Tab tabset)
        {
            if (tabset.Items.Count() > 4)
            {
                var item = tabset.Items.Last();
                tabset.RemoveTab(item);
            }
            return Task.CompletedTask;
        }

        private Placement BindPlacement = Placement.Top;

        private void SetPlacement(Placement placement)
        {
            BindPlacement = placement;
        }

        private static IEnumerable<MenuItem> GetSideMenuItems()
        {
            return new List<MenuItem>
            {
                new MenuItem() { Text = "计数器"  },
                new MenuItem() { Text = "天气预报" }
            };
        }

        [NotNull]
        private Tab? TabSetMenu { get; set; }

        [NotNull]
        private Menu? TabMenu { get; set; }

        private Task OnClickMenuItem(MenuItem item)
        {
            var text = item.Text;
            var tabItem = TabSetMenu.Items.FirstOrDefault(i => i.Text == text);
            if (tabItem == null) AddTabItem(text ?? "");
            else TabSetMenu.ActiveTab(tabItem);
            return Task.CompletedTask;
        }

        private void AddTabItem(string text) => TabSetMenu.AddTab(new Dictionary<string, object?>
        {
            [nameof(TabItem.Text)] = text,
            [nameof(TabItem.IsActive)] = true,
            [nameof(TabItem.ChildContent)] = text == "计数器" ? BootstrapDynamicComponent.CreateComponent<Counter>().Render() : BootstrapDynamicComponent.CreateComponent<FetchData>().Render()
        });

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "IsBorderCard",
                Description = "是否为带边框卡片样式",
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsCard",
                Description = "是否为卡片样式",
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsOnlyRenderActiveTab",
                Description = "是否仅渲染 Active 标签",
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowClose",
                Description = "是否显示关闭按钮",
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowExtendButtons",
                Description = "是否显示扩展按钮",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ClickTabToNavigation",
                Description = "点击标题时是否导航",
                Type = "boolean",
                ValueList = "true/false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "Placement",
                Description = "设置标签位置",
                Type = "Placement",
                ValueList = "Top|Right|Bottom|Left",
                DefaultValue = "Top"
            },
            new AttributeItem() {
                Name = "Height",
                Description = "设置标签高度",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "Items",
                Description = "TabItem 集合",
                Type = "IEnumerable<TabItemBase>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "ChildContent 模板",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "AdditionalAssemblies",
                Description = "额外程序集合，用于初始化路由",
                Type = "IEnumerable<Assembly>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "OnClickTab",
                Description = "点击 TabItem 标题时回调委托方法",
                Type = "Func<TabItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "TabItemTextDictionary",
                Description = "设置标签页显示标题集合，未设置时内部尝试使用菜单项数据",
                Type = "Dictionary<string, string>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };

        /// <summary>
        /// 获得方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<MethodItem> GetMethods() => new MethodItem[]
        {
            // TODO: 移动到数据库中
            new MethodItem() {
                Name = "AddTab",
                Description = "添加 TabItem 到 Tab 中",
                Parameters = "TabItem",
                ReturnValue = " — "
            },
            new MethodItem() {
                Name = "RemoveTab",
                Description = "移除 TabItem",
                Parameters = "TabItem",
                ReturnValue = " — "
            },
            new MethodItem() {
                Name = "ActiveTab",
                Description = "设置指定 TabItem 为激活状态",
                Parameters = "TabItem",
                ReturnValue = " — "
            },
            new MethodItem() {
                Name = "ClickPrevTab",
                Description = "切换到上一个标签方法",
                Parameters = "",
                ReturnValue = "Task"
            },
            new MethodItem() {
                Name = "ClickNextTab",
                Description = "切换到下一个标签方法",
                Parameters = "",
                ReturnValue = "Task"
            },
            new MethodItem() {
                Name = "CloseCurrentTab",
                Description = "关闭当前标签页方法",
                Parameters = "",
                ReturnValue = "Task"
            },
            new MethodItem() {
                Name = "CloseOtherTabs",
                Description = "关闭其他标签页方法",
                Parameters = "",
                ReturnValue = "Task"
            },
            new MethodItem() {
                Name = "CloseAllTabs",
                Description = "关闭所有标签页方法",
                Parameters = "",
                ReturnValue = "Task"
            },
        };
    }
}
