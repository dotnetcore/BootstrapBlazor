// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Handwritten 组件示例代码
/// </summary>
public sealed partial class Handwrittens
{
    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? DrawBase64 { get; set; }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "SaveButtonText",
            Description = Localizer["SaveButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SaveButtonTextDefaultValue"]
        },
        new()
        {
            Name = "ClearButtonText",
            Description = Localizer["ClearButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["ClearButtonTextDefaultValue"]
        },
        new()
        {
            Name = "Result",
            Description = Localizer["Result"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HandwrittenBase64",
            Description = Localizer["HandwrittenBase64"],
            Type = "EventCallback<string>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
