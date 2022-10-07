// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class Transitions
{
    private bool Show { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

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

    private bool TransitionEndShow { get; set; }

    private void OnTransitionShow()
    {
        TransitionEndShow = true;
    }

    private Task OnTransitionEndShow()
    {
        TransitionEndShow = false;
        Trace.Log("animation ends");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private bool FadeInShow { get; set; }

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

    private static IEnumerable<AttributeItem> GetAttributes() => new[]
    {
            new AttributeItem() {
                Name = "TransitionType",
                Description = "Animation effect name",
                Type = "TransitionType",
                ValueList = " — ",
                DefaultValue = "FadeIn"
            },
            new AttributeItem() {
                Name = "Show",
                Description = "Control animation execution",
                Type = "Boolean",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem() {
                Name = "Duration",
                Description = "Control animation duration",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "OnTransitionEnd",
                Description = "Animation execution complete callback",
                Type = "Func<Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
}
