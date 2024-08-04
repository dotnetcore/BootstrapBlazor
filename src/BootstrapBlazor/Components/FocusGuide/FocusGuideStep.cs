﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide 组件步骤组件
/// </summary>
public class FocusGuideStep : ComponentBase, IDisposable
{
    /// <summary>
    /// 获得/设置 当前步骤目标元素选择器 默认 null 必须设置
    /// </summary>
    [Parameter]
    [JsonPropertyName("element")]
    public string? Selector { get; set; }

    /// <summary>
    /// Title shown in the popover.
    /// </summary>
    [Parameter]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Descriptions shown in the popover.
    /// </summary>
    [Parameter]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    [JsonIgnore]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    [JsonIgnore]
    private FocusGuide? Guide { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<FocusGuide>? Localizer { get; set; }

    [JsonInclude]
    [JsonPropertyName("popover")]
    private IFocusGuidePopover? _popover;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Guide?.AddStep(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        _popover ??= new InternalFocusPopover()
        {
            Title = Title,
            Description = Description,
            PrevBtnText = Localizer[nameof(InternalFocusPopover.PrevBtnText)],
            NextBtnText = Localizer[nameof(InternalFocusPopover.NextBtnText)],
            DoneBtnText = Localizer[nameof(InternalFocusPopover.DoneBtnText)]
        };
        builder.OpenComponent<CascadingValue<FocusGuideStep>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<FocusGuideStep>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<FocusGuideStep>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<FocusGuideStep>.ChildContent), ChildContent);
        builder.CloseComponent();
    }

    /// <summary>
    /// 更新 FocusGuidePopover 实例方法
    /// </summary>
    /// <param name="popover"></param>
    public void UpdatePopover(IFocusGuidePopover? popover)
    {
        _popover = popover;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            Guide?.RemoveStep(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ///// <summary>
    ///// The popover configuration for this step.
    ///// </summary>
    //public FocusGuidePopover? Popover { get; set; }

    ///// <summary>
    ///// Callback when the current step is deselected
    ///// </summary>
    ///// <param name="step"></param>
    ///// <param name="config"></param>
    ///// <param name="state"></param>
    ///// <returns></returns>
    //public Task OnDeselected(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    //{
    //    return Task.CompletedTask;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="step"></param>
    ///// <param name="config"></param>
    ///// <param name="state"></param>
    ///// <returns></returns>
    //public Task OnHighlightStarted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    //{
    //    return Task.CompletedTask;
    //}

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="step"></param>
    ///// <param name="config"></param>
    ///// <param name="state"></param>
    ///// <returns></returns>
    //public Task OnHighlighted(FocusGuideStep step, FocusGuideConfig config, FocusGuideState state)
    //{
    //    return Task.CompletedTask;
    //}
}