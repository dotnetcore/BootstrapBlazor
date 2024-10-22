﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultCalendarHolidays : ICalendarHolidays
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public bool IsHoliday(DateTime dt) => false;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public bool IsWorkday(DateTime dt) => false;
}
