// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapTooltip 组件
/// </summary>
public partial class BootstrapTooltip : IAsyncDisposable
{
    /// <summary>
    /// 获得/设置 显示文字是否为 Html 默认为 false
    /// </summary>
    [Parameter]
    public bool IsHtml { get; set; }

    /// <summary>
    /// 获得/设置 位置 默认为 Placement.Top
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Top;

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    [NotNull]
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
    /// </summary>
    [Parameter]
    public string Trigger { get; set; } = "focus hover";

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Title ??= "";
        CustomClass ??= "";
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await ExecuteTooltip();
        }
    }

    /// <summary>
    /// 显示 Tooltip 方法
    /// </summary>
    /// <returns></returns>
    public ValueTask ExecuteTooltip() => JSRuntime.InvokeVoidAsync(Id, "bb_tooltip", "", Title, Placement.ToDescriptionString(), IsHtml, Trigger, CustomClass);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public ValueTask DisposeTooltip() => JSRuntime.InvokeVoidAsync(Id, "bb_tooltip", "dispose");

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await DisposeTooltip();
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
