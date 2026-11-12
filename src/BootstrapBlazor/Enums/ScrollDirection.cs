// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">滚动方向枚举</para>
/// <para lang="en">Scroll direction enumeration</para>
/// </summary>
public enum ScrollDirection
{
    /// <summary>
    /// <para lang="zh">滚动到底部，默认值</para>
    /// <para lang="en">Scrolls to the bottom. Default value</para>
    /// </summary>
    [Description("bottom")]
    Bottom,

    /// <summary>
    /// <para lang="zh">滚动到顶部</para>
    /// <para lang="en">Scrolls to the top</para>
    /// </summary>
    [Description("top")]
    Top,

    /// <summary>
    /// <para lang="zh">滚动到最左侧</para>
    /// <para lang="en">Scrolls to the left edge</para>
    /// </summary>
    [Description("left")]
    Left,

    /// <summary>
    /// <para lang="zh">滚动到最右侧</para>
    /// <para lang="en">Scrolls to the right edge</para>
    /// </summary>
    [Description("right")]
    Right
}
