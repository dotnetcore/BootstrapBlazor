// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Drawers
/// </summary>
public sealed partial class Drawers
{
    [Inject, NotNull]
    private DrawerService? DrawerService { get; set; }

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

    private bool IsKeyboardOpen { get; set; }

    private void OpenKeyboardDrawer() => IsKeyboardOpen = true;

    private bool IsBodyScrollOpen { get; set; }

    private void OpenBodyScrollDrawer() => IsBodyScrollOpen = true;

    private async Task DrawerServiceShow() => await DrawerService.Show(new DrawerOption()
    {
        Placement = Placement.Right,
        ChildContent = builder => builder.AddContent(0, "Test"),
        ShowBackdrop = true,
        AllowResize = true,
        IsBackdrop = true
    });

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
            Name = "Position",
            Description = "Where the component position",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BodyScroll",
            Description = "Where the enable body scrolling when drawer is shown",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ChildContent",
            Description = "Subassembly",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ZIndex",
            Description = "sets the z-order",
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Drawer.OnCloseAsync),
            Description = "The callback when close drawer",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
