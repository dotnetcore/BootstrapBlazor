// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 日历 HeaderTemplate 组件
/// </summary>
public partial class CalendarHeaderTemplate
{
    /// <summary>
    /// 获得/设置 是否显示周视图 默认为 CalendarVieModel.Month 月视图
    /// </summary>
    [Parameter]
    public CalendarViewMode ViewMode { get; set; }
}
