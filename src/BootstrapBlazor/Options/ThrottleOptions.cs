// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 限流配置类
/// </summary>
public class ThrottleOptions
{
    /// <summary>
    /// 获得/设置 限流时长 默认 500 单位毫秒
    /// </summary>
    public TimeSpan Interval { get; set; } = TimeSpan.FromMilliseconds(500);

    /// <summary>
    /// 获得/设置 是否执行结束后开始延时 默认 false
    /// </summary>
    public bool DelayAfterExecution { get; set; }

    /// <summary>
    /// 获得/设置 发生错误时是否重置时长 默认 false
    /// </summary>
    public bool ResetIntervalOnException { get; set; }
}
