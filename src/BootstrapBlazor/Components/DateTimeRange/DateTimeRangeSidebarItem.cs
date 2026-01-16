// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">DateTimeRange 组件侧边栏快捷项目类</para>
///  <para lang="en">DateTimeRange Component Sidebar Item Class</para>
/// </summary>
public class DateTimeRangeSidebarItem
{
    /// <summary>
    ///  <para lang="zh">获得/设置 快捷项目文本</para>
    ///  <para lang="en">Get/Set Item Text</para>
    /// </summary>
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 开始时间</para>
    ///  <para lang="en">Get/Set Start Time</para>
    /// </summary>
    public DateTime StartDateTime { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 结束时间</para>
    ///  <para lang="en">Get/Set End Time</para>
    /// </summary>
    public DateTime EndDateTime { get; set; }
}
