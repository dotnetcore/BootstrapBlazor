// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Topology 数据项实体类
/// </summary>
public class TopologyItem
{
    /// <summary>
    /// 
    /// </summary>
    public string? ID { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public double ShowChild { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? TextColor { get; set; }
}
