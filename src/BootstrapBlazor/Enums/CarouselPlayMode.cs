// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Carousel 组件播放方式枚举</para>
/// <para lang="en">Carousel Play Mode Enum</para>
/// </summary>
public enum CarouselPlayMode
{
    /// <summary>
    /// <para lang="zh">加载后自动播放</para>
    /// <para lang="en">Auto Play On Load</para>
    /// </summary>
    [Description("carousel")]
    AutoPlayOnload,

    /// <summary>
    /// <para lang="zh">用户点击按钮后自动播放</para>
    /// <para lang="en">Auto Play After Manually Click</para>
    /// </summary>
    [Description("true")]
    AutoPlayAfterManually,

    /// <summary>
    /// <para lang="zh">用户控制</para>
    /// <para lang="en">Manually</para>
    /// </summary>
    [Description("false")]
    Manually
}
