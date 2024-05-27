// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("execute", Id, IsOpen);
        }
    }

    private DrawerOption GetOption() => new()
    {
        Height = Height,
        Width = Width,
        IsBackdrop = IsBackdrop,
        ShowBackdrop = ShowBackdrop,
        Placement = Placement,
        AllowResize = AllowResize,
        OnClickBackdrop = OnClickBackdrop,
        ChildContent = ChildContent
    };

    /// <summary>
    /// 点击背景遮罩方法
    /// </summary>
    public async Task OnContainerClick()
    {
        if (IsBackdrop)
        {
            await Close(GetOption());
            if (OnClickBackdrop != null) await OnClickBackdrop.Invoke();
        }
    }

    /// <summary>
    /// 关闭抽屉方法
    /// </summary>
    /// <returns></returns>
    public async Task Close(DrawerOption option)
    {
        IsOpen = false;
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
