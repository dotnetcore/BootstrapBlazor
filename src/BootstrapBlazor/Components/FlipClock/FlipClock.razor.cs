// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/


namespace BootstrapBlazor.Components;

/// <summary>
/// FlipClock 组件
/// </summary>
public partial class FlipClock
{
    /// <summary>
    /// 获得/设置 是否显示 Hour 默认 true
    /// </summary>
    [Parameter]
    public bool ShowHour { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Minute 默认 true
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Second 默认 true
    /// </summary>
    [Parameter]
    public bool ShowSecond { get; set; } = true;

    /// <summary>
    /// 获得/设置 计时结束回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnCompletedAsync { get; set; }

    /// <summary>
    /// 获得/设置 显示模式 默认 <see cref="FlipClockViewMode.DateTime"/>
    /// </summary>
    [Parameter]
    public FlipClockViewMode ViewMode { get; set; }

    /// <summary>
    /// 获得/设置 组件高度 默认 null 未设置使用样式默认值 200px;
    /// </summary>
    /// <remarks>支持多种单位 200px 200em 200pt 100% 等</remarks>
    [Parameter]
    public string? Height { get; set; }

    /// <summary>
    /// 获得/设置 组件背景色 默认 null 未设置使用样式默认值 radial-gradient(ellipse at center, rgba(150, 150, 150, 1) 0%, rgba(89, 89, 89, 1) 100%);
    /// </summary>
    [Parameter]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// 获得/设置 组件字体大小 默认 null 未设置使用样式默认值 80px;
    /// </summary>
    [Parameter]
    public string? FontSize { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片宽度 默认 null 未设置使用样式默认值 60px;
    /// </summary>
    [Parameter]
    public string? CardWidth { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片高度 默认 null 未设置使用样式默认值 90px;
    /// </summary>
    [Parameter]
    public string? CardHeight { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片字体颜色 默认 null 未设置使用样式默认值 #ccc;
    /// </summary>
    [Parameter]
    public string? CardColor { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片背景颜色 默认 null 未设置使用样式默认值 #333;
    /// </summary>
    [Parameter]
    public string? CardBackgroundColor { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片分割线高度 默认 null 未设置使用样式默认值 1px;
    /// </summary>
    [Parameter]
    public string? CardDividerHeight { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片分割线颜色 默认 null 未设置使用样式默认值 rgba(0, 0, 0, .4);
    /// </summary>
    [Parameter]
    public string? CardDividerColor { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片间隔 默认 null 未设置使用样式默认值 5;
    /// </summary>
    [Parameter]
    public string? CardMargin { get; set; }

    /// <summary>
    /// 获得/设置 组件卡片组间隔 默认 null 未设置使用样式默认值 20;
    /// </summary>
    [Parameter]
    public string? CardGroupMargin { get; set; }

    /// <summary>
    /// 获得/设置 倒计时或者计时的开始时间 <see cref="FlipClockViewMode.Count"/> 默认 <see cref="FlipClockViewMode.CountDown" /> 模式下生效
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
    /// 倒计时结束回调方法由 JSInvoke 调用
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
