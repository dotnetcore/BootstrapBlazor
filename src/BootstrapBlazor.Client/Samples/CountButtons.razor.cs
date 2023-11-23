// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 倒计时按钮组件
/// </summary>
public partial class CountButtons
{
    private Task OnClick() => Task.Delay(2000);

    private string CountTextCallback(int count) => Localizer["CountButtonText", count];

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
    };
}
