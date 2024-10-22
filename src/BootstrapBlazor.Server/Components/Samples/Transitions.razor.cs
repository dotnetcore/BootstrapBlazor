// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Transitions
/// </summary>
public partial class Transitions
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private bool TransitionEndShow { get; set; }

    private bool Show { get; set; }

    private bool FadeInShow { get; set; }

    private void OnShow()
    {
        Show = true;
    }

    private Task OnShowEnd()
    {
        Show = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void OnTransitionShow()
    {
        TransitionEndShow = true;
    }

    private Task OnTransitionEndShow()
    {
        TransitionEndShow = false;
        Logger.Log("animation ends");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private void OnFadeInShow()
    {
        FadeInShow = true;
    }

    private Task OnFadeInEndShow()
    {
        FadeInShow = false;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static AttributeItem[] GetAttributes() =>
    [
        new() {
            Name = "TransitionType",
            Description = "Animation effect name",
            Type = "TransitionType",
            ValueList = " — ",
            DefaultValue = "FadeIn"
        },
        new() {
            Name = "Show",
            Description = "Control animation execution",
            Type = "Boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = "Duration",
            Description = "Control animation duration",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new() {
            Name = "OnTransitionEnd",
            Description = "Animation execution complete callback",
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
