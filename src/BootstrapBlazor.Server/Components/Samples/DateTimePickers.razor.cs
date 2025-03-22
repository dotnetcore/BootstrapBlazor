// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// DateTimePickers
/// </summary>
public sealed partial class DateTimePickers
{
    private DateTime DateTimePickerValue { get; set; }

    [NotNull]
    private ConsoleLogger? TimePickerLogger { get; set; }

    private Task TimePickerValueChanged(DateTime dt)
    {
        TimePickerLogger.Log($"Value: {dt}");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private DateTime Value { get; set; } = DateTime.Today;

    private Task NormalOnConfirm()
    {
        NormalLogger.Log($"Value: {Value:yyyy-MM-dd}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// ModelValidateValue
    /// </summary>
    [Required]
    public DateTime? ValidateFormValue { get; set; }

    private DateTimeOffset? DateTimeOffsetValue { get; set; } = DateTimeOffset.Now;

    private TimeSpan TimeValue { get; set; } = DateTime.Now - DateTime.Today;

    private TimeSpan SpanValue { get; set; } = DateTime.Now.Subtract(DateTime.Today);

    private DateTime AutoCloseValue { get; set; } = DateTime.Today;

    private DateTime AllowValue { get; set; } = DateTime.Today;

    private DateTime SidebarValue { get; set; } = DateTime.Today;

    private DateTime YearValue { get; set; } = DateTime.Today;

    private DateTime MonthValue { get; set; } = DateTime.Today;

    private DateTime DateValue { get; set; } = DateTime.Today;

    private DateTime DateTimeValue { get; set; } = DateTime.Today;

    private static string FormatterSpanString(TimeSpan ts)
    {
        return ts.ToString("hh\\:mm\\:ss");
    }

    private DateTime? BindNullValue { get; set; }

    private string GetNullValueString => BindNullValue.HasValue ? BindNullValue.Value.ToString("yyyy-MM-dd") : "";

    private DateTime? ShowLabelValue { get; set; }

    private bool IsDisabled { get; set; } = true;

    private DateTime? BindValue { get; set; } = DateTime.Today;

    private string BindValueString
    {
        get => BindValue.HasValue ? BindValue.Value.ToString("yyyy-MM-dd") : "";
        set => BindValue = DateTime.TryParse(value, out var d) ? d : null;
    }

    private static string? GetMarkByDay(DateTime dt) => CssBuilder.Default("bb-picker-body-lunar-text")
        .AddClass("mark", Enumerable.Range(7, 7).Contains(dt.Day))
        .Build();

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePickers>? Localizer { get; set; }

    private bool _showLunar = true;
    private bool _showSolarTerm = true;
    private bool _showFestivals = true;
    private bool _showHolidays = true;
    private bool _disableWeekend = true;
    private bool _disableToday = true;
    private DateTime? _disabledNullValue = DateTime.Today;
    private DateTime _disabledValue = DateTime.Today;

    private async Task<List<DateTime>> OnGetDisabledDaysCallback(DateTime start, DateTime end)
    {
        var ret = new List<DateTime>();
        if (_disableWeekend)
        {
            var day = start;
            while (day <= end)
            {
                if (day.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday)
                {
                    ret.Add(day);
                }
                day = day.AddDays(1);
            }

            if (DateTime.Today.DayOfWeek is DayOfWeek.Sunday or DayOfWeek.Saturday)
            {
                // 处理今天是否禁用
                ret.Add(DateTime.Today);
            }
        }

        if (_disableToday)
        {
            // 处理今天是否禁用
            ret.Add(DateTime.Today);
        }

        // 模拟异步延迟
        await Task.Delay(100);
        return ret;
    }

    private DateTimePicker<DateTime?> _picker1 = default!;

    private DateTimePicker<DateTime> _picker2 = default!;

    private Task OnDisabledDaysChanged(bool v)
    {
        _picker1.ClearDisabledDays();
        _picker2.ClearDisabledDays();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnClickConfirm",
            Description = Localizer["Event1"],
            Type ="Action"
        },
        new()
        {
            Name = "ValueChanged",
            Description = Localizer["Event2"],
            Type ="EventCallback<DateTime?>"
        },
        new()
        {
            Name = "OnGetDisabledDaysCallback",
            Description = Localizer["OnGetDisabledDaysCallbackEvent"],
            Type ="Func<DateTime, DateTime, Task<List<DateTime>>>"
        }
    ];

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSidebar",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Format",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "yyyy-MM-dd"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["Att6"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Value",
            Description = Localizer["Att8"],
            Type = "TValue",
            ValueList = "DateTime | DateTime?",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(DateTimePicker<DateTime>.FirstDayOfWeek),
            Description = Localizer["AttrFirstDayOfWeek"],
            Type = "DayOfWeek",
            ValueList = " — ",
            DefaultValue = "Sunday"
        },
        new()
        {
            Name = "ViewMode",
            Description = Localizer["Att9"],
            Type = "DatePickerViewMode",
            ValueList = " Date / DateTime / Year / Month",
            DefaultValue = "Date"
        },
        new() {
            Name = "AutoClose",
            Description = Localizer["AttrAutoClose"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsEditable",
            Description = Localizer["AttrIsEditable"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowLunar",
            Description = Localizer["AttrShowLunar"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowSolarTerm",
            Description = Localizer["AttrShowSolarTerm"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowFestivals",
            Description = Localizer["AttrShowFestivals"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowHolidays",
            Description = Localizer["AttrShowHolidays"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "EnableDisabledDaysCache",
            Description = Localizer["AttrEnableDisabledDaysCache"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new() {
            Name = "DisplayDisabledDayAsEmpty",
            Description = Localizer["AttrDisplayDisabledDayAsEmpty"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    ];
}
