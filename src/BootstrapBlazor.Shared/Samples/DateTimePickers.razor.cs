// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class DateTimePickers
{
    private TimeSpan SpanValue { get; set; } = DateTime.Now.Subtract(DateTime.Today);

    private string SpanValue2 { get; set; } = DateTime.Now.ToString("HH:mm:ss");

    [NotNull]
    private BlockLogger? DateLogger { get; set; }

    [NotNull]
    private BlockLogger? TimeLogger { get; set; }

    private DateTime? BindValue { get; set; } = DateTime.Today;

    private DateTime? BindNullValue { get; set; }

    private bool IsDisabled { get; set; } = true;

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public DateTime? ModelValidateValue { get; set; }

    private string? SubmitText { get; set; }

    private string GetNullValueString => BindNullValue.HasValue ? BindNullValue.Value.ToString("yyyy-MM-dd") : " ";

    [Inject]
    [NotNull]
    private IStringLocalizer<DateTimePickers>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        SubmitText ??= Localizer[nameof(SubmitText)];
    }

    /// <summary>
    /// 
    /// </summary>
    private string BindValueString
    {
        get
        {
            return BindValue.HasValue ? BindValue.Value.ToString("yyyy-MM-dd") : "";
        }
        set
        {
            if (DateTime.TryParse(value, out var d))
            {
                BindValue = d;
            }
            else
            {
                BindValue = DateTime.Today;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    private Task DateValueChanged(DateTime d)
    {
        DateLogger.Log($"{Localizer["Log1Text"]}: {d:yyyy-MM-dd}");
        return Task.CompletedTask;
    }

    private static string FormatterSpanString(TimeSpan ts)
    {
        return ts.ToString("hh\\:mm\\:ss");
    }

    private TimeSpan TimeNow { get; set; } = DateTime.Now - DateTime.Today;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    private void TimeValueChanged(TimeSpan d)
    {
        TimeLogger.Log($"{Localizer["Log2Text"]}: {d:hh\\:mm\\:ss}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="d"></param>
    private Task DateTimeValueChanged(DateTime? d)
    {
        BindValue = d;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ts"></param>
    private void OnValueChange(TimeSpan ts)
    {
        SpanValue2 = ts.ToString("hh\\:mm\\:ss");
        StateHasChanged();
    }

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
            },
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
                Name = "TimeFormat",
                Description = Localizer["Att7"],
                Type = "string",
                ValueList = "",
                DefaultValue = "hh:mm:ss"
            },
            new AttributeItem() {
                Name = "Value",
                Description = Localizer["Att8"],
                Type = "TValue",
                ValueList = "DateTime | DateTime?",
                DefaultValue = " — "
            },
             new AttributeItem() {
                Name = "ViewModel",
                Description = Localizer["Att9"],
                Type = "DatePickerViewModel",
                ValueList = " Date / DateTime / Year / Month",
                DefaultValue = "Date"
            },
    };
}
