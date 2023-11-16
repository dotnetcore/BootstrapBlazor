// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Progress
/// </summary>
public sealed partial class Progress
{
    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
            Name = "Class",
            Description = "Style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Height",
            Description = "Progress bar height",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "15"
        },
        new()
        {
            Name = "IsAnimated",
            Description = "Whether to display dynamically",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsShowValue",
            Description = "Whether to display the value",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsStriped",
            Description = "Whether to show stripes",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        }
    };
}
