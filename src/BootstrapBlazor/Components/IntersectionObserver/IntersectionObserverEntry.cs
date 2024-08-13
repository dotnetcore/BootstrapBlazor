// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
