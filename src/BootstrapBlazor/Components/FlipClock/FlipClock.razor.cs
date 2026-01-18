// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone


namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FlipClock 组件</para>
/// <para lang="en">FlipClock Component</para>
/// </summary>
public partial class FlipClock
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Year 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Year Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowYear { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Month 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Month Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowMonth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Day 默认 false</para>
    /// <para lang="en">Gets or sets Whether to Show Day Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDay { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Hour 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Show Hour Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowHour { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Minute 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Show Minute Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Second 默认 true</para>
    /// <para lang="en">Gets or sets Whether to Show Second Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 计时结束回调方法 默认 null</para>
    /// <para lang="en">Gets or sets Timer Completed Callback Method Default null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCompletedAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示模式 默认 <see cref="FlipClockViewMode.DateTime"/></para>
    /// <para lang="en">Gets or sets View Mode Default <see cref="FlipClockViewMode.DateTime"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public FlipClockViewMode ViewMode { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件高度 默认 null 未设置使用样式默认值 200px;</para>
    /// <para lang="en">Gets or sets Component Height Default null Use Style Default Value if not set 200px;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>支持多种单位 200px 200em 200pt 100% 等</remarks>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件背景色 默认 null 未设置使用样式默认值 radial-gradient(ellipse at center, rgba(150, 150, 150, 1) 0%, rgba(89, 89, 89, 1) 100%);</para>
    /// <para lang="en">Gets or sets Component Background Color Default null Use Style Default Value if not set radial-gradient(ellipse at center, rgba(150, 150, 150, 1) 0%, rgba(89, 89, 89, 1) 100%);</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件字体大小 默认 null 未设置使用样式默认值 80px;</para>
    /// <para lang="en">Gets or sets Component Font Size Default null Use Style Default Value if not set 80px;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? FontSize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片宽度 默认 null 未设置使用样式默认值 60px;</para>
    /// <para lang="en">Gets or sets Component Card Width Default null Use Style Default Value if not set 60px;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片高度 默认 null 未设置使用样式默认值 90px;</para>
    /// <para lang="en">Gets or sets Component Card Height Default null Use Style Default Value if not set 90px;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardHeight { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片字体颜色 默认 null 未设置使用样式默认值 #ccc;</para>
    /// <para lang="en">Gets or sets Component Card Font Color Default null Use Style Default Value if not set #ccc;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片背景颜色 默认 null 未设置使用样式默认值 #333;</para>
    /// <para lang="en">Gets or sets Component Card Background Color Default null Use Style Default Value if not set #333;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardBackgroundColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片分割线高度 默认 null 未设置使用样式默认值 1px;</para>
    /// <para lang="en">Gets or sets Component Card Divider Height Default null Use Style Default Value if not set 1px;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardDividerHeight { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片分割线颜色 默认 null 未设置使用样式默认值 rgba(0, 0, 0, .4);</para>
    /// <para lang="en">Gets or sets Component Card Divider Color Default null Use Style Default Value if not set rgba(0, 0, 0, .4);</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardDividerColor { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片间隔 默认 null 未设置使用样式默认值 5;</para>
    /// <para lang="en">Gets or sets Component Card Margin Default null Use Style Default Value if not set 5;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardMargin { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件卡片组间隔 默认 null 未设置使用样式默认值 20;</para>
    /// <para lang="en">Gets or sets Component Card Group Margin Default null Use Style Default Value if not set 20;</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? CardGroupMargin { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 倒计时或者计时的开始时间 <see cref="FlipClockViewMode.Count"/> 默认 <see cref="FlipClockViewMode.CountDown" /> 模式下生效</para>
    /// <para lang="en">Gets or sets Start Time for Countdown or Count <see cref="FlipClockViewMode.Count"/> Default Effective in <see cref="FlipClockViewMode.CountDown" /> Mode</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TimeSpan? StartValue { get; set; }

    private string? ClassString => CssBuilder.Default("bb-flip-clock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-flip-clock-height: {Height};", !string.IsNullOrEmpty(Height))
        .AddClass($"--bb-flip-clock-bg: {BackgroundColor};", !string.IsNullOrEmpty(BackgroundColor))
        .AddClass($"--bb-flip-clock-font-size: {FontSize};", !string.IsNullOrEmpty(FontSize))
        .AddClass($"--bb-flip-clock-item-width: {CardWidth};", !string.IsNullOrEmpty(CardWidth))
        .AddClass($"--bb-flip-clock-item-height: {CardHeight};", !string.IsNullOrEmpty(CardHeight))
        .AddClass($"--bb-flip-clock-number-color: {CardColor};", !string.IsNullOrEmpty(CardColor))
        .AddClass($"--bb-flip-clock-number-bg: {CardBackgroundColor};", !string.IsNullOrEmpty(CardBackgroundColor))
        .AddClass($"--bb-flip-clock-number-line-height: {CardDividerHeight};", !string.IsNullOrEmpty(CardDividerHeight))
        .AddClass($"--bb-flip-clock-number-line-bg: {CardDividerColor};", !string.IsNullOrEmpty(CardDividerColor))
        .AddClass($"--bb-flip-clock-item-margin: {CardMargin};", !string.IsNullOrEmpty(CardMargin))
        .AddClass($"--bb-flip-clock-list-margin-right: {CardGroupMargin};", !string.IsNullOrEmpty(CardGroupMargin))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, OnCompleted = nameof(OnCompleted), ViewMode = ViewMode.ToString(), StartValue = GetTicks() });

    private double GetTicks() => StartValue?.TotalMilliseconds ?? 0;

    /// <summary>
    /// <para lang="zh">倒计时结束回调方法由 JSInvoke 调用</para>
    /// <para lang="en">Countdown Completed Callback Method invoke by JSInvoke</para>
    /// </summary>
    [JSInvokable]
    public async Task OnCompleted()
    {
        if (OnCompletedAsync != null)
        {
            await OnCompletedAsync();
        }
    }
}
