// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Components;

/// <summary>
/// RibbonTab 组件
/// </summary>
public partial class RibbonTab : IDisposable
{
    /// <summary>
    /// 获得/设置 是否悬浮 默认 false
    /// </summary>
    [Parameter]
    public bool IsFloat { get; set; }

    /// <summary>
    /// 获得/设置 是否悬浮回调委托
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsFloatChanged { get; set; }

    /// <summary>
    /// 获得/设置 是否显示悬浮小箭头 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowFloatButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 IsFloat 改变时回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnFloatChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string RibbonArrowUpIcon { get; set; } = "fa fa-angle-up fa-2x";

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string RibbonArrowDownIcon { get; set; } = "fa fa-angle-down fa-2x";

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string RibbonArrowPinIcon { get; set; } = "fa fa-thumb-tack fa-rotate-90";

    private string? ArrowIconClassString => CssBuilder.Default()
        .AddClass(RibbonArrowUpIcon, !IsFloat)
        .AddClass(RibbonArrowDownIcon, IsFloat && !IsExpand)
        .AddClass(RibbonArrowPinIcon, IsFloat && IsExpand)
        .Build();

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 点击命令按钮回调方法
    /// </summary>
    [Parameter]
    public Func<RibbonTabItem, Task>? OnTabItemClickAsync { get; set; }

    /// <summary>
    /// 获得/设置 右侧按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? RightButtonsTemplate { get; set; }

    private bool IsExpand { get; set; }

    private JSInterop<RibbonTab>? Interop { get; set; }

    private string? HeaderClassString => CssBuilder.Default("ribbon-tab")
        .AddClass("is-float", IsFloat)
        .AddClass("is-expand", IsFloat && IsExpand)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private static string? GetClassString(RibbonTabItem item) => CssBuilder.Default()
        .AddClass("active", item.IsActive)
        .Build();

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Interop = new(JSRuntime);
            await Interop.InvokeVoidAsync(this, Id, "bb_ribbon", nameof(SetExpand));
        }
    }

    /// <summary>
    /// SetExpand 方法
    /// </summary>
    [JSInvokable]
    public void SetExpand()
    {
        IsExpand = false;
        StateHasChanged();
    }

    private async Task OnClick(RibbonTabItem item)
    {
        if (OnTabItemClickAsync != null)
        {
            await OnTabItemClickAsync(item);
        }
    }

    private Task OnClickTab(TabItem item)
    {
        if (IsFloat)
        {
            IsExpand = true;
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

    private IEnumerable<RibbonTabItem> GetItems() => Items ?? Enumerable.Empty<RibbonTabItem>();

    private async Task OnToggleFloat()
    {
        IsFloat = !IsFloat;
        if (!IsFloat)
        {
            IsExpand = false;
        }
        if (IsFloatChanged.HasDelegate)
        {
            await IsFloatChanged.InvokeAsync(IsFloat);
        }
        if (OnFloatChanged != null)
        {
            await OnFloatChanged(IsFloat);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            if (Interop != null)
            {
                Interop.Dispose();
                Interop = null;
            }
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
