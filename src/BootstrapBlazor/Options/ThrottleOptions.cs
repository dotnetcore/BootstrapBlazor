// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">限流配置类</para>
///  <para lang="en">Throttle configuration class</para>
/// </summary>
public class ThrottleOptions
{
    /// <summary>
    ///  <para lang="zh">获得/设置 限流时长 默认 500 单位毫秒</para>
    ///  <para lang="en">Gets or sets throttle interval default 500 ms</para>
    /// </summary>
    public TimeSpan Interval { get; set; } = TimeSpan.FromMilliseconds(500);

    /// <summary>
    ///  <para lang="zh">获得/设置 是否执行结束后开始延时 默认 false</para>
    ///  <para lang="en">Gets or sets whether to delay after execution default false</para>
    /// </summary>
    public bool DelayAfterExecution { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 发生错误时是否重置时长 默认 false</para>
    ///  <para lang="en">Gets or sets whether to reset interval on exception default false</para>
    /// </summary>
    public bool ResetIntervalOnException { get; set; }
}
