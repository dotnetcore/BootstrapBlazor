// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Extensions;

/// <summary>
/// DateTimeRangeValue 扩展方法
/// </summary>
public static class DateTimeRangeValueExtensions
{
    /// <summary>
    /// 获得开始时间 为空时返回 DateTime.MinValue
    /// </summary>
    /// <returns></returns>
    public static DateTime GetStartValue(this DateTimeRangeValue value) => value.Start ?? DateTime.MinValue;

    /// <summary>
    /// 获得结束时间 为空时返回 DateTime.MinValue
    /// </summary>
    /// <returns></returns>
    public static DateTime GetEndValue(this DateTimeRangeValue value) => value.End ?? DateTime.MinValue;
}
