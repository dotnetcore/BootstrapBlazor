// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 函数枚举
/// </summary>
public enum AggregateType
{
    /// <summary>
    /// 合计
    /// </summary>
    Sum,
    /// <summary>
    /// 平均数
    /// </summary>
    Average,
    /// <summary>
    /// 数量
    /// </summary>
    Count,
    /// <summary>
    /// 最小值
    /// </summary>
    Min,
    /// <summary>
    /// 最大值
    /// </summary>
    Max,
    /// <summary>
    /// 自定义
    /// </summary>
    Customer
}
