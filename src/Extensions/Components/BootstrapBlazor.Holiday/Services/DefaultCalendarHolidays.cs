// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System.Globalization;
using System.Text.Json;

namespace BootstrapBlazor.Holiday.Services;

class DefaultCalendarHolidays : ICalendarHolidays
{
    public DefaultCalendarHolidays()
    {
        var option = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        var assembly = GetType().Assembly;
        foreach (var file in assembly.GetManifestResourceNames())
        {
            using var stream = assembly.GetManifestResourceStream(file);
            if (stream != null)
            {
                var items = JsonSerializer.Deserialize(stream, typeof(List<HolidayItem>), option);
                if (items is List<HolidayItem> list)
                {
                    _holidays.AddRange(list.Where(i => i.Type == "holiday"));
                    _workdays.AddRange(list.Where(i => i.Type == "workingday"));
                }
            }
        }
    }

    private readonly List<HolidayItem> _holidays = [];
    private readonly List<HolidayItem> _workdays = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="dt"></param>
    public bool IsHoliday(DateTime dt) => _holidays.Find(i => FindHoliday(i, dt)) != null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="dt"></param>
    public bool IsWorkday(DateTime dt) => _workdays.Find(i => FindHoliday(i, dt)) != null;

    private static bool FindHoliday(HolidayItem item, DateTime value)
    {
        var ret = false;
        if (item.Range != null)
        {
            if (item.Range.Count == 1 && DateTime.TryParseExact(item.Range[0], "yyyy-MM-dd", null, DateTimeStyles.None, out var v))
            {
                ret = v == value;
            }
            else if (item.Range.Count == 2
                && DateTime.TryParseExact(item.Range[0], "yyyy-MM-dd", null, DateTimeStyles.None, out var v1)
                && DateTime.TryParseExact(item.Range[1], "yyyy-MM-dd", null, DateTimeStyles.None, out var v2))
            {
                ret = value >= v1 && value <= v2;
            }
        }
        return ret;
    }

    class HolidayItem
    {
        public string? Name { get; set; }

        public List<string>? Range { get; set; }

        public string? Type { get; set; }
    }
}
