// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// GroupBoxes
/// </summary>
public sealed partial class GroupBoxes
{
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Title",
            Description = Localizer["AttTitle"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
