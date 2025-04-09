// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// DateTimeRanges
/// </summary>
public sealed partial class DateTimeRanges
{
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private DateTimeRangeValue NormalDateTimeRangeValue { get; set; } = new();

    private DateTimeRangeValue YearRangeValue { get; set; } = new() { Start = DateTime.Today.AddYears(-1), End = DateTime.Today };

    private DateTimeRangeValue MonthRangeValue { get; set; } = new() { Start = DateTime.Today.AddMonths(-1), End = DateTime.Today };

    private Task OnNormalConfirm(DateTimeRangeValue value)
    {
        NormalLogger.Log($"选择的时间范围是: {value.Start:yyyy-MM-dd HH:mm:ss} - {value.End:yyyy-MM-dd HH:mm:ss}");
        return Task.CompletedTask;
    }

    private DateTimeRangeValue BindValueDemoDateTimeRangeValue { get; set; } = new();

    private string? BindValueDemoRange;

    private bool _showLunar = true;
    private bool _showSolarTerm = true;
    private bool _showFestivals = true;
    private bool _showHolidays = true;

    private Task BindValueDemoOnValueChanged(DateTimeRangeValue val, int index)
    {
        var ret = "";
        if (val.Start != DateTime.MinValue)
        {
            ret = val.Start.ToString("yyyy-MM-dd");
        }
        if (val.End != DateTime.MinValue)
        {
            ret = $"{ret} - {val.End:yyyy-MM-dd}";
        }
        if (index == 1)
        {
            BindValueDemoRange = ret;
        }
        return Task.CompletedTask;
    }

    private DateTimeRangeValue MaxMinDateTimeRangeValue { get; set; } = new()
    {
        Start = DateTime.Today,
        End = DateTime.Today.AddDays(3)
    };

    private string? MaxMinRange;

    private Task MaxMinOnValueChanged(DateTimeRangeValue val, int index)
    {
        var ret = "";
        if (val.Start != DateTime.MinValue)
        {
            ret = val.Start.ToString("yyyy-MM-dd");
        }
        if (val.End != DateTime.MinValue)
        {
            ret = $"{ret} - {val.End:yyyy-MM-dd}";
        }
        if (index != 1)
        {
            MaxMinRange = ret;
        }
        return Task.CompletedTask;
    }

    private bool IsDisabled { get; set; } = true;

    private DateTimeRangeValue DisabledDateTimeRangeValue { get; set; } = new();

    private DateTimeRangeValue SidebarDateTimeRangeValue { get; set; } = new();

    private DateTimeRangeValue TodayDateTimeRangeValue { get; set; } = new();

    [NotNull]
    private ValidateFormRangeFoo? ValidateFormModel { get; set; }

    private RowType ValidateFormRowType { get; set; }

    private string? ValidateFormClassString => CssBuilder.Default("row g-3")
        .AddClass("form-inline", ValidateFormRowType == RowType.Inline)
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ValidateFormModel = new ValidateFormRangeFoo()
        {
            DateTime = DateTime.Now,
            Range = new DateTimeRangeValue()
            {
                Start = DateTime.Today.AddMonths(-1),
                End = DateTime.Today
            }
        };
    }

    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    private class ValidateFormRangeFoo : Foo
    {
        [Required(ErrorMessage = "{0}不可为空")]
        [Display(Name = "时间范围")]
        public DateTimeRangeValue Range { get; set; } = new();
    }

    private DateTimeRangeValue AutoCloseDateTimeRangeValue { get; set; } = new();

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private static List<EventItem> GetEvents() =>
    [
        new()
        {
            Name = "OnConfirm",
            Description="Confirm callback delegate",
            Type ="Action"
        },
        new()
        {
            Name = "OnClearValue",
            Description="Clear callback delegate",
            Type ="Action"
        },
        new()
        {
            Name = "OnValueChanged",
            Description="ValueChanged callback delegate",
            Type ="Func<DateTimeRangeValue,Task>"
        },
        new()
        {
            Name = "OnDateClick",
            Description="Date cell click event callback",
            Type ="Func<DateTime,Task>"
        }
    ];

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static List<AttributeItem> GetAttributes() =>
    [
        new()
        {
            Name = "ShowLabel",
            Description = "Whether to show the pre-label",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSidebar",
            Description = "Whether to show the shortcut sidebar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(DateTimeRange.ShowSelectedValue),
            Description = "Whether to show the selected value",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowToday",
            Description = "Whether to show today shortcut button",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsDisabled",
            Description = "Whether to disable default is false",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowSidebar",
            Description = "Whether to display the shortcut sidebar The default is false",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Placement",
            Description = "Set the popup location",
            Type = "Placement",
            ValueList = "top|bottom|left|right",
            DefaultValue = "auto"
        },
        new()
        {
            Name = "DisplayText",
            Description = "Pre-label display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DateFormat",
            Description = "Date format string defaults to yyyy-MM-dd",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "yyyy-MM-dd"
        },
        new()
        {
            Name = "Value",
            Description = "Custom class containing start time end time",
            Type = "DateTimeRangeValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SidebarItems",
            Description = "Sidebar shortcut options collection",
            Type = "IEnumerable<DateTimeRangeSidebarItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsEditable",
            Description = "Is manual date entry allowed",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    ];
}
