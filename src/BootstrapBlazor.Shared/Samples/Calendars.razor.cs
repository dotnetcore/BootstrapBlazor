// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections.Concurrent;

namespace BootstrapBlazor.Shared.Samples;

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

    private static string Formatter(DateTime ts) => ts.ToString("yyyy-MM-dd");

    private ConcurrentDictionary<DateTime, List<Crew>> Data { get; } = new();

    private DateTime CrewInfoValue { get; set; } = DateTime.Today;

    private List<Crew> GetCrewsByDate(DateTime d) => Data.GetOrAdd(d, CalendarDemoDataHelper.GetCrewsByDate);

    private int GetSumByName(string name) => Data.Where(d => d.Key.Month == CrewInfoValue.Month).Sum(d => d.Value.FirstOrDefault(v => v.Name == name)?.Value ?? 0);

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "ValueChanged",
            Description = Localizer["ValueChanged"],
            Type ="EventCallback<DateTime>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["Value"],
            Type = "DateTime",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["ChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "CellTemplate",
            Description = Localizer["CellTemplate"],
            Type = "RenderFragment<CalendarCellValue>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
