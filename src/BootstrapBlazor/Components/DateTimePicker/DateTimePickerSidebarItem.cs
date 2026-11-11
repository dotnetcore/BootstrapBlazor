// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DateTimePicker 组件侧边栏快捷项目类</para>
/// <para lang="en">DateTimePicker Component Sidebar Item Class</para>
/// <para><version>11.1.0</version></para>
/// </summary>
public class DateTimePickerSidebarItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 快捷项目文本</para>
    /// <para lang="en">Gets or sets Item Text</para>
    /// <para><version>11.1.0</version></para>
    /// </summary>
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 快捷项目值</para>
    /// <para lang="en">Gets or sets Item Value</para>
    /// <para><version>11.1.0</version></para>
    /// </summary>
    public DateTime Value { get; set; }
}
