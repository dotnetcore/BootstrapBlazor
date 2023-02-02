// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Drawers
/// </summary>
public sealed partial class Drawers
{
    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Width",
            Description = "drawer width",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "360px"
        },
        new AttributeItem() {
            Name = "Height",
            Description = "drawer height",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "290px"
        },
        new AttributeItem() {
            Name = "IsOpen",
            Description = "Is the drawer open?",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsBackdrop",
            Description = "Whether click on the mask closes the drawer",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "OnClickBackdrop",
            Description = "Callback delegate method when background mask is clicked",
            Type = "Action",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Placement",
            Description = "Where the component appears",
            Type = "Placement",
            ValueList = "Left|Right|Top|Bottom",
            DefaultValue = "Left"
        },
        new AttributeItem() {
            Name = "ChildContent",
            Description = "Subassembly",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
