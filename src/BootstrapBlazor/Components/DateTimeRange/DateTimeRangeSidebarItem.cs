// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimeRange 组件侧边栏快捷项目类
/// </summary>
public class DateTimeRangeSidebarItem
{
    /// <summary>
    /// 获得/设置 快捷项目文本
    /// </summary>
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 开始时间
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    /// 获得/设置 开始时间
    /// </summary>
    public DateTime EndDateTime { get; set; }
}
