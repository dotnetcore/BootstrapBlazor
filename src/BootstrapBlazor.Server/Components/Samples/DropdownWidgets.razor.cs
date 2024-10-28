// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class DropdownWidgets
{
    private ConsoleLogger _logger = default!;

    private Task OnItemCloseAsync(DropdownWidgetItem item)
    {
        _logger.Log($"Item {item.BadgeNumber} closed");
        return Task.CompletedTask;
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Icon",
            Description = Localizer["Icon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-bell"
        },
        new()
        {
            Name = "BadgeColor",
            Description = Localizer["BadgeColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Success"
        },
        new()
        {
            Name = "HeaderColor",
            Description = Localizer["HeaderColor"],
            Type = "Color",
            ValueList = "Primary / Secondary / Info / Warning / Danger ",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "BadgeNumber",
            Description = Localizer["BadgeNumber"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowArrow",
            Description = Localizer["ShowArrow"],
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "MenuAlignment",
            Description = Localizer["MenuAlignment"],
            Type = "Alignment",
            ValueList = "None / Left / Center / Right ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["HeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyTemplate",
            Description = Localizer["BodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FooterTemplate",
            Description = Localizer["FooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnItemCloseAsync",
            Description = Localizer["OnItemCloseAsync"],
            Type = "Func<DropdownWidgetItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
