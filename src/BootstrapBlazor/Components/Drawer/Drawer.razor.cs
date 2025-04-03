// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Drawer component
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
        .AddStyle("--bb-drawer-position", Position)
        .AddClass($"--bb-drawer-zindex: {ZIndex};", ZIndex.HasValue)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 抽屉 Style 字符串
    /// </summary>
    private string? DrawerStyleString => CssBuilder.Default()
        .AddStyle("--bb-drawer-width", Width, Placement != Placement.Top && Placement != Placement.Bottom)
        .AddStyle("--bb-drawer-height", Height, Placement == Placement.Top || Placement == Placement.Bottom)
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
    /// 获得/设置 z-index 参数值 默认 null 未设置
    /// </summary>
    [Parameter]
    public int? ZIndex { get; set; }

    /// <summary>
    /// 获得/设置 关闭抽屉回调委托 默认 null
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 抽屉内容相关数据 多用于传值
    /// </summary>
    [Parameter]
    public object? BodyContext { get; set; }

    /// <summary>
    /// 获得/设置 是否支持键盘 ESC 关闭当前弹窗 默认 false
    /// </summary>
    [Parameter]
    public bool IsKeyboard { get; set; }

    /// <summary>
    /// 获得/设置 抽屉显示时是否允许滚动 body 默认为 false 不滚动
    /// </summary>
    [Parameter]
    public bool BodyScroll { get; set; }

    private string? KeyboardString => IsKeyboard ? "true" : null;

    private string? BodyScrollString => BodyScroll ? "true" : null;

    private bool _render = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override bool ShouldRender() => _render;

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
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(Close));

    private RenderFragment RenderBackdrop() => builder =>
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(10, "class", "drawer-backdrop modal-backdrop fade");
        if (IsBackdrop)
        {
            builder.AddAttribute(20, "onclick", EventCallback.Factory.Create(this, OnContainerClick));
        }
        builder.CloseElement();
    };

    /// <summary>
    /// 点击背景遮罩方法
    /// </summary>
    public async Task OnContainerClick()
    {
        if (OnClickBackdrop != null)
        {
            await OnClickBackdrop();
        }
        _render = false;
        await Close();
        _render = true;
    }

    /// <summary>
    /// 关闭抽屉方法
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task Close()
    {
        IsOpen = false;
        var animation = await InvokeAsync<bool>("execute", Id, false);
        if (animation)
        {
            await Task.Delay(300);
        }
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
