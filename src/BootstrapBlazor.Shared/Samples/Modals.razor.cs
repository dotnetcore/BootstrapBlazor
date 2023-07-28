// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Modals
/// </summary>
public sealed partial class Modals
{
    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "FirstAfterRenderCallbackAsync",
            Description = Localizer["ModalsAttributesFirstAfterRenderCallbackAsync"],
            Type = "Func<Modal,Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["ModalsAttributeHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyTemplate",
            Description = Localizer["ModalsAttributeBodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
                Description = Localizer["ModalsAttributeChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FooterTemplate",
            Description = Localizer["ModalsAttributeFooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsBackdrop",
              Description = Localizer["ModalsAttributeIsBackdrop"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsKeyboard",
            Description = Localizer["ModalsAttributeIsKeyboard"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsCentered",
            Description = Localizer["ModalsAttributeIsCentered"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsScrolling",
                    Description = Localizer["ModalsAttributeIsScrolling"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsFade",
                       Description = Localizer["ModalsAttributeIsFade"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsDraggable",
                     Description = Localizer["ModalsAttributeIsDraggable"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowCloseButton",
              Description = Localizer["ModalsAttributeShowCloseButton"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowFooter",
            Description = Localizer["ModalsAttributeShowFooter"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Size",
            Description = Localizer["ModalsAttributeSize"],
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge / ExtraExtraLarge",
            DefaultValue = "ExtraExtraLarge"
        },
        new()
        {
            Name = nameof(ModalDialog.FullScreenSize),
            Description = Localizer["ModalsAttributeFullScreenSize"],
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge / ExtraExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "Title",
            Description = Localizer["ModalsAttributeTitle"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " not set "
        },
        new()
        {
            Name = nameof(ModalDialog.ShowMaximizeButton),
            Description = Localizer["ModalsAttributeShowMaximizeButton"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShownCallbackAsync",
            Description = Localizer["ModalsAttributeShownCallbackAsync"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
