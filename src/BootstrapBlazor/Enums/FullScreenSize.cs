// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Size 枚举类型
/// </summary>
public enum FullScreenSize
{
    /// <summary>
    /// 无设置
    /// </summary>
    None,

    /// <summary>
    /// 始终全屏
    /// </summary>
    [Description("fullscreen")]
    Always,

    /// <summary>
    /// sm 小设置小于 576px
    /// </summary>
    [Description("fullscreen-sm-down")]
    Small,

    /// <summary>
    /// md 中等设置小于 768px
    /// </summary>
    [Description("fullscreen-md-down")]
    Medium,

    /// <summary>
    /// lg 大设置小于 992px
    /// </summary>
    [Description("fullscreen-lg-down")]
    Large,

    /// <summary>
    /// xl 超大设置小于 1200px
    /// </summary>
    [Description("fullscreen-xl-down")]
    ExtraLarge,

    /// <summary>
    /// xxl 超大设置小于 1400px
    /// </summary>
    [Description("fullscreen-xxl-down")]
    ExtraExtraLarge
}
