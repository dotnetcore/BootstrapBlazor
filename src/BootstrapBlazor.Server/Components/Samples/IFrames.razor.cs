// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// IFrame 示例文档
/// </summary>
public partial class IFrames
{
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Src",
            Description = Localizer["AttributeSrc"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Data",
            Description = Localizer["AttributeData"],
            Type = "object",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnPostDataAsync",
            Description = Localizer["AttributeOnPostDataAsync"],
            Type = "Func<object?, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
