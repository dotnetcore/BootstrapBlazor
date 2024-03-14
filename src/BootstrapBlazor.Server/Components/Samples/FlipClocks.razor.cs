// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// FlipClock 示例代码
/// </summary>
public partial class FlipClocks
{
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
            Name = nameof(FlipClock.ShowHour),
            Description = "Excel/Word 文件路径或者URL",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
