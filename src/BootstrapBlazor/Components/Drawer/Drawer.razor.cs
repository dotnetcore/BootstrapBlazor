// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer 组件基类
/// </summary>
public sealed partial class Drawer
{
    private ElementReference DrawerElement { get; set; }

    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("drawer-wrapper")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 抽屉 Style 字符串
    /// </summary>
    private string? DrawerStyleString => CssBuilder.Default()
        .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width) && Placement != Placement.Top && Placement != Placement.Bottom)
        .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height) && (Placement == Placement.Top || Placement == Placement.Bottom))
        .Build();

    /// <summary>
    /// 获得 抽屉样式
    /// </summary>
    private string? DrawerClassString => CssBuilder.Default("drawer")
        .AddClass("left", Placement != Placement.Right && Placement != Placement.Top && Placement != Placement.Bottom)
        .AddClass("top", Placement == Placement.Top)
        .AddClass("right", Placement == Placement.Right)
        .AddClass("bottom", Placement == Placement.Bottom)
        .Build();

    /// <summary>
    /// 获得/设置 抽屉宽度 左右布局时生效
    /// </summary>
    [Parameter]
    public string Width { get; set; } = "360px";

    /// <summary>
    /// 获得/设置 抽屉高度 上下布局时生效
    /// </summary>
    [Parameter]
    public string Height { get; set; } = "290px";

    /// <summary>
    /// 获得/设置 抽屉是否打开 默认 false 未打开
    /// </summary>
    [Parameter]
    public bool IsOpen { get; set; }

    /// <summary>
    /// 获得/设置 IsOpen 属性改变时回调委托方法
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsOpenChanged { get; set; }

    /// <summary>
    /// 获得/设置 点击背景遮罩时回调委托方法 默认为 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickBackdrop { get; set; }

    /// <summary>
    /// 获得/设置 点击遮罩是否关闭抽屉 默认为 false
    /// </summary>
    [Parameter]
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// 获得/设置 组件出现位置 默认显示在 Left 位置
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Left;

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await JSRuntime.InvokeVoidAsync(DrawerElement, "bb_drawer", IsOpen);
        }
    }

    /// <summary>
    /// 点击背景遮罩方法
    /// </summary>
    public async Task OnContainerClick()
    {
        if (IsBackdrop)
        {
            IsOpen = false;
            if (IsOpenChanged.HasDelegate) await IsOpenChanged.InvokeAsync(IsOpen);
            if (OnClickBackdrop != null) await OnClickBackdrop.Invoke();
        }
    }
}
