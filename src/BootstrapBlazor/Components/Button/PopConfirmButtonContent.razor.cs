// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Popover Confirm 组件
/// </summary>
public partial class PopConfirmButtonContent
{
    /// <summary>
    /// 获得 关闭按钮样式
    /// </summary>
    protected string? CloseButtonClass => CssBuilder.Default("btn btn-xs")
        .AddClass($"btn-{CloseButtonColor.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得 关闭按钮样式
    /// </summary>
    protected string? ConfirmButtonClass => CssBuilder.Default("btn btn-xs")
        .AddClass($"btn-{ConfirmButtonColor.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得 图标样式
    /// </summary>
    protected string? IconClass => CssBuilder.Default()
        .AddClass(Icon)
        .Build();

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 自定义组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮显示文字
    /// </summary>
    [Parameter]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 确认按钮显示文字
    /// </summary>
    [Parameter]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color ConfirmButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 确认框图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClose { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= "fa-solid fa-exclamation-circle text-info";
    }

    /// <summary>
    /// 点击关闭按钮调用此方法
    /// </summary>
    public async Task OnCloseClick()
    {
        if (OnClose != null)
        {
            await OnClose();
        }
    }

    /// <summary>
    /// 点击确认按钮调用此方法
    /// </summary>
    public async Task OnConfirmClick()
    {
        if (OnConfirm != null)
        {
            await OnConfirm();
        }
    }
}
