﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Tooltip 组件
/// </summary>
public partial class Tooltip : ITooltip
{
    /// <summary>
    /// 弹窗位置字符串
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// 获得 是否关键字过滤字符串
    /// </summary>
    protected string? SanitizeString => Sanitize ? null : "false";

    /// <summary>
    /// 获得 是否 Html 字符串
    /// </summary>
    protected string? HtmlString => IsHtml ? "true" : null;

    /// <summary>
    /// component class
    /// </summary>
    protected string? ClassString => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// fallbackPlacements 参数
    /// </summary>
    protected string? FallbackPlacementsString => FallbackPlacements != null ? string.Join(",", FallbackPlacements) : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Delay { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Selector { get; set; }

    /// <summary>
    /// 获得/设置 显示内容
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 获得显示内容异步回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task<string>>? GetTitleCallback { get; set; }

    /// <summary>
    /// 获得/设置 显示文字是否为 Html 默认为 false
    /// </summary>
    [Parameter]
    public bool IsHtml { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// 获得/设置 位置 默认为 Placement.Top
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Top;

    /// <summary>
    /// 获得/设置 位置 默认为 null
    /// </summary>
    [Parameter]
    public string[]? FallbackPlacements { get; set; }

    /// <summary>
    /// 获得/设置 偏移量 默认为 null
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得 CustomClass 字符串
    /// </summary>
    protected virtual string? CustomClassString => CustomClass;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Trigger ??= "focus hover";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (string.IsNullOrEmpty(Title) && GetTitleCallback != null)
        {
            Title ??= await GetTitleCallback();
        }
    }

    /// <summary>
    /// 设置参数方法
    /// </summary>
    public void SetParameters(string title, Placement placement = Placement.Auto, string? trigger = null, string? customClass = null, bool? isHtml = null, bool? sanitize = null, string? delay = null, string? selector = null, string? offset = null)
    {
        Title = title;
        if (placement != Placement.Auto) Placement = placement;
        if (!string.IsNullOrEmpty(trigger)) Trigger = trigger;
        if (!string.IsNullOrEmpty(customClass)) CustomClass = customClass;
        if (isHtml.HasValue) IsHtml = isHtml.Value;
        if (sanitize.HasValue) Sanitize = sanitize.Value;
        if (!string.IsNullOrEmpty(delay)) Delay = delay;
        if (!string.IsNullOrEmpty(selector)) Selector = selector;
        if (!string.IsNullOrEmpty(selector)) Selector = selector;
        if (!string.IsNullOrEmpty(offset)) Offset = offset;
        StateHasChanged();
    }
}
