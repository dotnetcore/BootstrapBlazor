// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide 配置类
/// </summary>
public class DriverJsConfig
{
    /// <summary>
    /// Array of steps to highlight.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<DriverJsStep> Steps { get; set; } = [];

    /// <summary>
    /// Whether to animate the product tour. (default: true)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Animate { get; set; }

    /// <summary>
    /// Overlay color. (default: black)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OverlayColor { get; set; }

    /// <summary>
    /// Whether to smooth scroll to the highlighted element. (default: false)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SmoothScroll { get; set; }

    /// <summary>
    /// Whether to allow closing the popover by clicking on the backdrop. (default: true)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AllowClose { get; set; }

    /// <summary>
    /// Opacity of the backdrop. (default: 0.5)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? OverlayOpacity { get; set; }

    /// <summary>
    /// Distance between the highlighted element and the cutout. (default: 10)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? StagePadding { get; set; }

    /// <summary>
    /// Radius of the cutout around the highlighted element. (default: 5)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? StageRadius { get; set; }

    /// <summary>
    /// Whether to allow keyboard navigation. (default: true)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? AllowKeyboardControl { get; set; }

    /// <summary>
    /// Whether to disable interaction with the highlighted element. (default: false)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DisableActiveInteraction { get; set; }

    /// <summary>
    /// If you want to add custom class to the popover
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PopoverClass { get; set; }

    /// <summary>
    /// Distance between the popover and the highlighted element. (default: 10)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public float? PopoverOffset { get; set; }

    /// <summary>
    /// Array of buttons to show in the popover. Defaults to ["next", "previous", "close"]
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? ShowButtons { get; set; }

    /// <summary>
    /// Array of buttons to disable.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? DisableButtons { get; set; }

    /// <summary>
    /// Whether to show the progress text in popover. (default: false)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowProgress { get; set; }

    /// <summary>
    /// Template for the progress text. You can use the following placeholders in the template:
    /// - {{current}}: The current step number
    /// - {{total}}: Total number of steps
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProgressText { get; set; }

    /// <summary>
    /// Text to show in the next buttons.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NextBtnText { get; set; }

    /// <summary>
    /// Text to show in the prev buttons.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PrevBtnText { get; set; }

    /// <summary>
    /// Text to show in the done buttons.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DoneBtnText { get; set; }

    [JsonInclude]
    private bool HookDestroyStarted => OnDestroyStartedAsync != null;

    /// <summary>
    /// 获得/设置 组件销毁前回调方法名称
    /// </summary>
    [JsonIgnore]
    public Func<DriverJsConfig, int, Task<string?>>? OnDestroyStartedAsync { get; set; }

    [JsonInclude]
    private bool HookDestroyed => OnDestroyedAsync != null;

    /// <summary>
    /// 获得/设置 组件销毁前回调方法名称
    /// </summary>
    [JsonIgnore]
    public Func<Task>? OnDestroyedAsync { get; set; }
}
