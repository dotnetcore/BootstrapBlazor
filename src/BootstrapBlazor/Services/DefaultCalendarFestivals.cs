// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultCalendarFestivals : ICalendarFestivals
{
    private static readonly Dictionary<string, string> Festivals = new() {
        {"0101", "元旦"},
        {"0214", "情人节"},
        {"0308", "妇女节"},
        {"0312", "植树节"},
        {"0401", "愚人节"},
        {"0501", "劳动节"},
        {"0504", "青年节"},
        {"0601", "儿童节"},
        {"0701", "建党节"},
        {"0801", "建军节"},
        {"1001", "国庆节"},
        {"1225", "圣诞节"}
    };

    private static readonly Dictionary<string, string> LunarFestivals = new() {
        {"0101", "春节"},
        {"0115", "元宵节"},
        {"0505", "端午节"},
        {"0815", "中秋节"},
        {"0909", "重阳节"},
        {"1208", "腊八节"},
        {"1230", "除夕"},
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string? GetFestival(DateTime dt)
    {
        string? ret = null;
        var key = $"{dt:MMdd}";
        var (_, Month, Day) = dt.ToLunarDateTime();
        var lunarKey = $"{Month:00}{Day:00}";
        if (LunarFestivals.TryGetValue(lunarKey, out var v1))
        {
            ret = v1;
        }
        else if (Festivals.TryGetValue(key, out var v2))
        {
            ret = v2;
        }
        return ret;
    }
}
