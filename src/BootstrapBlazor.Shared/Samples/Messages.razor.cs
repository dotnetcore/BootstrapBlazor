// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Messages
/// </summary>
public sealed partial class Messages
{
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Placement",
            Description = "message popup location",
            Type = "Placement",
            ValueList = "Top|Bottom",
            DefaultValue = "Top"
        }
    };

    /// <summary>
    /// get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetMessageItemAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ChildContent",
            Description = "Content",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Class",
            Description = "Style",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Color",
            Description = "Color",
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new AttributeItem() {
            Name = "Icon",
            Description = "Icon",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ShowDismiss",
            Description = "Show close button",
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowBar",
            Description = "Whether to show the left Bar",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
