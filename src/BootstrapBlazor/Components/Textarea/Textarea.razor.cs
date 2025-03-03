// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Textarea 组件
/// </summary>
public partial class Textarea
{
    /// <summary>
    /// 滚动到顶部
    /// </summary>
    /// <returns></returns>
    public Task ScrollToTop() => InvokeVoidAsync("execute", Id, "toTop");

    /// <summary>
    /// 滚动到数值
    /// </summary>
    /// <returns></returns>
    public Task ScrollTo(int value) => InvokeVoidAsync("execute", Id, "to", value);

    /// <summary>
    /// 滚动到底部
    /// </summary>
    /// <returns></returns>
    public Task ScrollToBottom() => InvokeVoidAsync("execute", Id, "toBottom");

    /// <summary>
    /// 获得/设置 是否自动滚屏 默认 false
    /// </summary>
    [Parameter]
    public bool IsAutoScroll { get; set; }

    /// <summary>
    /// 获得/设置 是否使用 Shift + Enter 代替原回车按键行为 默认为 false
    /// </summary>
    [Parameter]
    public bool UseShiftEnter { get; set; }

    /// <summary>
    /// 获得 客户端是否自动滚屏标识
    /// </summary>
    private string? AutoScrollString => IsAutoScroll ? "auto" : null;

    private string? ShiftEnterString => UseShiftEnter ? "true" : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("execute", Id, "update");
        }
    }
}
