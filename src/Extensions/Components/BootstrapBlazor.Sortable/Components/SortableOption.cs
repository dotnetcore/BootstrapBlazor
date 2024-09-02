// Copyright (c) Argo Zhang (argo@live.ca). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// SortableOption 配置类
/// </summary>
public class SortableOption
{
    /// <summary>
    /// 获得/设置 目标元素选择器
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RootSelector { get; set; }

    /// <summary>
    /// or { name: "...", pull: [true, false, 'clone', array], put: [true, false, array] }
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Group { get; set; }

    /// <summary>
    /// 获得/设置 拖动时是否克隆元素 默认 null 未设置 不克隆
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Clone { get; set; }

    /// <summary>
    /// 获得/设置 是否允许拖动回来 默认 null 未设置 允许
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Putback { get; set; }

    /// <summary>
    /// sorting inside list
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Sort { get; set; }

    /// <summary>
    /// time in milliseconds to define when the sorting should start
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Delay { get; set; }

    /// <summary>
    /// only delay if user is using touch
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DelayOnTouchOnly { get; set; }

    /// <summary>
    /// // px, how many pixels the point should move before cancelling a delayed drag event
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? TouchStartThreshold { get; set; }

    /// <summary>
    /// Disables the sortable if set to true.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Disabled { get; set; }

    /// <summary>
    /// ms, animation speed moving items when sorting, `0` — without animation
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Animation { get; set; }

    /// <summary>
    /// Easing for animation. Defaults to null. See https://easings.net/ for examples.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Easing { get; set; }

    /// <summary>
    /// Drag handle selector within list items
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Handle { get; set; }

    /// <summary>
    /// Selectors that do not lead to dragging (String or Function)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Filter { get; set; }

    /// <summary>
    /// Call `event.preventDefault()` when triggered `filter`
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? PreventOnFilter { get; set; }

    /// <summary>
    /// Specifies which items inside the element should be draggable
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Draggable { get; set; }

    /// <summary>
    /// HTML attribute that is used by the `toArray()` method
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DataIdAttr { get; set; }

    /// <summary>
    /// Class name for the drop placeholder
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? GhostClass { get; set; }

    /// <summary>
    /// Class name for the chosen item
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChosenClass { get; set; }

    /// <summary>
    /// Class name for the dragging item
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DragClass { get; set; }

    /// <summary>
    /// Threshold of the swap zone
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? SwapThreshold { get; set; }

    /// <summary>
    /// Will always use inverted swap zone if set to true
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? InvertSwap { get; set; }

    /// <summary>
    /// Threshold of the inverted swap zone (will be set to swapThreshold value by default)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? InvertedSwapThreshold { get; set; }

    /// <summary>
    /// Direction of Sortable (will be detected automatically if not given)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Direction { get; set; }

    /// <summary>
    /// ignore the HTML5 DnD behaviour and force the fallback to kick in
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? ForceFallback { get; set; }

    /// <summary>
    /// Class name for the cloned DOM Element when using forceFallback
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? FallbackClass { get; set; }

    /// <summary>
    /// Appends the cloned DOM Element into the Document's Body
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? FallbackOnBody { get; set; }

    /// <summary>
    /// Specify in pixels how far the mouse should move before it's considered as a drag.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? FallbackTolerance { get; set; }

    /// <summary>
    /// DragoverBubble
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? DragoverBubble { get; set; }

    /// <summary>
    /// Remove the clone element when it is not showing, rather than just hiding it
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? RemoveCloneOnHide { get; set; }

    /// <summary>
    /// px, distance mouse must be from empty sortable to insert drag element into it
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? EmptyInsertThreshold { get; set; }

    /// <summary>
    /// 获得/设置 是否允许多拖动 默认 null 未设置
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? MultiDrag { get; set; }

    /// <summary>
    /// 获得/设置 是否交换拖动 默认 null 未设置
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Swap { get; set; }

    /// <summary>
    /// 获得/设置 是否交换拖动项样式名称 默认 null 未设置
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? SwapClass { get; set; }
}
