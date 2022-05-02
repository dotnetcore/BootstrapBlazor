// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Extensions;

internal static class DateTimeExtensions
{
    public static DateTime GetSafeYearDateTime(this DateTime dt, int year)
    {
        var @base = dt;
        if (year < 0)
        {
            if (DateTime.MinValue.AddYears(0 - year) < dt)
            {
                @base = dt.AddYears(year);
            }
            else
            {
                @base = DateTime.MinValue;
            }
        }
        else if (year > 0)
        {
            if (DateTime.MaxValue.AddYears(0 - year) > dt)
            {
                @base = dt.AddYears(year);
            }
            else
            {
                @base = DateTime.MaxValue;
            }
        }
        return @base;
    }

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
                @base = DateTime.MinValue;
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
                @base = DateTime.MaxValue;
            }
        }
        return @base;
    }

    public static DateTime GetSafeDayDateTime(this DateTime dt, int day)
    {
        var @base = dt;
        if (day < 0)
        {
            if (DateTime.MinValue.AddDays(0 - day) < dt)
            {
                @base = dt.AddDays(day);
            }
            else
            {
                @base = DateTime.MinValue;
            }
        }
        else if (day > 0)
        {
            if (DateTime.MaxValue.AddDays(0 - day) > dt)
            {
                @base = dt.AddDays(day);
            }
            else
            {
                @base = DateTime.MaxValue;
            }
        }
        return @base;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="day"></param>
    /// <returns></returns>
    public static bool IsDayOverflow(this DateTime dt, int day) => DateTime.MaxValue.AddDays(0 - day) < dt;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="year"></param>
    /// <returns></returns>
    public static bool IsYearOverflow(this DateTime dt, int year)
    {
        var ret = false;
        if (year < 0)
        {
            ret = DateTime.MinValue.AddYears(0 - year) > dt;
        }
        else if (year > 0)
        {
            ret = DateTime.MaxValue.AddYears(0 - year) < dt;
        }
        return ret;
    }
}
