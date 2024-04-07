// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
