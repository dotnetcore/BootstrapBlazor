// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ScrollOptions 配置类</para>
/// <para lang="en">ScrollOptions configuration class</para>
/// </summary>
public class ScrollOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 滚动条宽度 默认 5px</para>
    /// <para lang="en">Get/Set scroll width default 5px</para>
    /// </summary>
    public int ScrollWidth { get; set; } = 5;

    /// <summary>
    /// <para lang="zh">获得/设置 滚动条鼠标悬浮宽度 默认 5px</para>
    /// <para lang="en">Get/Set scroll hover width default 5px</para>
    /// </summary>
    public int ScrollHoverWidth { get; set; } = 5;
}
