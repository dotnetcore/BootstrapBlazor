// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// PopoverConfirms
/// </summary>
public sealed partial class PopoverConfirms
{
    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(PopConfirmButton.IsLink),
            Description = "Whether to render the component for the A tag",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Text",
            Description = "Show title",
            Type = "string",
            ValueList = "",
            DefaultValue = "Delete"
        },
        new AttributeItem() {
            Name = "Icon",
            Description = "Button icon",
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-solid fa-xmark"
        },
        new AttributeItem() {
            Name = "CloseButtonText",
            Description = "Close button display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Close"
        },
        new AttributeItem() {
            Name = "CloseButtonColor",
            Description = "Confirm button color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "Secondary"
        },
        new AttributeItem() {
            Name = "Color",
            Description = "Color",
            Type = "Color",
            ValueList = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "ConfirmButtonText",
            Description = "Confirm button display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Ok"
        },
        new AttributeItem() {
            Name = "ConfirmButtonColor",
            Description = "Confirm button color",
            Type = "None / Active / Primary / Secondary / Success / Danger / Warning / Info / Light / Dark / Link",
            ValueList = "",
            DefaultValue = "Primary"
        },
        new AttributeItem() {
            Name = "ConfirmIcon",
            Description = "Confirmation box icon",
            Type = "string",
            ValueList = "",
            DefaultValue = "fa-solid fa-circle-exclamation text-info"
        },
        new AttributeItem() {
            Name = "Content",
            Description = "Display text",
            Type = "string",
            ValueList = "",
            DefaultValue = "Confirm delete?"
        },
        new AttributeItem() {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "Popover Popup title",
            Type = "string",
            ValueList = "",
            DefaultValue = " "
        }
    };

    /// <summary>
    /// Get event method
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnConfirm",
            Description="Callback method when confirm is clicked",
            Type ="Func<Task>"
        },
        new EventItem()
        {
            Name = "OnClose",
            Description="Callback method when click close",
            Type ="Func<Task>"
        },
        new EventItem()
        {
            Name = "OnBeforeClick",
            Description="Click the callback method before confirming the pop-up window",
            Type ="Func<Task<bool>>"
        }
    };
}
