// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ClockPicker 组件示例代码
/// </summary>
public partial class ClockPickers
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ClockPickers>? Localizer { get; set; }

    private TimeSpan Value { get; set; } = DateTime.Now - DateTime.Today;

    private TimeSpan SecondValue { get; set; } = TimeSpan.FromHours(12.5);

    private TimeSpan MinuteValue { get; set; } = TimeSpan.FromHours(12);

    private TimeSpan ScaleValue { get; set; } = TimeSpan.FromHours(12.5);

    private bool _autoSwitch = false;

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(ClockPicker.IsAutoSwitch),
            Description = Localizer["IsAutoSwitchAttr"],
            Type = "bool",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(ClockPicker.ShowClockScale),
            Description = Localizer["ShowClockScaleAttr"],
            Type = "bool",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(ClockPicker.ShowMinute),
            Description = Localizer["ShowMinuteAttr"],
            Type = "bool",
            ValueList = "true / false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(ClockPicker.ShowSecond),
            Description = Localizer["ShowSecondAttr"],
            Type = "bool",
            ValueList = "true / false",
            DefaultValue = "true"
        }
    ];
}
