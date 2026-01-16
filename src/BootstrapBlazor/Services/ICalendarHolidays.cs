// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">休假接口</para>
/// <para lang="en">Holiday Interface</para>
/// </summary>
public interface ICalendarHolidays
{
    /// <summary>
    /// <para lang="zh">是否为假日</para>
    /// <para lang="en">Is Holiday</para>
    /// </summary>
    /// <returns></returns>
    bool IsHoliday(DateTime dt);

    /// <summary>
    /// <para lang="zh">是否为工作日</para>
    /// <para lang="en">Is Workday</para>
    /// </summary>
    /// <returns></returns>
    bool IsWorkday(DateTime dt);
}
