// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">交叉检测项实例类
///</para>
/// <para lang="en">交叉检测项instance类
///</para>
/// </summary>

public class IntersectionObserverEntry
{
    /// <summary>
    /// <para lang="zh">获得/设置 检测项与根元素交叉比率 0 - 1 之间
    ///</para>
    /// <para lang="en">Gets or sets 检测项与根元素交叉比率 0 - 1 之间
    ///</para>
    /// </summary>
    public float IntersectionRatio { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否正在交叉
    ///</para>
    /// <para lang="en">Gets or sets whether正在交叉
    ///</para>
    /// </summary>
    public bool IsIntersecting { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前项索引
    ///</para>
    /// <para lang="en">Gets or sets 当前项index
    ///</para>
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 与文档创建时间差
    ///</para>
    /// <para lang="en">Gets or sets 与文档创建时间差
    ///</para>
    /// </summary>
    public double Time { get; set; }
}
