﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
