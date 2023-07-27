// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// HandwrittenPage
/// </summary>
public sealed partial class HandwrittenPage
{
    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? DrawBase64 { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem()
        {
            Name = "SaveButtonText",
            Description = Localizer["SaveButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SaveButtonTextDefaultValue"]
        },
        new AttributeItem()
        {
            Name = "ClearButtonText",
            Description = Localizer["ClearButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["ClearButtonTextDefaultValue"]
        },
        new AttributeItem()
        {
            Name = "Result",
            Description = Localizer["Result"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "HandwrittenBase64",
            Description = Localizer["HandwrittenBase64"],
            Type = "EventCallback<string>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
