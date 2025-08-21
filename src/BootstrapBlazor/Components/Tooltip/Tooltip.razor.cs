// Licensed to the .NET Foundation under one or more agreements.
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

    private string? ClassString => CssBuilder.Default("bb-tooltip")
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
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 获得显示内容异步回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task<string>>? GetTitleCallback { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool IsHtml { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
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
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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

    /// <summary>
    /// 显示 Tooltip 弹窗方法
    /// </summary>
    /// <param name="delay">延时指定毫秒后显示弹窗 默认 null 不延时</param>
    /// <returns></returns>
    public Task Show(int? delay = null) => InvokeVoidAsync("show", Id, delay);

    /// <summary>
    /// 关闭 Tooltip 弹窗方法
    /// </summary>
    /// <param name="delay">延时指定毫秒后关闭弹窗 默认 null 不延时</param>
    /// <returns></returns>
    public Task Hide(int? delay = null) => InvokeVoidAsync("hide", Id, delay);

    /// <summary>
    /// 切换 Tooltip 弹窗当前状态方法
    /// </summary>
    /// <param name="delay">延时指定毫秒后切换弹窗方法 默认 null 不延时</param>
    /// <returns></returns>
    public Task Toggle(int? delay = null) => InvokeVoidAsync("toggle", Id, delay);
}
