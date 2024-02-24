// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// TimePicker 组件示例代码
/// </summary>
public partial class TimePickers
{
    [Inject]
    [NotNull]
    private IStringLocalizer<TimePickers>? Localizer { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private TimeSpan Value { get; set; } = DateTime.Now - DateTime.Today;

    private TimeSpan SecondValue { get; set; } = TimeSpan.FromMinutes(1.5);

    private Task OnConfirm(TimeSpan ts)
    {
        Value = ts;
        Logger.Log($"Value: {Value:hh\\:mm\\:ss}");
        return Task.CompletedTask;
    }
}
