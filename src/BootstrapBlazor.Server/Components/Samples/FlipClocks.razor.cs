// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(FlipClock.ShowHour),
            Description = "是否显示小时",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(FlipClock.ShowMinute),
            Description = "是否显示分钟",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(FlipClock.ViewMode),
            Description = "是否显示分钟",
            Type = "FlipClockViewMode",
            ValueList = "DateTime|Count|CountDown",
            DefaultValue = "DateTime"
        },
        new()
        {
            Name = nameof(FlipClock.StartValue),
            Description = "开始时间",
            Type = "TimeSpan",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.OnCompletedAsync),
            Description = "计时结束回调方法",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.Height),
            Description = "组件高度",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.BackgroundColor),
            Description = "组件背景色",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.FontSize),
            Description = "组件字体大小",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardWidth),
            Description = "组件卡片宽度",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardHeight),
            Description = "组件卡片高度",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardColor),
            Description = "组件卡片字体颜色",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardBackgroundColor),
            Description = "组件卡片背景颜色",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardDividerHeight),
            Description = "组件卡片分割线高度",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardDividerColor),
            Description = "组件卡片分割线颜色",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardMargin),
            Description = "组件卡片间隔",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FlipClock.CardGroupMargin),
            Description = "组件卡片组间隔",
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
