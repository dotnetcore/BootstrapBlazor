// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class GanttItem
{
    /// <summary>
    /// 获得或设置 任务编号
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 获得或设置 任务名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///  获得或设置 任务开始时间
    /// </summary>
    public string? Start { get; set; }

    /// <summary>
    ///  获得或设置 任务结束时间
    /// </summary>
    public string? End { get; set; }

    /// <summary>
    ///  获得或设置 任务进度
    /// </summary>
    public int Progress { get; set; }

    /// <summary>
    /// 获得或设置 任务依赖
    /// </summary>
    public string? Dependencies { get; set; }

}
