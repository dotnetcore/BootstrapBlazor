﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 倒计时按钮组件
/// </summary>
public partial class CountButtons
{
    private static Task OnClick() => Task.Delay(2000);

    private string CountTextCallback(int count) => Localizer["CountButtonText", count];

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(CountButton.Count),
            Description = Localizer["Count"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountButton.CountText),
            Description = Localizer["CountText"],
            Type = "CountOption",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountButton.CountTextCallback),
            Description = Localizer["CountTextCallback"],
            Type = "Func<int, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
