// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// FocusGuide 组件步骤组件
/// </summary>
public class DriverJsStep : ComponentBase, IDisposable
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
    private DriverJs? Driver { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<DriverJs>? Localizer { get; set; }

    [JsonInclude]
    [JsonPropertyName("popover")]
    private IDriverJsPopover? _popover;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Driver?.AddStep(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        _popover ??= new DriverJsHighlightPopover()
        {
            Title = Title,
            Description = Description,
            PrevBtnText = Localizer[nameof(DriverJsHighlightPopover.PrevBtnText)],
            NextBtnText = Localizer[nameof(DriverJsHighlightPopover.NextBtnText)],
            DoneBtnText = Localizer[nameof(DriverJsHighlightPopover.DoneBtnText)]
        };
        builder.OpenComponent<CascadingValue<DriverJsStep>>(0);
        builder.AddAttribute(1, nameof(CascadingValue<DriverJsStep>.Value), this);
        builder.AddAttribute(2, nameof(CascadingValue<DriverJsStep>.IsFixed), true);
        builder.AddAttribute(3, nameof(CascadingValue<DriverJsStep>.ChildContent), ChildContent);
        builder.CloseComponent();
    }

    /// <summary>
    /// 更新 FocusGuidePopover 实例方法
    /// </summary>
    /// <param name="popover"></param>
    public void UpdatePopover(IDriverJsPopover? popover)
    {
        _popover = popover;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            Driver?.RemoveStep(this);
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
}
