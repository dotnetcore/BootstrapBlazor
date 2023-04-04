// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Markdown 示例代码
/// </summary>
public partial class Markdowns
{
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem(){
            Name = "Height",
            Description = Localizer["Att1"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "300"
        },
        new AttributeItem(){
            Name = "MinHeight",
            Description = Localizer["Att2"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "200"
        },
        new AttributeItem(){
            Name = "InitialEditType",
            Description = Localizer["Att3"],
            Type = "InitialEditType",
            ValueList = "Markdown/Wysiwyg",
            DefaultValue = "Markdown"
        },
        new AttributeItem(){
            Name = "PreviewStyle",
            Description = Localizer["Att4"],
            Type = "PreviewStyle",
            ValueList = "Tab/Vertical",
            DefaultValue = "Vertical"
        },
        new AttributeItem(){
            Name = "Language",
            Description = Localizer["Att5"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Placeholder",
            Description = Localizer["Att6"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "IsViewer",
            Description = Localizer["Att7"],
            Type = "bool",
            ValueList = " true/false ",
            DefaultValue = " false "
        },
        new AttributeItem(){
            Name = "IsDark",
            Description = Localizer["Att8"],
            Type = "bool",
            ValueList = " true/false ",
            DefaultValue = " false "
        },
        new AttributeItem(){
            Name = "EnableHighlight",
            Description = Localizer["Att9"],
            Type = "bool",
            ValueList = " true/false ",
            DefaultValue = " false "
        }
    };
}
