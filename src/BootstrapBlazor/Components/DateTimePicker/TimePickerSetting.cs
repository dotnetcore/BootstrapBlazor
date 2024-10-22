// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePicker 配置项
/// </summary>
public class TimePickerSetting : ComponentBase
{
    /// <summary>
    /// 是否显示表盘刻度 默认 false
    /// </summary>
    [Parameter]
    public bool ShowClockScale { get; set; }

    /// <summary>
    /// 是否显示秒 默认 true
    /// </summary>
    [Parameter]
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// 是否显示分钟 默认 true
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// 是否自动切换 小时、分钟、秒 自动切换 默认 true
    /// </summary>
    [Parameter]
    public bool IsAutoSwitch { get; set; } = true;

    [CascadingParameter]
    private TimePickerOption? Option { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Option != null)
        {
            Option.ShowClockScale = ShowClockScale;
            Option.ShowSecond = ShowSecond;
            Option.ShowMinute = ShowMinute;
            Option.IsAutoSwitch = IsAutoSwitch;
        }
    }
}
