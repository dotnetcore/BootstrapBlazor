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
    /// 获得/设置 倒计时或者计时的开始时间 <see cref="FlipClockViewMode.CountDown"/> 默认 <see cref="FlipClockViewMode.Count" /> 模式下生效
    /// </summary>
    [Parameter]
    public DateTimeOffset? StartValue { get; set; }

    private string? ClassString => CssBuilder.Default("bb-flip-clock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, ViewMode = ViewMode.ToString(), StartValue = GetTicks() / 10000 });

    private long GetTicks() => StartValue.HasValue ? StartValue.Value.UtcTicks - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks : 0;

    /// <summary>
    /// Timing end callback method called by js invoke
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
