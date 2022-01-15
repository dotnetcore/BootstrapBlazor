// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    [Description("ssm-down")]
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
    /// xl 超大设置小于 1400px
    /// </summary>
    [Description("fullscreen-xxl-down")]
    ExtraExtraLarge
}
