// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide Popover 配置类
/// </summary>
public class FocusGuidePopover
{
    /// <summary>
    /// Title shown in the popover.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Descriptions shown in the popover.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The position of the popover. "top" | "right" | "bottom" | "left"
    /// </summary>
    public string? Side { get; set; }

    /// <summary>
    /// The alignment of the popover. "start" | "center" | "end"
    /// </summary>
    public string? Align { get; set; }

    /// <summary>
    /// Array of buttons to show in the popover. "next" | "previous" | "close"
    /// </summary>
    public List<string>? ShowButtons { get; set; }

    /// <summary>
    /// An array of buttons to disable. "next" | "previous" | "close"
    /// </summary>
    public List<string>? DisableButtons { get; set; }

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
    /// Whether to show the progress text in popover.
    /// </summary>
    public bool ShowProgress { get; set; }

    /// <summary>
    /// Template for the progress text. Defaults "{{current}} of {{total}}"
    /// </summary>
    public string? ProgressText { get; set; }

    /// <summary>
    /// Custom class to add to the popover element.
    /// </summary>
    public string? PopoverClass { get; set; }

    /// <summary>
    /// Hook to run after the popover is rendered
    /// </summary>
    /// <param name="step"></param>
    /// <param name="config"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    public Task OnPopoverRender(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
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
