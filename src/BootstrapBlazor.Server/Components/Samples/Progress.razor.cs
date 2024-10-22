// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Progress
/// </summary>
public sealed partial class Progress
{
    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
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
    ];
}
