// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Toasts
/// </summary>
public sealed partial class Toasts
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Category",
            Description = Localizer["ToastsAttrCategory"],
            Type = "ToastCategory",
            ValueList = "Success/Information/Error/Warning",
            DefaultValue = "Success"
        },
        new AttributeItem() {
            Name = "Title",
            Description = Localizer["ToastsAttrTitle"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Cotent",
            Description = Localizer["ToastsAttrCotent"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Delay",
            Description = Localizer["ToastsAttrDelay"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "4000"
        },
        new AttributeItem() {
            Name = "IsAutoHide",
            Description = Localizer["ToastsAttrIsAutoHide"],
            Type = "boolean",
            ValueList = "",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsHtml",
            Description = Localizer["ToastsAttrIsHtml"],
            Type = "boolean",
            ValueList = "",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Placement",
            Description = Localizer["ToastsAttrPlacement"],
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        }
    };
}
