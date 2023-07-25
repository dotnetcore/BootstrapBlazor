// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// DateTimePickers
/// </summary>
public sealed partial class DateTimePickers
{
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private DateTime Value { get; set; } = DateTime.Today;

    private Task NormalOnConfirm()
    {
        NormalLogger.Log($"Value: {Value:yyyy-MM-dd}");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? ValueChangedLogger { get; set; }

    private TimeSpan ValueChangedValue { get; set; } = DateTime.Now - DateTime.Today;

    private void ValueChangedOnConfirm()
    {
        ValueChangedLogger.Log($"Value: {ValueChangedValue:hh\\:mm\\:ss}");
    }

    /// <summary>
    /// ModelValidateValue
    /// </summary>
    [Required]
    public DateTime? ValidateFormValue { get; set; }

    private DateTimeOffset? DateTimeOffsetValue { get; set; } = DateTimeOffset.Now;

    private TimeSpan TimeValue { get; set; } = DateTime.Now - DateTime.Today;

    private TimeSpan SpanValue { get; set; } = DateTime.Now.Subtract(DateTime.Today);

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

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePickers>? Localizer { get; set; }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnClickConfirm",
            Description = Localizer["Event1"],
            Type ="Action"
        },
        new EventItem()
        {
            Name = "ValueChanged",
            Description = Localizer["Event2"],
            Type ="EventCallback<DateTime?>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowSidebar",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Format",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "yyyy-MM-dd"
        },
        new AttributeItem() {
            Name = "IsShown",
            Description = Localizer["Att5"],
            Type = "boolean",
            ValueList = "",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["Att6"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["Att8"],
            Type = "TValue",
            ValueList = "DateTime | DateTime?",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ViewMode",
            Description = Localizer["Att9"],
            Type = "DatePickerViewMode",
            ValueList = " Date / DateTime / Year / Month",
            DefaultValue = "Date"
        },
        new AttributeItem() {
            Name = "AutoClose",
            Description = Localizer["AttrAutoClose"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    };
}
