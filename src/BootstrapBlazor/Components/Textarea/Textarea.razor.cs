// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Textarea 文本域组件</para>
/// <para lang="en">Textarea Component</para>
/// </summary>
public partial class Textarea
{
    /// <summary>
    /// <para lang="zh">滚动到顶部方法</para>
    /// <para lang="en">Scrolls to the top</para>
    /// </summary>
    public Task ScrollToTop() => InvokeVoidAsync("execute", Id, "toTop");

    /// <summary>
    /// <para lang="zh">滚动到指定位置方法</para>
    /// <para lang="en">Scrolls to a specific value</para>
    /// </summary>
    public Task ScrollTo(int value) => InvokeVoidAsync("execute", Id, "to", value);

    /// <summary>
    /// <para lang="zh">滚动到底部方法</para>
    /// <para lang="en">Scrolls to the bottom</para>
    /// </summary>
    public Task ScrollToBottom() => InvokeVoidAsync("execute", Id, "toBottom");

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动滚动，默认为 false</para>
    /// <para lang="en">Gets or sets whether auto-scroll is enabled. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsAutoScroll { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 Shift + Enter 替代默认 Enter 键行为，默认为 false</para>
    /// <para lang="en">Gets or sets whether Shift + Enter replaces the default Enter key behavior. Default is false</para>
    /// </summary>
    [Parameter]
    public bool UseShiftEnter { get; set; }

    private string? AutoScrollString => IsAutoScroll ? "auto" : null;

    private string? ShiftEnterString => UseShiftEnter ? "true" : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("execute", Id, "update");
        }
    }
}
