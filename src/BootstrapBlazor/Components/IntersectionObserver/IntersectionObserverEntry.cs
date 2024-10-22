// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 交叉检测项实例类
/// </summary>

public class IntersectionObserverEntry
{
    /// <summary>
    /// 获得/设置 检测项与根元素交叉比率 0 - 1 之间
    /// </summary>
    public float IntersectionRatio { get; set; }

    /// <summary>
    /// 获得/设置 是否正在交叉
    /// </summary>
    public bool IsIntersecting { get; set; }

    /// <summary>
    /// 获得/设置 当前项索引
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 获得/设置 与文档创建时间差
    /// </summary>
    public double Time { get; set; }
}
