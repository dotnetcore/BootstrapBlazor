// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Empties
/// </summary>
public partial class Empties
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
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
    ];
}
