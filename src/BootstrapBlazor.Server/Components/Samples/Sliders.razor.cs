// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Slider 组件示例
/// </summary>
public partial class Sliders
{
    private string DisplayText { get; set; } = "Range";

    private double MaxValue { get; set; } = 100;

    private double MinValue { get; set; } = -100;

    private double CurrentValue { get; set; } = 0;

    private double Step { get; set; } = 20;

    private bool IsDisabled { get; set; }

    private bool UseInput { get; set; }

    private bool UseGroup { get; set; }

    private bool ShowLabel { get; set; } = true;

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Range(10, 100)]
    public int RangeValue { get; set; } = 20;

    private Task OnRangeSliderValueChanged(double value)
    {
        Logger.Log($"RangeSlider: Bind Value: {value}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["SlidersIsDisabled"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Value",
            Description = Localizer["SlidersValue"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
    ];

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "ValueChanged",
            Description = Localizer["SlidersValueChanged"],
            Type ="EventCallback<int>"
        }
    ];
}
