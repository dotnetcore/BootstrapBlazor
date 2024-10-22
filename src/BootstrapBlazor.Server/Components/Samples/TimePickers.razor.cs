// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
