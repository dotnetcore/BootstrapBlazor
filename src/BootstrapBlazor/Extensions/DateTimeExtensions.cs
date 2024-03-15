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

    /// <summary>
    /// 获得阴历时间
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static DateTime ToLunar(this DateTime dt)
    {
        var calendar = new ChineseLunisolarCalendar();
        var year = calendar.GetYear(dt);
        var month = calendar.GetMonth(dt);
        var day = calendar.GetDayOfMonth(dt);

        // 检查闰月
        var leap = calendar.GetLeapMonth(year);
        if (leap > 0 && leap <= month)
        {
            month--;
        }
        return new DateTime(year, month, day);
    }

    private static List<string> Months = ["正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二"];
    private static string[] Days = ["初", "十", "廿", "三"];
    private static string[] DaysLeft = ["一", "二", "三", "四", "五", "六", "七", "八", "九", "十"];
}
