// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">可见检测组件
///</para>
/// <para lang="en">可见检测component
///</para>
/// </summary>
public partial class IntersectionObserver
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否使用元素视口作为根元素 默认为 true 使用当前元素作为根元素
    /// <para>The element that is used as the viewport for checking visibility of the target. Must be the ancestor of the target. Defaults to the browser viewport if value is false. Default value is true</para>
    ///</para>
    /// <para lang="en">Gets or sets whether使用元素视口作为根元素 Default is为 true 使用当前元素作为根元素
    /// <para>The element that is used as the viewport for checking visibility of the target. Must be the ancestor of the target. Defaults to the browser viewport if value is false. Default value is true</para>
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool UseElementViewport { get; set; } = true;

    /// <summary>
    /// <para lang="zh">Margin around the root. Can have values similar to the CSS margin 属性, e.g. "10px 20px 30px 40px" (top, right, bottom, left). values can be percentages. This set of values serves to grow or shrink each side of the root element's bounding box before computing intersections. 默认s to all zeros.
    ///</para>
    /// <para lang="en">Margin around the root. Can have values similar to the CSS margin property, e.g. "10px 20px 30px 40px" (top, right, bottom, left). The values can be percentages. This set of values serves to grow or shrink each side of the root element's bounding box before computing intersections. Defaults to all zeros.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RootMargin { get; set; }

    /// <summary>
    /// <para lang="zh">Either a single number or an array of numbers which indicate at what percentage of the target's visibility the observer's 回调 should be executed. If you only want to detect when visibility passes the 50% mark, you can use a value of 0.5. If you want the 回调 to run every time visibility passes another 25%, you would specify the array [0, 0.25, 0.5, 0.75, 1]. default is 0 (meaning as soon as even one pixel is visible, the 回调 will be run). A value of 1.0 means that the threshold isn't considered passed until every pixel is visible.
    ///</para>
    /// <para lang="en">Either a single number or an array of numbers which indicate at what percentage of the target's visibility the observer's callback should be executed. If you only want to detect when visibility passes the 50% mark, you can use a value of 0.5. If you want the callback to run every time visibility passes another 25%, you would specify the array [0, 0.25, 0.5, 0.75, 1]. The default is 0 (meaning as soon as even one pixel is visible, the callback will be run). A value of 1.0 means that the threshold isn't considered passed until every pixel is visible.
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Threshold { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 可见后是否自动取消观察 默认 true 可见后自动取消观察提高性能
    ///</para>
    /// <para lang="en">Gets or sets 可见后whether自动取消观察 Default is true 可见后自动取消观察提高性能
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoUnobserveWhenIntersection { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 不可见后是否自动取消观察 默认 false 不可见后自动取消观察提高性能
    ///</para>
    /// <para lang="en">Gets or sets 不可见后whether自动取消观察 Default is false 不可见后自动取消观察提高性能
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool AutoUnobserveWhenNotIntersection { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 已经交叉回调方法
    ///</para>
    /// <para lang="en">Gets or sets 已经交叉callback method
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IntersectionObserverEntry, Task>? OnIntersecting { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件
    ///</para>
    /// <para lang="en">Gets or sets 子component
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-intersection-observer")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { UseElementViewport, RootMargin, Threshold, AutoUnobserveWhenIntersection, AutoUnobserveWhenNotIntersection, Callback = nameof(TriggerIntersecting) });

    /// <summary>
    /// <para lang="zh">交叉检测回调方法 由 JavaScript 调用
    ///</para>
    /// <para lang="en">交叉检测callback method 由 JavaScript 调用
    ///</para>
    /// </summary>
    /// <param name="entry"><see cref="IntersectionObserverEntry"/> 实例</param>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerIntersecting(IntersectionObserverEntry entry)
    {
        if (OnIntersecting != null)
        {
            await OnIntersecting(entry);
        }
    }
}
