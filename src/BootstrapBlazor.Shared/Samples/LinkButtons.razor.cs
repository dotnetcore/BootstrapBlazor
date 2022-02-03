// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class LinkButtons
{
    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<LinkButtons>? Localizer { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private void OnClick()
    {
        Trace.Log($"{DateTimeOffset.Now}: Clicked!");
    }

    /// <summary>
    /// 
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
                Name = nameof(LinkButton.Title),
                Description = Localizer[nameof(LinkButton.Title)],
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
