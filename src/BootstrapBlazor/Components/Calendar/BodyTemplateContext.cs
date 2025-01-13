﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// BodyTemplateContext 上下文类
/// </summary>
public class BodyTemplateContext
{
    /// <summary>
    /// 获得/设置 当前星期日数据集合
    /// </summary>
    public List<CalendarCellValue> Values { get; } = [];
}
