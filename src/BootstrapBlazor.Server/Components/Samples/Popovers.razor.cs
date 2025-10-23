// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Popovers
/// </summary>
public sealed partial class Popovers
{
    private static readonly string ValueString = "Pop-up box";

    private static readonly string Title = "popup title";

    private static readonly string Content = "Here is the text of the pop-up box, the <code>html</code> tag is supported here, or a <code>Table</code> can be built-in";

    private string? _templateTitle;

    private Popover? _popover;

    private async Task ToggleShow()
    {
        if (_popover != null)
        {
            await _popover.Toggle();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _templateTitle = Localizer["PopoversTemplateTitleText"];
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Content",
            Description = "Popover content",
            Type = "string",
            ValueList = "",
            DefaultValue = "Popover"
        },
        new()
        {
            Name = "IsHtml",
            Description = "Whether the content contains Html code",
            Type = "boolean",
            ValueList = "",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Placement",
            Description = "Location",
            Type = "Placement",
            ValueList = "Auto / Top / Left / Bottom / Right",
            DefaultValue = "Auto"
        },
        new()
        {
            Name = "Title",
            Description = "Popover title",
            Type = "string",
            ValueList = "",
            DefaultValue = "Popover"
        }
    ];
}
