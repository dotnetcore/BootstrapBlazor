// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IGuidePopover 接口定义
/// </summary>
public interface IDriverJsPopover
{
    /// <summary>
    /// Title shown in the popover.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Descriptions shown in the popover.
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// The position of the popover. "top" | "right" | "bottom" | "left"
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Side { get; set; }

    /// <summary>
    /// The alignment of the popover. "start" | "center" | "end"
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Align { get; set; }

    /// <summary>
    /// Array of buttons to show in the popover. "next" | "previous" | "close"
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    List<string>? ShowButtons { get; set; }

    /// <summary>
    /// An array of buttons to disable. "next" | "previous" | "close"
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    List<string>? DisableButtons { get; set; }

    /// <summary>
    /// Text to show in the next buttons.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? NextBtnText { get; set; }

    /// <summary>
    /// Text to show in the prev buttons.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? PrevBtnText { get; set; }

    /// <summary>
    /// Text to show in the done buttons.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? DoneBtnText { get; set; }

    /// <summary>
    /// Whether to show the progress text in popover. (default true)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    bool? ShowProgress { get; set; }

    /// <summary>
    /// Template for the progress text. Defaults "{{current}} of {{total}}"
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? ProgressText { get; set; }

    /// <summary>
    /// Custom class to add to the popover element.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? PopoverClass { get; set; }
}
