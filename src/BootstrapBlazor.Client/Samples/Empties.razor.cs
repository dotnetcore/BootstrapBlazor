// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Empties
/// </summary>
public partial class Empties
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Image",
            Description = Localizer["Image"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Text",
            Description =  Localizer["Text"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["TextDefaultValue"]
        },
        new()
        {
            Name = "Width",
            Description =  Localizer["Width"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " 100 "
        },
        new()
        {
            Name = "Height",
            Description =  Localizer["Height"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " 100 "
        },
        new()
        {
            Name = "Template",
            Description =  Localizer["Template"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
            Description =  Localizer["ChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
