// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class DateTimeRanges
{
    [NotNull]
    private BlockLogger? DateLogger { get; set; }

    private DateTimeRangeValue DateTimeRangeValue1 { get; set; } = new DateTimeRangeValue();

    private DateTimeRangeValue DateTimeRangeValue2 { get; set; } = new DateTimeRangeValue();

    private DateTimeRangeValue DateTimeRangeValue3 { get; set; } = new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today.AddDays(3) };

    private DateTimeRangeValue DateTimeRangeValue4 { get; set; } = new DateTimeRangeValue();

    private bool IsDisabled { get; set; } = true;

    private Task OnConfirm(DateTimeRangeValue value)
    {
        DateLogger?.Log($"选择的时间范围是: {value.Start:yyyy-MM-dd} - {value.End:yyyy-MM-dd}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnConfirm",
                Description="确认按钮回调委托",
                Type ="Action"
            },
            new EventItem()
            {
                Name = "OnClearValue",
                Description="清空按钮回调委托",
                Type ="Action"
            },
            new EventItem()
            {
                Name = "OnValueChanged",
                Description="值改变回调委托",
                Type ="Func<DateTimeRangeValue,Task>"
            }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem() {
                Name = "ShowLabel",
                Description = "是否显示前置标签",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "ShowSidebar",
                Description = "是否显示快捷侧边栏",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowToday",
                Description = "是否显示今天快捷按钮",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "IsDisabled",
                Description = "是否禁用 默认为 fasle",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "ShowSidebar",
                Description = "是否显示快捷侧边栏 默认为 fasle",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "Placement",
                Description = "设置弹窗出现位置",
                Type = "Placement",
                ValueList = "top|bottom|left|right",
                DefaultValue = "auto"
            },
            new AttributeItem() {
                Name = "DisplayText",
                Description = "前置标签显示文本",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "DateFormat",
                Description = "日期格式字符串 默认为 yyyy-MM-dd",
                Type = "string",
                ValueList = " — ",
                DefaultValue = "yyyy-MM-dd"
            },
            new AttributeItem() {
                Name = "Value",
                Description = "包含开始时间结束时间的自定义类",
                Type = "DateTimeRangeValue",
                ValueList = "",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "SidebarItems",
                Description = "侧边栏快捷选项集合",
                Type = "IEnumerable<DateTimeRangeSidebarItem>",
                ValueList = "",
                DefaultValue = " — "
            }
    };
}
