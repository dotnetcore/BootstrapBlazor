// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Modals
{
    [NotNull]
    private Modal? Modal { get; set; }

    [NotNull]
    private Modal? BackdropModal { get; set; }

    [NotNull]
    private Modal? SmailModal { get; set; }

    [NotNull]
    private Modal? LargeModal { get; set; }

    [NotNull]
    private Modal? ExtraLargeModal { get; set; }

    [NotNull]
    private Modal? ExtraExtraLargeModal { get; set; }

    [NotNull]
    private Modal? SmailFullScreenModal { get; set; }

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

    private bool IsKeyboard { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private Task OnShownCallbackAsync()
    {
        Trace.Log("弹窗已显示");
        return Task.CompletedTask;
    }

    private void OnClickKeyboard()
    {
        IsKeyboard = !IsKeyboard;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "HeaderTemplate",
            Description = "Modal body ModalHeader template",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = "Modal body ModalBody component",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ChildContent",
            Description = "Content",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = "ModalFooter component at the bottom of the modal",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsBackdrop",
            Description = "Whether to close the popup in the background",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsKeyboard",
            Description = "Whether to respond to ESC keyboard",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsCentered",
            Description = "Whether to center vertically",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsScrolling",
            Description = "Whether to scroll when the text of the pop-up window is too long",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsFade",
            Description = "Whether to enable the fade-in and fade-out animation effect",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsDraggable",
            Description = "Whether to enable the draggable effect",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCloseButton",
            Description = "whether to show the close button",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = "Whether to show Footer",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Size",
            Description = "Size",
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge / ExtraExtraLarge",
            DefaultValue = "ExtraExtraLarge"
        },
        new AttributeItem() {
            Name = nameof(ModalDialog.FullScreenSize),
            Description = "Full screen when smaller than a certain size",
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge / ExtraExtraLarge",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "Popup title",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " not set "
        },
        new AttributeItem() {
            Name = nameof(ModalDialog.ShowMaximizeButton),
            Description = "Whether to show the popup maximize button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShownCallbackAsync",
            Description = "弹窗已显示回调方法",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
