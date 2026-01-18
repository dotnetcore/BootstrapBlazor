// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Transition 动画组件</para>
/// <para lang="en">Transition Animation Component</para>
/// </summary>
public partial class Transition
{
    private string? ClassString => CssBuilder.Default("animate__animated")
        .AddClass(TransitionType.ToDescriptionString(), Show)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--animate-duration: {Duration / 1000}s;", Duration > 100)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示动画，默认 true</para>
    /// <para lang="en">Gets or sets whether to display the animation. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool Show { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 动画名称，默认 FadeIn</para>
    /// <para lang="en">Gets or sets the animation name. Default is FadeIn.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public TransitionType TransitionType { get; set; } = TransitionType.FadeIn;

    /// <summary>
    /// <para lang="zh">获得/设置 动画执行时长，单位毫秒，默认为 0</para>
    /// <para lang="en">Gets or sets the animation execution duration in milliseconds. Default is 0.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Duration { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子内容</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 动画执行完成回调委托</para>
    /// <para lang="en">Gets or sets the animation completion callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnTransitionEnd { get; set; }

    /// <summary>
    /// <para lang="zh">动画执行完毕结束的异步方法，由 JSInvoke 调用</para>
    /// <para lang="en">Called when animation ends, triggered by JSInvoke</para>
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
