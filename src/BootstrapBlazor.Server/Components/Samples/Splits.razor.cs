// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Splits
/// </summary>
public sealed partial class Splits
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "IsVertical",
            Description = Localizer["SplitsIsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Basis",
            Description = Localizer["SplitsBasis"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "50%"
        },
        new()
        {
            Name = "FirstPanelTemplate",
            Description = Localizer["SplitsFirstPanelTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SecondPanelTemplate",
            Description = Localizer["SplitsSecondPanelTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
