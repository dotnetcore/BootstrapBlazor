// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Tooltip 组件</para>
/// <para lang="en">Tooltip Component</para>
/// </summary>
public partial class Tooltip : ITooltip
{
    /// <summary>
    /// <para lang="zh">弹窗位置字符串</para>
    /// <para lang="en">Popup Position String</para>
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    /// <para lang="zh">获得 是否进行关键字过滤字符串</para>
    /// <para lang="en">Gets the keyword filter string</para>
    /// </summary>
    protected string? SanitizeString => Sanitize ? null : "false";

    /// <summary>
    /// <para lang="zh">获得 是否为 Html 字符串</para>
    /// <para lang="en">Gets the HTML string flag</para>
    /// </summary>
    protected string? HtmlString => IsHtml ? "true" : null;

    private string? ClassString => CssBuilder.Default("bb-tooltip")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">fallbackPlacements 参数</para>
    /// <para lang="en">fallbackPlacements Parameter</para>
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
    /// <para lang="zh">获得/设置 获取显示内容的异步回调方法，默认 null</para>
    /// <para lang="en">Gets or sets the callback method to get display content asynchronously. Default is null.</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh">获得/设置 位置，默认为 null</para>
    /// <para lang="en">Gets or sets the placement. Default is null.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string[]? FallbackPlacements { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 偏移量，默认为 null</para>
    /// <para lang="en">Gets or sets the offset. Default is null.</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh">获得/设置 子组件</para>
    /// <para lang="en">Gets or sets the child component</para>
    /// <para><version>10.2.2</version></para>
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
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (string.IsNullOrEmpty(Title) && GetTitleCallback != null)
        {
            Title ??= await GetTitleCallback();
        }
    }

    /// <summary>
    /// <para lang="zh">设置参数方法</para>
    /// <para lang="en">Sets the parameters</para>
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
    /// <para lang="zh">显示 Tooltip 弹窗方法</para>
    /// <para lang="en">Shows the Tooltip popup</para>
    /// </summary>
    /// <param name="delay"><para lang="zh">延时指定毫秒后显示弹窗，默认 null 不延时</para><para lang="en">Delay showing the popup for specified milliseconds. Default is null (no delay).</para></param>
    public Task Show(int? delay = null) => InvokeVoidAsync("show", Id, delay);

    /// <summary>
    /// <para lang="zh">关闭 Tooltip 弹窗方法</para>
    /// <para lang="en">Hides the Tooltip popup</para>
    /// </summary>
    /// <param name="delay"><para lang="zh">延时指定毫秒后关闭弹窗，默认 null 不延时</para><para lang="en">Delay hiding the popup for specified milliseconds. Default is null (no delay).</para></param>
    /// <returns></returns>
    public Task Hide(int? delay = null) => InvokeVoidAsync("hide", Id, delay);

    /// <summary>
    /// <para lang="zh">切换 Tooltip 弹窗当前状态方法</para>
    /// <para lang="en">Toggles the Tooltip popup state</para>
    /// </summary>
    /// <param name="delay"><para lang="zh">延时指定毫秒后切换弹窗状态，默认 null 不延时</para><para lang="en">Delay toggling the popup state for specified milliseconds. Default is null (no delay).</para></param>
    /// <returns></returns>
    public Task Toggle(int? delay = null) => InvokeVoidAsync("toggle", Id, delay);
}
