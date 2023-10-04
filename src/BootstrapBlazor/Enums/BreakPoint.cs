// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
