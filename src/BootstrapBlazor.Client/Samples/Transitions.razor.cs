// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
    };
}
