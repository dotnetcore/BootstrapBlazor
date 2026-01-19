// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Calendars
{
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private void OnValueChanged(DateTime ts)
    {
        NormalLogger.Log($"{ts:yyyy-MM-dd}");
    }

    private DateTime BindValue { get; set; } = DateTime.Today;

    private CalendarViewMode HeaderTemplateViewMode { get; set; }

    private static string Formatter(DateTime ts) => ts.ToString("yyyy-MM-dd");

    private ConcurrentDictionary<DateTime, List<Crew>> Data { get; } = new();

    private DateTime CrewInfoValue { get; set; } = DateTime.Today;

    private List<Crew> GetCrewsByDate(DateTime d) => Data.GetOrAdd(d, CalendarDemoDataHelper.GetCrewsByDate);

    private int GetSumByName(string name) => Data.Where(d => d.Key.Month == CrewInfoValue.Month).Sum(d => d.Value.FirstOrDefault(v => v.Name == name)?.Value ?? 0);

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new EventItem()
        {
            Name = "ValueChanged",
            Description = Localizer["ValueChanged"],
            Type ="EventCallback<DateTime>"
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
            Name = "Value",
            Description = Localizer["Value"],
            Type = "DateTime",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["ChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "CellTemplate",
            Description = Localizer["CellTemplate"],
            Type = "RenderFragment<CalendarCellValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Calendar.FirstDayOfWeek),
            Description = Localizer["FirstDayOfWeek"],
            Type = "DayOfWeek",
            ValueList = " — ",
            DefaultValue = "Sunday"
        }
    ];
}
