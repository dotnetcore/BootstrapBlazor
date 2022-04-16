// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum BreakPoint
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,

    /// <summary>
    /// 未设置
    /// </summary>
    ExtraSmall,

    /// <summary>
    /// 小屏幕 576px
    /// </summary>
    Small,

    /// <summary>
    /// 中屏幕 768px
    /// </summary>
    Medium,

    /// <summary>
    /// 大屏幕 992px
    /// </summary>
    Large,

    /// <summary>
    /// 超大屏幕 1200px
    /// </summary>
    ExtraLarge,

    /// <summary>
    /// 超大屏幕 1400px
    /// </summary>
    ExtraExtraLarge
}
