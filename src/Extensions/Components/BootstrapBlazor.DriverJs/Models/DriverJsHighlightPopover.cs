// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DriverJsHighlightPopover 实例
/// </summary>
public class DriverJsHighlightPopover : IDriverJsPopover
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Side { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Align { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? ShowButtons { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<string>? DisableButtons { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? NextBtnText { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PrevBtnText { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DoneBtnText { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ShowProgress { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ProgressText { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PopoverClass { get; set; }
}
