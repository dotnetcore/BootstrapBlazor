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

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(FlipClock.ShowYear),
            Description = Localizer["ShowYear_Description"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(FlipClock.ShowMonth),
            Description = Localizer["ShowMonth_Description"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(FlipClock.ShowDay),
            Description = Localizer["ShowDay_Description"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(FlipClock.ShowHour),
            Description = Localizer["ShowHour_Description"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(FlipClock.ShowMinute),
            Description = Localizer["ShowMinute_Description"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(FlipClock.ViewMode),
            Description = Localizer["ViewMode_Description"],
            Type = "FlipClockViewMode",
            ValueList = "DateTime|Count|CountDown",
            DefaultValue = "DateTime"
        },
        new()
        {
            Name = nameof(FlipClock.StartValue),
            Description = Localizer["StartValue_Description"],
            Type = "TimeSpan",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.OnCompletedAsync),
            Description = Localizer["OnCompletedAsync_Description"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.Height),
            Description = Localizer["Height_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.BackgroundColor),
            Description = Localizer["BackgroundColor_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.FontSize),
            Description = Localizer["FontSize_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardWidth),
            Description = Localizer["CardWidth_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardHeight),
            Description = Localizer["CardHeight_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardColor),
            Description = Localizer["CardColor_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardBackgroundColor),
            Description = Localizer["CardBackgroundColor_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardDividerHeight),
            Description = Localizer["CardDividerHeight_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardDividerColor),
            Description = Localizer["CardDividerColor_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardMargin),
            Description = Localizer["CardMargin_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardGroupMargin),
            Description = Localizer["CardGroupMargin_Description"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
