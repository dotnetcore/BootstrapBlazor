// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 弹窗组件示例代码
/// </summary>
public sealed partial class Dialogs
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Component",
            Description = "Parameters of the component referenced in the dialog Body",
            Type = "DynamicComponent",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyContext",
            Description = "pop-up window",
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
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
            Name = "FooterTemplate",
            Description = "ModalFooter component at the bottom of the modal",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsCentered",
            Description = "Whether to center vertically",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsScrolling",
            Description = "Whether to scroll when the text of the pop-up window is too long",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCloseButton",
            Description = "whether to show the close button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowHeaderCloseButton",
            Description = "Whether to display the close button on the right side of the title bar",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = "whether to display Footer",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowPrintButton),
            Description = "Whether to show the print button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowPrintButtonInHeader),
            Description = "Whether the print button is displayed in the Header",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Size",
            Description = "Size of dialog",
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "Large"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.FullScreenSize),
            Description = "Full screen when smaller than a certain size",
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge",
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
            Name = nameof(DialogOption.PrintButtonText),
            Description = "print button display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "The value set in the resource file"
        },
        new AttributeItem() {
            Name = nameof(DialogOption.ShowMaximizeButton),
            Description = "Whether to show the maximize button",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
