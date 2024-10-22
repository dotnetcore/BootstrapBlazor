// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// BreakPoint 枚举
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BreakPoint
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,

    /// <summary>
    /// 超小屏幕 小于 375px
    /// </summary>
    ExtraExtraSmall,

    /// <summary>
    /// 超小屏幕 大于等于 375px
    /// </summary>
    ExtraSmall,

    /// <summary>
    /// 小屏幕 大于等于 576px
    /// </summary>
    Small,

    /// <summary>
    /// 中屏幕 大于等于 768px
    /// </summary>
    Medium,

    /// <summary>
    /// 大屏幕 大于等于 992px
    /// </summary>
    Large,

    /// <summary>
    /// 超大屏幕 大于等于 1200px
    /// </summary>
    ExtraLarge,

    /// <summary>
    /// 超大屏幕 大于等于 1400px
    /// </summary>
    ExtraExtraLarge
}
