// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 休假接口
/// </summary>
public interface ICalendarHolidays
{
    /// <summary>
    /// 是否为假日
    /// </summary>
    /// <returns></returns>
    bool IsHoliday(DateTime dt);

    /// <summary>
    /// 是否为工作日
    /// </summary>
    /// <returns></returns>
    bool IsWorkday(DateTime dt);
}
