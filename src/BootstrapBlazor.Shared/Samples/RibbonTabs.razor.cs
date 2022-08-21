// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class RibbonTabs
{
    [NotNull]
    private IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Items = new List<RibbonTabItem>()
        {
            new()
            {
                Text = "文件",
                Items = new List<RibbonTabItem>()
                {
                    new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组一" },
                    new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组一" },
                    new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组一" },
                    new() { Text = "打开", Icon = "fa-solid fa-font-awesome", GroupName = "操作组二" },
                    new() { Text = "保存", Icon = "fa-solid fa-font-awesome", GroupName = "操作组二" },
                    new() { Text = "另存为", Icon = "fa-solid fa-font-awesome", GroupName = "操作组二" }
                }
            },
            new()
            {
                Text = "编辑",
                Items = new List<RibbonTabItem>()
                {
                    new() { Text = "打开", Icon = "fa-solid fa-font-awesome", GroupName = "操作组三" },
                    new() { Text = "保存", Icon = "fa-solid fa-font-awesome", GroupName = "操作组三" },
                    new() { Text = "另存为", Icon = "fa-solid fa-font-awesome", GroupName = "操作组三" },
                    new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组四" },
                    new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组四" },
                    new() { Text = "常规操作", Icon = "fa-solid fa-font-awesome", GroupName = "操作组四" }
                }
            }
        };
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = "是否显示悬浮小箭头",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnFloatChanged),
            Description = "组件是否悬浮状态改变时回调方法",
            Type = "bool",
            ValueList = "Func<bool, Task>",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowUpIcon),
            Description = "选项卡向上箭头图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-up fa-2x"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowDownIcon),
            Description = "选项卡向下箭头图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-angle-down fa-2x"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RibbonArrowPinIcon),
            Description = "选项卡可固定图标",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-thumbtack fa-rotate-90"
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.ShowFloatButton),
            Description = "是否显示悬浮小箭头",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.Items),
            Description = "数据源",
            Type = "IEnumerable<RibbonTabItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.OnTabItemClickAsync),
            Description = "点击命令按钮回调方法",
            Type = "Func<RibbonTabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(RibbonTab.RightButtonsTemplate),
            Description = "右侧按钮模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
