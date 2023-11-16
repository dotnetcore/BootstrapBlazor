// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Footers
/// </summary>
public sealed partial class Footers
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Text",
            Description = Localizer["Desc1"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Target",
            Description = Localizer["Desc2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Footer.ShowGoto),
            Description = Localizer["ShowGotoDesc"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Footer.ChildContent),
            Description = Localizer["ChildContentDesc"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
