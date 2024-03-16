// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTime 扩展方法
/// </summary>
internal static class DateTimeExtensions
{
    /// <summary>
    /// 获得安全的月份时间
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="month"></param>
    /// <returns></returns>
    public static DateTime GetSafeMonthDateTime(this DateTime dt, int month)
    {
        var @base = dt;
        if (month < 0)
        {
            if (DateTime.MinValue.AddMonths(0 - month) < dt)
            {
                @base = dt.AddMonths(month);
            }
            else
            {
                @base = DateTime.MinValue.Date;
            }
        }
        else if (month > 0)
        {
            if (DateTime.MaxValue.AddMonths(0 - month) > dt)
            {
                @base = dt.AddMonths(month);
            }
            else
            {
                @base = DateTime.MaxValue.Date;
            }
        }
        return @base;
    }

    private static readonly ChineseLunisolarCalendar calendar = new();

    /// <summary>
    /// 获得阴历时间方法
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static (int Year, int Month, int Day) ToLunarDateTime(this DateTime dt)
    {
        var year = calendar.GetYear(dt);
        var month = calendar.GetMonth(dt);
        var day = calendar.GetDayOfMonth(dt);

        // 检查闰月
        var leapMonth = calendar.GetLeapMonth(year);
        if (leapMonth > 0 && leapMonth <= month)
        {
            month--;
        }
        return (year, month, day);
    }

    /// <summary>
    /// 获得阴历信息
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="showSolarTerm"></param>
    /// <param name="calendarFestivals"></param>
    /// <returns></returns>
    public static string ToLunarText(this DateTime dt, bool showSolarTerm = false, ICalendarFestivals? calendarFestivals = null) => calendarFestivals?.GetFestival(dt)
        ?? (showSolarTerm
            ? dt.GetSolarTermName() ?? dt.GetLunarMonthName()
            : dt.GetLunarMonthName());

    static string GetLunarMonthName(this DateTime dt)
    {
        var year = calendar.GetYear(dt);
        var month = calendar.GetMonth(dt);
        var day = calendar.GetDayOfMonth(dt);

        // 检查闰月
        var leapMonth = calendar.GetLeapMonth(year);
        var isLeapMonth = false;
        if (leapMonth > 0 && leapMonth <= month)
        {
            isLeapMonth = leapMonth == month;
            month--;
        }
        var monthPrefix = isLeapMonth ? "闰" : string.Empty;
        return day == 1 ? $"{monthPrefix}{Months[month - 1]}月" : GetLunisolarDay(day);

        static string GetLunisolarDay(int day)
        {
            string? ret;
            if (day != 20 && day != 30)
            {
                ret = $"{Days[(day - 1) / 10]}{DaysLeft[(day - 1) % 10]}";
            }
            else
            {
                ret = $"{DaysLeft[(day - 1) / 10]}{Days[1]}";
            }
            return ret;
        }
    }

    public static string? GetSolarTermName(this DateTime dt)
    {
        var day1 = GetSolarTermDay(dt.Year, dt.Month * 2 - 1);
        var day2 = GetSolarTermDay(dt.Year, dt.Month * 2);
        string? ret = null;
        if (dt.Day == day1)
        {
            ret = SolarTerms[dt.Month * 2 - 2];
        }
        else if (dt.Day == day2)
        {
            ret = SolarTerms[dt.Month * 2 - 1];
        }
        return ret;

        static int GetSolarTermDay(int year, int n)
        {
            //1900年1月6日：小寒
            var minutes = 525948.766245 * (year - 1900) + SolarTermOffset[n - 1];
            var baseDate = new DateTime(1900, 1, 6, 2, 5, 0);
            var veryDate = baseDate.AddMinutes(minutes);
            return veryDate.Day;
        }
    }

    private static readonly string[] Months = ["正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊"];
    private static readonly string[] Days = ["初", "十", "廿", "三"];
    private static readonly string[] DaysLeft = ["一", "二", "三", "四", "五", "六", "七", "八", "九", "十"];
    private static readonly string[] SolarTerms = ["小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至"];
    private static readonly int[] SolarTermOffset = [0, 21208, 42467, 63836, 85337, 107014, 128867, 150921, 173149, 195551, 218072, 240693, 263343, 285989, 308563, 331033, 353350, 375494, 397447, 419210, 440795, 462224, 483532, 504758];
}
