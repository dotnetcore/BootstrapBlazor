// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 确认弹窗按钮组件
/// </summary>
public abstract class PopConfirmButtonBase : ButtonBase
{
    /// <summary>
    /// 获得/设置 弹窗显示位置
    /// </summary>
    [Parameter]
    public Placement Placement { get; set; } = Placement.Auto;

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 点击确认时回调方法
    /// </summary>
    [Parameter]
    public Func<Task> OnConfirm { get; set; } = () => Task.CompletedTask;

    /// <summary>
    /// 获得/设置 点击关闭时回调方法
    /// </summary>
    [Parameter]
    public Func<Task> OnClose { get; set; } = () => Task.CompletedTask;

    /// <summary>
    /// 获得/设置 点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出
    /// </summary>
    [Parameter]
    public Func<Task<bool>> OnBeforeClick { get; set; } = () => Task.FromResult(true);

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮颜色
    /// </summary>
    [Parameter]
    public Color CloseButtonColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 关闭按钮显示文字 默认为 关闭
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮显示文字 默认为 确定
    /// </summary>
    [Parameter]
    [NotNull]
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
    public string ConfirmIcon { get; set; } = "fa fa-exclamation-circle text-info";
}
