// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Carousel 组件播放方式枚举
/// </summary>
public enum CarouselPlayMode
{
    /// <summary>
    /// 加载后自动播放
    /// </summary>
    [Description("carousel")]
    AutoPlayOnload,

    /// <summary>
    /// 用户点击按钮后自动播放
    /// </summary>
    [Description("true")]
    AutoPlayAfterManually,

    /// <summary>
    /// 用户控制
    /// </summary>
    [Description("false")]
    Manually
}
