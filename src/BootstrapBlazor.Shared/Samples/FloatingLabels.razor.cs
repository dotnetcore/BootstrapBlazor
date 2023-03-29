// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// FloatingLabels
/// </summary>
public partial class FloatingLabels
{
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["FloatingLabelsChildContent"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["FloatingLabelsShowLabel"].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["FloatingLabelsDisplayText"].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FormatString",
            Description = Localizer["FloatingLabelsFormatString"].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Formatter",
            Description = Localizer["FloatingLabelsFormatter"].Value,
            Type = "RenderFragment<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "type",
            Description = Localizer["FloatingLabelsType"].Value,
            Type = "string",
            ValueList = "text / number / email / url / password",
            DefaultValue = "text"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["FloatingLabelsIsDisabled"].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
