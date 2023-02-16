// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Lights
/// </summary>
public partial class Lights
{
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(Light.Color),
            Description = "Color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Success"
        },
        new AttributeItem() {
            Name = nameof(Light.IsFlash),
            Description = "Is it flashing",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(Light.TooltipText),
            Description = "Indicator tooltip Display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Light.TooltipTrigger),
            Description = "Indicator tooltip trigger type",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
