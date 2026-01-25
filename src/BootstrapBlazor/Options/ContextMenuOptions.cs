// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ContextMenuOptions 配置类</para>
/// <para lang="en">ContextMenuOptions configuration class</para>
/// </summary>
public class ContextMenuOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 移动端触屏右键菜单延时时长 默认 500 毫秒</para>
    /// <para lang="en">Gets or sets mobile touch context menu delay duration default 500 ms</para>
    /// </summary>
    public int OnTouchDelay { get; set; } = 500;
}
