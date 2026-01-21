// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">搜索栏渲染模式 默认 Popup 弹窗形式</para>
/// <para lang="en">搜索栏渲染模式 Default is Popup 弹窗形式</para>
/// </summary>
public enum SearchMode
{
    /// <summary>
    /// <para lang="zh">弹窗模式</para>
    /// <para lang="en">弹窗模式</para>
    /// </summary>
    Popup,

    /// <summary>
    /// <para lang="zh">Table 组件上方</para>
    /// <para lang="en">Table component上方</para>
    /// </summary>
    Top
}
