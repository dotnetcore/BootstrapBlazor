// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class AnchorLinks
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AnchorLinks>? Localizer { get; set; }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(AnchorLink.Id),
            Description = Localizer[$"Attr{nameof(AnchorLink.Id)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(AnchorLink.Icon),
            Description = Localizer[$"Attr{nameof(AnchorLink.Icon)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-link"
        },
        new()
        {
            Name = nameof(AnchorLink.Text),
            Description = Localizer[$"Attr{nameof(AnchorLink.Text)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(AnchorLink.TooltipText),
            Description = Localizer[$"Attr{nameof(AnchorLink.TooltipText)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
