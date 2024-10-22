// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
