// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FlipClock 示例代码
/// </summary>
public partial class FlipClocks
{
    private int HeightValue { get; set; } = 100;

    private int FontSizeValue { get; set; } = 46;

    private int CardHeightValue { get; set; } = 60;

    private int CardWidthValue { get; set; } = 46;

    private int CardMarginValue { get; set; } = 5;

    private int CardGroupMarginValue { get; set; } = 20;

    private string HeightValueString => $"{HeightValue}px";

    private string FontSizeValueString => $"{FontSizeValue}px";

    private string CardHeightValueString => $"{CardHeightValue}px";

    private string CardWidthValueString => $"{CardWidthValue}px";

    private string CardMarginValueString => $"{CardMarginValue}px";

    private string CardGroupMarginValueString => $"{CardGroupMarginValue}px";

    private bool _isCompleted;
    private bool _showYear = false;
    private bool _showMonth = false;
    private bool _showDay = false;
    private bool _showHour = true;
    private bool _showMinute = true;
    private bool _showSecond = true;

    private Task OnCompletedAsync()
    {
        _isCompleted = true;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
