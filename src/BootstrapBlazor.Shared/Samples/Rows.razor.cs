// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Rows
/// </summary>
public sealed partial class Rows
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ItemsPerRow",
            Description = Localizer["RowsItemsPerRow"],
            Type = "enum",
            ValueList = " One,Two,Three,Four,Six,Twelve ",
            DefaultValue = " One "
        },
        new AttributeItem() {
            Name = "RowType",
            Description = Localizer["RowsRowType"],
            Type = "enum?",
            ValueList = "Normal, Inline",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "ColSpan",
            Description = Localizer["RowsColSpan"],
            Type = "int?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "MaxCount",
            Description = Localizer["RowsMaxCount"],
            Type = "int?",
            ValueList = "-",
            DefaultValue = "null"
        }
    };
}
