// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
