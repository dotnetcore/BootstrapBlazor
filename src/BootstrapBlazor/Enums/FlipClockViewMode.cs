// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">显示模式</para>
///  <para lang="en">display模式</para>
/// </summary>
public enum FlipClockViewMode
{
    /// <summary>
    ///  <para lang="zh">时间</para>
    ///  <para lang="en">时间</para>
    /// </summary>
    DateTime,
    /// <summary>
    ///  <para lang="zh">计数</para>
    ///  <para lang="en">计数</para>
    /// </summary>
    Count,
    /// <summary>
    ///  <para lang="zh">倒计时</para>
    ///  <para lang="en">倒计时</para>
    /// </summary>
    CountDown
}
