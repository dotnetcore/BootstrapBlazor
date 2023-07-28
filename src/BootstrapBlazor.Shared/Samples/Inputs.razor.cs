// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Inputs
/// </summary>
public partial class Inputs
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new() {
            Name = "ChildContent",
            Description = Localizer["InputsAtt1"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "ShowLabel",
            Description = Localizer["InputsAtt2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "DisplayText",
            Description = Localizer["InputsAtt3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Color",
            Description = Localizer["InputsAtt4"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new() {
            Name = "FormatString",
            Description = Localizer["InputsAtt5"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Formatter",
            Description = Localizer["InputsAtt6"],
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "type",
            Description = Localizer["InputsAtt7"],
            Type = "string",
            ValueList = "text / number / email / url / password",
            DefaultValue = "text"
        },
        new() {
            Name = "OnEnterAsync",
            Description = Localizer["InputsAtt8"],
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnEscAsync",
            Description = Localizer["InputsAtt9"],
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["InputsAtt10"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsAutoFocus",
            Description = Localizer["InputsAtt11"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(BootstrapInput<string>.IsSelectAllTextOnFocus),
            Description = Localizer[nameof(BootstrapInput<string>.IsSelectAllTextOnFocus)].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(BootstrapInput<string>.IsTrim),
            Description = Localizer[nameof(BootstrapInput<string>.IsTrim)].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(BootstrapInput<string>.ValidateRules),
            Description = Localizer[nameof(BootstrapInput<string>.ValidateRules)].Value,
            Type = "List<IValidator>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
