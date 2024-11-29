﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// Modals
/// </summary>
public sealed partial class Modals
{
    [NotNull]
    private Modal? Modal { get; set; }

    [NotNull]
    private Modal? BackdropModal { get; set; }

    [NotNull]
    private Modal? SmallModal { get; set; }

    [NotNull]
    private Modal? SizeSmallModal { get; set; }

    [NotNull]
    private Modal? LargeModal { get; set; }

    [NotNull]
    private Modal? ExtraLargeModal { get; set; }

    [NotNull]
    private Modal? ExtraExtraLargeModal { get; set; }

    [NotNull]
    private Modal? SmallFullScreenModal { get; set; }

    [NotNull]
    private Modal? LargeFullScreenModal { get; set; }

    [NotNull]
    private Modal? ExtraLargeFullScreenModal { get; set; }

    [NotNull]
    private Modal? ExtraExtraLargeFullScreenModal { get; set; }

    [NotNull]
    private Modal? CenterModal { get; set; }

    [NotNull]
    private Modal? LongContentModal { get; set; }

    [NotNull]
    private Modal? ScrollModal { get; set; }

    [NotNull]
    private Modal? DragModal { get; set; }

    [NotNull]
    private Modal? MaximizeModal { get; set; }

    [NotNull]
    private Modal? ShownCallbackModal { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnShownCallbackAsync()
    {
        Logger.Log("弹窗已显示");
        return Task.CompletedTask;
    }

    private static async Task<bool> OnSaveAsync()
    {
        await Task.Delay(1000);
        return true;
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
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
    ];
}
