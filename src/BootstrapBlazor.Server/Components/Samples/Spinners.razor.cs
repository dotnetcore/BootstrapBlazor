// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Spinners
{
    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Class",
            Description = "style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = "Color",
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "Size",
            Description = "Size",
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "SpinnerType",
            Description = "Icon type",
            Type = "SpinnerType",
            ValueList = " Border / Grow ",
            DefaultValue = "SpinnerType.Border"
        }
    };
}
