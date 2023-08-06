// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// GroupBoxes
/// </summary>
public sealed partial class GroupBoxes
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Title",
            Description = Localizer["AttTitle"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
