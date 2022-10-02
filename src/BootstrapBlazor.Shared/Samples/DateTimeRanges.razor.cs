// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

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

    private DateTimeRangeValue DateTimeRangeValue5 { get; set; } = new DateTimeRangeValue() { Start = DateTime.Today, End = DateTime.Today.AddDays(3) };

    private string? range;
    private string? range2;

    private bool IsDisabled { get; set; } = true;

    [NotNull]
    private RangeFoo? Model { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private RowType FormRowType { get; set; }

    private string? GroupFormClassString => CssBuilder.Default("row g-3")
        .AddClass("form-inline", FormRowType == RowType.Inline)
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Model = new RangeFoo()
        {
            DateTime = DateTime.Now,
            Range = new DateTimeRangeValue()
            {
                Start = DateTime.Today.AddMonths(-1),
                End = DateTime.Today
            }
        };
    }

    private Task OnValueChanged(DateTimeRangeValue val, int index)
    {
        var ret = "";
        if (val.Start != DateTime.MinValue)
        {
            ret = val.Start.ToString("yyyy-MM-dd");
        }
        if (val.End != DateTime.MinValue)
        {
            ret = $"{ret} - {val.End.ToString("yyyy-MM-dd")}";
        }
        if (index == 1)
        {
            range2 = ret;
        }
        else
        {
            range = ret;
        }
        return Task.CompletedTask;
    }

    private Task OnConfirm(DateTimeRangeValue value)
    {
        DateLogger?.Log($"选择的时间范围是: {value.Start:yyyy-MM-dd} - {value.End:yyyy-MM-dd}");
        return Task.CompletedTask;
    }

    private class RangeFoo : Foo
    {
        [Required(ErrorMessage = "{0}不可为空")]
        [Display(Name = "时间范围")]
        public DateTimeRangeValue Range { get; set; } = new();
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
            Description="Confirm callback delegate",
            Type ="Action"
        },
        new EventItem()
        {
            Name = "OnClearValue",
            Description="Clear callback delegate",
            Type ="Action"
        },
        new EventItem()
        {
            Name = "OnValueChanged",
            Description="ValueChanged callback delegate",
            Type ="Func<DateTimeRangeValue,Task>"
        }
    };

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowLabel",
            Description = "Whether to show the pre-label",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowSidebar",
            Description = "Whether to show the shortcut sidebar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowToday",
            Description = "Whether to show today shortcut button",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = "Whether to disable default is false",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ShowSidebar",
            Description = "Whether to display the shortcut sidebar The default is fasle",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "Placement",
            Description = "Set the popup location",
            Type = "Placement",
            ValueList = "top|bottom|left|right",
            DefaultValue = "auto"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = "Pre-label display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "DateFormat",
            Description = "Date format string defaults to yyyy-MM-dd",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "yyyy-MM-dd"
        },
        new AttributeItem() {
            Name = "Value",
            Description = "Custom class containing start time end time",
            Type = "DateTimeRangeValue",
            ValueList = "",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "SidebarItems",
            Description = "Sidebar shortcut options collection",
            Type = "IEnumerable<DateTimeRangeSidebarItem>",
            ValueList = "",
            DefaultValue = " — "
        }
    };
}
