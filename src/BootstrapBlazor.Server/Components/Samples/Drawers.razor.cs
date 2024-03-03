// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Drawers
/// </summary>
public sealed partial class Drawers
{
    private bool IsOpen { get; set; }

    private Placement DrawerAlign { get; set; }

    private SelectedItem[] DrawerDirection { get; } =
    [
        new("left", "left to right")
        {
            Active = true
        },
        new("right", "right to left"),
        new("top", "top to bottom"),
        new("bottom", "bottom to top")
    ];

    private Task OnStateChanged(IEnumerable<SelectedItem> values, SelectedItem val)
    {
        DrawerAlign = val.Value switch
        {
            "right" => Placement.Right,
            "top" => Placement.Top,
            "bottom" => Placement.Bottom,
            _ => Placement.Left
        };
        IsOpen = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private bool IsBackdropOpen { get; set; }

    private void OpenDrawer()
    {
        IsBackdropOpen = true;
    }

    private bool IsShowBackdropOpen { get; set; }

    private void OpenNoBackdropDrawer() => IsShowBackdropOpen = true;

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Width",
            Description = "drawer width",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "360px"
        },
        new()
        {
            Name = "Height",
            Description = "drawer height",
            Type = "string",
            ValueList = " — ",
            DefaultValue = "290px"
        },
        new()
        {
            Name = "IsOpen",
            Description = "Is the drawer open?",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsBackdrop",
            Description = "Whether click on the mask closes the drawer",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "AllowResize",
            Description = "Whether allow drag resize the drawer",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnClickBackdrop",
            Description = "Callback delegate method when background mask is clicked",
            Type = "Action",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Placement",
            Description = "Where the component appears",
            Type = "Placement",
            ValueList = "Left|Right|Top|Bottom",
            DefaultValue = "Left"
        },
        new()
        {
            Name = "ChildContent",
            Description = "Subassembly",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
