// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// LinkButtons
/// </summary>
public sealed partial class LinkButtons
{
    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem(){
            Name = nameof(LinkButton.Text),
            Description = Localizer[nameof(LinkButton.Text)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = nameof(LinkButton.Url),
            Description = Localizer[nameof(LinkButton.Url)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#"
        },
        new AttributeItem(){
            Name = nameof(LinkButton.TooltipText),
            Description = Localizer[nameof(LinkButton.TooltipText)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = nameof(LinkButton.ImageUrl),
            Description = Localizer[nameof(LinkButton.ImageUrl)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = nameof(LinkButton.Icon),
            Description = Localizer[nameof(LinkButton.Icon)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = nameof(LinkButton.TooltipPlacement),
            Description = Localizer[nameof(LinkButton.TooltipPlacement)],
            Type = "Placement",
            ValueList = "Top/Left/Right/Bottom",
            DefaultValue = "Top"
        },
        new AttributeItem(){
            Name = nameof(LinkButton.ChildContent),
            Description = Localizer[nameof(LinkButton.ChildContent)],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(LinkButton.OnClick),
            Description = Localizer[nameof(LinkButton.OnClick)],
            Type = "EventCallback<MouseEventArgs>",
            ValueList = "—",
            DefaultValue = " — "
        }
    };
}
