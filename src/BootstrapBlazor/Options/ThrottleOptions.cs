// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
