// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">BodyTemplateContext 上下文类</para>
///  <para lang="en">BodyTemplateContext context class</para>
/// </summary>
public class BodyTemplateContext
{
    /// <summary>
    ///  <para lang="zh">获得/设置 当前星期日数据集合</para>
    ///  <para lang="en">Gets or sets the current sunday data collection</para>
    /// </summary>
    public List<CalendarCellValue> Values { get; } = [];
}
