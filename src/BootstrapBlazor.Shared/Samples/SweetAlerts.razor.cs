// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// SweetAlerts
/// </summary>
public partial class SweetAlerts
{
    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Category",
            Description = Localizer["AttrCategory"],
            Type = "SwalCategory",
            ValueList = "Success/Error/Information/Warning/Question",
            DefaultValue = "Success"
        },
        new AttributeItem() {
            Name = "Title",
            Description = Localizer["AttrTitle"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Cotent",
            Description = Localizer["AttrContent"],
            Type = "string",
            ValueList = "—",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "Delay",
            Description = Localizer["AttrDelay"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "4000"
        },
        new AttributeItem() {
            Name = "IsAutoHide",
            Description = Localizer["AttrAutoHide"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowClose",
            Description = Localizer["AttrShowClose"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = Localizer["AttrShowFooter"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsConfirm",
            Description = Localizer["AttrIsConfirm"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "BodyContext",
            Description = Localizer["AttrBodyContext"],
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = Localizer["AttrBodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = Localizer["AttrFooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ButtonTemplate",
            Description = Localizer["AttrButtonTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
