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
}
