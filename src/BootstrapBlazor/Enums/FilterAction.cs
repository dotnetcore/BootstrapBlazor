// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">关系运算符</para>
/// <para lang="en">Relational Operator</para>
/// </summary>
public enum FilterAction
{
    /// <summary>
    /// <para lang="zh">等于</para>
    /// <para lang="en">Equal to</para>
    /// </summary>
    [Description("等于")]
    Equal,

    /// <summary>
    /// <para lang="zh">不等于</para>
    /// <para lang="en">Not equal to</para>
    /// </summary>
    [Description("不等于")]
    NotEqual,

    /// <summary>
    /// <para lang="zh">大于</para>
    /// <para lang="en">Greater than</para>
    /// </summary>
    [Description("大于")]
    GreaterThan,

    /// <summary>
    /// <para lang="zh">大于等于</para>
    /// <para lang="en">Greater than or equal to</para>
    /// </summary>
    [Description("大于等于")]
    GreaterThanOrEqual,

    /// <summary>
    /// <para lang="zh">小于</para>
    /// <para lang="en">Less than</para>
    /// </summary>
    [Description("小于")]
    LessThan,

    /// <summary>
    /// <para lang="zh">小于等于</para>
    /// <para lang="en">Less than or equal to</para>
    /// </summary>
    [Description("小于等于")]
    LessThanOrEqual,

    /// <summary>
    /// <para lang="zh">包含</para>
    /// <para lang="en">Contains</para>
    /// </summary>
    [Description("包含")]
    Contains,

    /// <summary>
    /// <para lang="zh">不包含</para>
    /// <para lang="en">Not contains</para>
    /// </summary>
    [Description("不包含")]
    NotContains,

    /// <summary>
    /// <para lang="zh">自定义条件</para>
    /// <para lang="en">Custom Predicate</para>
    /// </summary>
    [Description("自定义条件")]
    CustomPredicate
}
