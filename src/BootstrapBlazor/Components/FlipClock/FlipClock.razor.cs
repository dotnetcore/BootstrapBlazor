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
    /// 获得/设置 是否显示使用本地时间 默认 true
    /// </summary>
    [Parameter]
    public bool UseLocaleTimeZone { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否为倒计时 默认 false
    /// </summary>
    [Parameter]
    public bool IsCountDown { get; set; }

    /// <summary>
    /// 获得/设置 倒计时开始时间 <see cref="IsCountDown"/> 默认 null 未设置
    /// </summary>
    [Parameter]
    public DateTime? StartValue { get; set; }

    private string? ClassString => CssBuilder.Default("bb-flip-clock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { Invoke = Interop, OnCompleted = nameof(OnCompleted), UseLocaleTimeZone });

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
