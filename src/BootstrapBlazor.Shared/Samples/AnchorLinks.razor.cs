// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class AnchorLinks
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AnchorLinks>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new() {
            Name = nameof(AnchorLink.Id),
            Description = Localizer[$"Attr{nameof(AnchorLink.Id)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(AnchorLink.Icon),
            Description = Localizer[$"Attr{nameof(AnchorLink.Icon)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-link"
        },
        new() {
            Name = nameof(AnchorLink.Text),
            Description = Localizer[$"Attr{nameof(AnchorLink.Text)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(AnchorLink.TooltipText),
            Description = Localizer[$"Attr{nameof(AnchorLink.TooltipText)}"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
