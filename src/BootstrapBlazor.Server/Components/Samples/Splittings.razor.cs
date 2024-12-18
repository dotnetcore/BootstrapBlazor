// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Loader 示例代码
/// </summary>
public partial class Splittings
{
    private int _columns = 30;

    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Text",
            Description = "display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "Loading ..."
        },
        new()
        {
            Name = "Columns",
            Description = "Progress bar segmentation granularity",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "10"
        },
        new()
        {
            Name = "Color",
            Description = "the color of progress",
            Type = "Enum",
            ValueList = " — ",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "ShowLoadingText",
            Description = "Whether show the text of loading",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Repeat",
            Description = "Is repeat the animation",
            Type = "int",
            ValueList = ">= -1",
            DefaultValue = "-1"
        }
    ];
}
