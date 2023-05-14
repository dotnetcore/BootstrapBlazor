// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Editors
/// </summary>
public sealed partial class Editors
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Placeholder",
            Description = Localizer["Att1"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att1DefaultValue"]!
        },
        new AttributeItem() {
            Name = "IsEditor",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowSubmit",
            Description = Localizer["AttShowSubmit"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Height",
            Description = Localizer["Att3"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "ToolbarItems",
            Description = Localizer["Att4"],
            Type = "IEnumerable<object>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "CustomerToolbarButtons",
            Description = Localizer["Att5"],
            Type = "IEnumerable<EditorToolbarButton>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
