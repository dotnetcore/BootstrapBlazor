// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// LinkButton 组件示例
/// </summary>
public sealed partial class LinkButtons
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private void OnClick()
    {
        Logger.Log($"{DateTimeOffset.Now}: Clicked!");
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(LinkButton.Text),
            Description = Localizer[nameof(LinkButton.Text)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(LinkButton.Url),
            Description = Localizer[nameof(LinkButton.Url)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#"
        },
        new()
        {
            Name = nameof(LinkButton.TooltipText),
            Description = Localizer[nameof(LinkButton.TooltipText)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(LinkButton.ImageUrl),
            Description = Localizer[nameof(LinkButton.ImageUrl)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(LinkButton.Icon),
            Description = Localizer[nameof(LinkButton.Icon)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(LinkButton.TooltipPlacement),
            Description = Localizer[nameof(LinkButton.TooltipPlacement)],
            Type = "Placement",
            ValueList = "Top/Left/Right/Bottom",
            DefaultValue = "Top"
        },
        new()
        {
            Name = nameof(LinkButton.ChildContent),
            Description = Localizer[nameof(LinkButton.ChildContent)],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(LinkButton.OnClick),
            Description = Localizer[nameof(LinkButton.OnClick)],
            Type = "EventCallback<MouseEventArgs>",
            ValueList = "—",
            DefaultValue = " — "
        }
    ];
}
