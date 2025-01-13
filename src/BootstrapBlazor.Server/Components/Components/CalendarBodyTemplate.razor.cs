// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 日历 BodyTemplate 组件
/// </summary>
public partial class CalendarBodyTemplate
{
    /// <summary>
    /// 获得/设置 月视图 BodyTempalte 上下文
    /// </summary>
    [Parameter]
    [EditorRequired]
    [NotNull]
    public BodyTemplateContext? Context { get; set; }
}
