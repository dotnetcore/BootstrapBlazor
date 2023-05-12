// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Transition 动画组件
/// </summary>
public partial class Transition
{
    private string? ClassString => CssBuilder.Default("animate__animated")
        .AddClass(TransitionType.ToDescriptionString(), Show)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"animate-duration: {Duration / 1000}s", Duration > 100)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 是否显示动画 默认 true
    /// </summary>
    [Parameter]
    public bool Show { get; set; } = true;

    /// <summary>
    /// 获得/设置 动画名称 默认 FadeIn
    /// </summary>
    [Parameter]
    public TransitionType TransitionType { get; set; } = TransitionType.FadeIn;

    /// <summary>
    ///  获得/设置 动画执行时长 单位毫秒 默认为 0 未生效
    /// </summary>
    [Parameter]
    public int Duration { get; set; }

    /// <summary>
    /// 获得/设置 子内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 动画执行完成回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnTransitionEnd { get; set; }

    /// <summary>
    /// 动画执行完毕结束异步方法 JSInvoke 调用
    /// </summary>
    [JSInvokable]
    public async Task TransitionEndAsync()
    {
        if (OnTransitionEnd != null)
        {
            await OnTransitionEnd();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(TransitionEndAsync));
}
