// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">函数枚举</para>
/// <para lang="en">Aggregate Functions Enum</para>
/// </summary>
public enum AggregateType
{
    /// <summary>
    /// <para lang="zh">合计</para>
    /// <para lang="en">Sum</para>
    /// </summary>
    Sum,
    /// <summary>
    /// <para lang="zh">平均数</para>
    /// <para lang="en">Average</para>
    /// </summary>
    Average,
    /// <summary>
    /// <para lang="zh">数量</para>
    /// <para lang="en">Count</para>
    /// </summary>
    Count,
    /// <summary>
    /// <para lang="zh">最小值</para>
    /// <para lang="en">Min</para>
    /// </summary>
    Min,
    /// <summary>
    /// <para lang="zh">最大值</para>
    /// <para lang="en">Max</para>
    /// </summary>
    Max,
    /// <summary>
    /// <para lang="zh">自定义</para>
    /// <para lang="en">Custom</para>
    /// </summary>
    Customer
}
