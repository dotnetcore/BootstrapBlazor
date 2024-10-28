﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer 组件基类
/// </summary>
public partial class Drawer
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("drawer collapse")
        .AddClass("no-bd", !ShowBackdrop)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bb-drawer-position: {Position};", !string.IsNullOrEmpty(Position))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 抽屉 Style 字符串
    /// </summary>
    private string? DrawerStyleString => CssBuilder.Default()
        .AddClass($"--bb-drawer-width: {Width};", !string.IsNullOrEmpty(Width) && Placement != Placement.Top && Placement != Placement.Bottom)
        .AddClass($"--bb-drawer-height: {Height};", !string.IsNullOrEmpty(Height) && (Placement == Placement.Top || Placement == Placement.Bottom))
        .Build();

    /// <summary>
    /// 获得 抽屉样式
    /// </summary>
    private string? DrawerClassString => CssBuilder.Default("drawer-body")
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
    /// 获得/设置 是否显示遮罩 默认为 true 显示遮罩
    /// </summary>
    [Parameter]
    public bool ShowBackdrop { get; set; } = true;

    /// <summary>
    /// 获得/设置 组件出现位置 默认显示在 Left 位置
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Left;

    /// <summary>
    /// 获得/设置 组件定位位置 默认 null 未设置 使用样式内置定位 fixed 可更改为 absolute
    /// </summary>
    [Parameter]
    public string? Position { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否允许调整大小 默认 false
    /// </summary>
    [Parameter]
    public bool AllowResize { get; set; }

    /// <summary>
    /// 获得/设置 关闭抽屉回调委托 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender || IsOpen)
        {
            await InvokeVoidAsync("execute", Id, IsOpen);
        }
    }

    /// <summary>
    /// 点击背景遮罩方法
    /// </summary>
    public async Task OnContainerClick()
    {
        if (IsBackdrop)
        {
            if (OnClickBackdrop != null)
            {
                await OnClickBackdrop();
            }
            await Close();
        }
    }

    /// <summary>
    /// 关闭抽屉方法
    /// </summary>
    /// <returns></returns>
    public async Task Close()
    {
        IsOpen = false;
        if (OnCloseAsync != null)
        {
            await OnCloseAsync();
        }
        if (IsOpenChanged.HasDelegate)
        {
            await IsOpenChanged.InvokeAsync(IsOpen);
        }
        else
        {
            StateHasChanged();
        }
    }
}
