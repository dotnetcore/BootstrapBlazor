﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide 配置类
/// </summary>
public class FocusGuideConfig
{
    /// <summary>
    /// Array of steps to highlight.
    /// </summary>
    public List<FocusGuideStep>? Steps { get; set; }

    /// <summary>
    /// Whether to animate the product tour
    /// </summary>
    public bool Animate { get; set; } = true;

    /// <summary>
    /// Overlay color. (default: black)
    /// </summary>
    public string? OverlayColor { get; set; }

    /// <summary>
    /// Whether to smooth scroll to the highlighted element. (default: false)
    /// </summary>
    public bool SmoothScroll { get; set; } = false;

    /// <summary>
    /// Whether to allow closing the popover by clicking on the backdrop. (default: true)
    /// </summary>
    public bool AllowClose { get; set; } = true;

    /// <summary>
    /// Opacity of the backdrop. (default: 0.5)
    /// </summary>
    public float OverlayOpacity { get; set; } = 0.5f;

    /// <summary>
    /// Distance between the highlighted element and the cutout. (default: 10)
    /// </summary>
    public float StagePadding { get; set; } = 10f;

    /// <summary>
    /// Radius of the cutout around the highlighted element. (default: 5)
    /// </summary>
    public int StageRadius { get; set; } = 5;

    /// <summary>
    /// Whether to allow keyboard navigation. (default: true)
    /// </summary>
    public bool AllowKeyboardControl { get; set; } = true;

    /// <summary>
    /// Whether to disable interaction with the highlighted element. (default: false)
    /// </summary>
    public bool DisableActiveInteraction { get; set; }

    /// <summary>
    /// If you want to add custom class to the popover
    /// </summary>
    public string? PopoverClass { get; set; }

    /// <summary>
    /// Distance between the popover and the highlighted element. (default: 10)
    /// </summary>
    public float PopoverOffset { get; set; } = 10f;

    /// <summary>
    /// Array of buttons to show in the popover. Defaults to ["next", "previous", "close"]
    /// </summary>
    public List<string>? ShowButtons { get; set; }

    /// <summary>
    /// Array of buttons to disable.
    /// </summary>
    public List<string>? DisableButtons { get; set; }

    /// <summary>
    /// Whether to show the progress text in popover. (default: false)
    /// </summary>
    public bool ShowProgress { get; set; }

    /// <summary>
    /// Template for the progress text. You can use the following placeholders in the template:
    /// - {{current}}: The current step number
    /// - {{total}}: Total number of steps
    /// </summary>
    public string? ProgressText { get; set; }

    /// <summary>
    /// Text to show in the next buttons.
    /// </summary>
    public string? NextBtnText { get; set; }

    /// <summary>
    /// Text to show in the prev buttons.
    /// </summary>
    public string? PrevBtnText { get; set; }

    /// <summary>
    /// Text to show in the done buttons.
    /// </summary>
    public string? DoneBtnText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnHighlightStarted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnHighlighted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Callback when the current step is deselected
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnDeselected(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnDestroyStarted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnDestroyed(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Callbacks for next button clicks
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnNextClick(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Callbacks for prev button clicks
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnPrevClick(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Callbacks for close button clicks
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnCloseClick(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    {
        return Task.CompletedTask;
    }
}
