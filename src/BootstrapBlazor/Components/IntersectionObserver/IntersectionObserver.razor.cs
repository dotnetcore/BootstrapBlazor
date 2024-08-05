// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/


namespace BootstrapBlazor.Components;

/// <summary>
/// 可见检测组件
/// </summary>
public partial class IntersectionObserver
{
    /// <summary>
    /// The element that is used as the viewport for checking visibility of the target. Must be the ancestor of the target. Defaults to the browser viewport if not specified or if null
    /// </summary>
    [Parameter]
    public string? RootSelector { get; set; }

    /// <summary>
    /// Margin around the root. Can have values similar to the CSS margin property, e.g. "10px 20px 30px 40px" (top, right, bottom, left). The values can be percentages. This set of values serves to grow or shrink each side of the root element's bounding box before computing intersections. Defaults to all zeros.
    /// </summary>
    [Parameter]
    public string? RootMargin { get; set; }

    /// <summary>
    /// Either a single number or an array of numbers which indicate at what percentage of the target's visibility the observer's callback should be executed. If you only want to detect when visibility passes the 50% mark, you can use a value of 0.5. If you want the callback to run every time visibility passes another 25%, you would specify the array [0, 0.25, 0.5, 0.75, 1]. The default is 0 (meaning as soon as even one pixel is visible, the callback will be run). A value of 1.0 means that the threshold isn't considered passed until every pixel is visible.
    /// </summary>
    [Parameter]
    public float Threshold { get; set; }

    /// <summary>
    /// 获得/设置 是否自动取消观察 默认 true 可见后自动取消观察提高性能
    /// </summary>
    [Parameter]
    public bool AutoUnobserve { get; set; } = true;

    /// <summary>
    /// 获得/设置 已经交叉回调方法
    /// </summary>
    [Parameter]
    public Func<int, Task>? OnIntersectingAsync { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-intersection-observer")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Threshold < 0 || Threshold > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(Threshold), $"{nameof(Threshold)} must be between 0 and 1");
        }

        if (string.IsNullOrEmpty(RootMargin))
        {
            RootMargin = "0px 0px 0px 0px";
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { Root = RootSelector, RootMargin, Threshold, AutoUnobserve });

    /// <summary>
    /// 交叉检测回调方法 由 JavaScript 调用
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task OnIntersecting(int index)
    {
        if (OnIntersectingAsync != null)
        {
            await OnIntersectingAsync(index);
        }
    }
}
