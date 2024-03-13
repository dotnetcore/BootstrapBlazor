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
    /// clock display hour
    /// </summary>
    [Parameter]
    public bool ShowHour { get; set; } = true;

    /// <summary>
    /// clock display minute
    /// </summary>
    [Parameter]
    public bool ShowMinute { get; set; } = true;

    /// <summary>
    /// Timing end callback method
    /// </summary>
    [Parameter]
    public Func<Task>? OnCompletedAsync { get; set; }

    private string? ClassString => CssBuilder.Default("bb-flip-clock")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(OnCompleted));

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
