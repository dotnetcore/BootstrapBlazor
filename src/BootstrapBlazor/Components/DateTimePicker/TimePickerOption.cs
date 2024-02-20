// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 配置类
/// </summary>
internal class TimePickerOption
{
    /// <summary>
    /// 是否显示表盘刻度 默认 false
    /// </summary>
    public bool ShowClockScale { get; set; }

    /// <summary>
    /// 是否显示秒 默认 true
    /// </summary>
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// 是否显示分钟 默认 true
    /// </summary>
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// 是否自动切换 小时、分钟、秒 自动切换 默认 true
    /// </summary>
    public bool IsAutoSwitch { get; set; } = true;
}
