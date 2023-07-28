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
        new() {
            Name = "Placeholder",
            Description = Localizer["Att1"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att1DefaultValue"]!
        },
        new() {
            Name = "IsEditor",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowSubmit",
            Description = Localizer["AttShowSubmit"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = "Height",
            Description = Localizer["Att3"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ToolbarItems",
            Description = Localizer["Att4"],
            Type = "IEnumerable<object>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "CustomerToolbarButtons",
            Description = Localizer["Att5"],
            Type = "IEnumerable<EditorToolbarButton>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
