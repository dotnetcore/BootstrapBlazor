// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Popovers
/// </summary>
public sealed partial class Popovers
{
    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = "Cotent",
            Description = "Popover content",
            Type = "string",
            ValueList = "",
            DefaultValue = "Popover"
        },
        new AttributeItem()
        {
            Name = "IsHtml",
            Description = "Whether the content contains Html code",
            Type = "boolean",
            ValueList = "",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        },
        new AttributeItem()
        {
            Name = "Title",
            Description = "Popover title",
            Type = "string",
            ValueList = "",
            DefaultValue = "Popover"
        }
    };
}
