// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class AnchorLinks
{
    [Inject]
    [NotNull]
    private IStringLocalizer<AnchorLink>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Id",
            Description = "Component Id",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AnchorLink.Icon),
            Description = "Component Icon",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-link"
        },
        new AttributeItem() {
            Name = "Text",
            Description = "Component display text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "TooltipText",
            Description = "Tooltip Text",
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer[nameof(AnchorLink.TooltipText)]
        }
    };
}
