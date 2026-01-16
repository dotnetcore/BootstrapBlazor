// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Tooltip 组件</para>
///  <para lang="en">Tooltip component</para>
/// </summary>
public partial class Tooltip : ITooltip
{
    /// <summary>
    ///  <para lang="zh">弹窗位置字符串</para>
    ///  <para lang="en">弹窗位置字符串</para>
    /// </summary>
    protected string? PlacementString => Placement == Placement.Auto ? null : Placement.ToDescriptionString();

    /// <summary>
    ///  <para lang="zh">获得 是否关键字过滤字符串</para>
    ///  <para lang="en">Gets whether关键字过滤字符串</para>
    /// </summary>
    protected string? SanitizeString => Sanitize ? null : "false";

    /// <summary>
    ///  <para lang="zh">获得 是否 Html 字符串</para>
    ///  <para lang="en">Gets whether Html 字符串</para>
    /// </summary>
    protected string? HtmlString => IsHtml ? "true" : null;

    private string? ClassString => CssBuilder.Default("bb-tooltip")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh">fallbackPlacements 参数</para>
    ///  <para lang="en">fallbackPlacements 参数</para>
    /// </summary>
    protected string? FallbackPlacementsString => FallbackPlacements != null ? string.Join(",", FallbackPlacements) : null;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Delay { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Selector { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 获得显示内容异步回调方法 默认 null</para>
    ///  <para lang="en">Gets or sets Getsdisplaycontent异步callback method Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task<string>>? GetTitleCallback { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsHtml { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Sanitize { get; set; } = true;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Top;

    /// <summary>
    ///  <para lang="zh">获得/设置 位置 默认为 null</para>
    ///  <para lang="en">Gets or sets 位置 Default is为 null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string[]? FallbackPlacements { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 偏移量 默认为 null</para>
    ///  <para lang="en">Gets or sets 偏移量 Default is为 null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Offset { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CustomClass { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Trigger { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 子组件</para>
    ///  <para lang="en">Gets or sets 子component</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Trigger ??= "focus hover";
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
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
    ///  <para lang="zh">设置参数方法</para>
    ///  <para lang="en">Sets参数方法</para>
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
    ///  <para lang="zh">显示 Tooltip 弹窗方法</para>
    ///  <para lang="en">display Tooltip 弹窗方法</para>
    /// </summary>
    /// <param name="delay"><para lang="zh">延时指定毫秒后显示弹窗 默认 null 不延时</para><para lang="en">延时指定毫秒后display弹窗 default is null 不延时</para></param>
    /// <returns></returns>
    public Task Show(int? delay = null) => InvokeVoidAsync("show", Id, delay);

    /// <summary>
    ///  <para lang="zh">关闭 Tooltip 弹窗方法</para>
    ///  <para lang="en">关闭 Tooltip 弹窗方法</para>
    /// </summary>
    /// <param name="delay"><para lang="zh">延时指定毫秒后关闭弹窗 默认 null 不延时</para><para lang="en">延时指定毫秒后关闭弹窗 default is null 不延时</para></param>
    /// <returns></returns>
    public Task Hide(int? delay = null) => InvokeVoidAsync("hide", Id, delay);

    /// <summary>
    ///  <para lang="zh">切换 Tooltip 弹窗当前状态方法</para>
    ///  <para lang="en">切换 Tooltip 弹窗当前状态方法</para>
    /// </summary>
    /// <param name="delay"><para lang="zh">延时指定毫秒后切换弹窗方法 默认 null 不延时</para><para lang="en">延时指定毫秒后切换弹窗method default is null 不延时</para></param>
    /// <returns></returns>
    public Task Toggle(int? delay = null) => InvokeVoidAsync("toggle", Id, delay);
}
