// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Rates
/// </summary>
public sealed partial class Rates
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private double BindValue { get; set; } = 3.0;

    private double BindValue1 { get; set; } = 2;

    private double BindValue2 { get; set; } = 2;

    private double BindValue3 { get; set; } = 2.8;

    private bool IsDisable { get; set; }

    private void OnValueChanged(double val)
    {
        BindValue = val;
        Logger.Log($"{Localizer["RatesLog"]} {val}");
    }

    private double IconListValue { get; set; } = 1;

    private List<string> IconList { get; } =
    [
        "fa-solid fa-face-sad-cry",
        "fa-solid fa-face-sad-tear",
        "fa-solid fa-face-smile",
        "fa-solid fa-face-surprise",
        "fa-solid fa-face-grin-stars"
    ];

    private string GetIconList(int index) => IconList[index - 1];

    private string GetIconValueChanged() => (IconListValue - 1) switch
    {
        0 => Localizer["RatesCry"],
        1 => Localizer["RatesTear"],
        2 => Localizer["RatesSmile"],
        3 => Localizer["RatesSurprise"],
        _ => Localizer["RatesGrin"]
    };
}
