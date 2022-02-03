// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ResultDialog 对话框类
/// </summary>
public partial class ResultDialogFooter
{
    /// <summary>
    /// 显示确认按钮
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// 确认按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// 确认按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonYesIcon { get; set; } = "fa fa-check";

    /// <summary>
    /// 确认按钮颜色
    /// </summary>
    [Parameter] public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// 显示取消按钮
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// 取消按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// 取消按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonNoIcon { get; set; } = "fa fa-close";

    /// <summary>
    /// 取消按钮颜色
    /// </summary>
    [Parameter]
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// 显示关闭按钮
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 关闭按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// 关闭按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonCloseIcon { get; set; } = "fa fa-close";

    /// <summary>
    /// 关闭按钮颜色
    /// </summary>
    [Parameter]
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 点击关闭按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickClose { get; set; }

    /// <summary>
    /// 获得/设置 点击确认按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickYes { get; set; }

    /// <summary>
    /// 获得/设置 点击取消按钮回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClickNo { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ResultDialogOption>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ButtonCloseText ??= Localizer[nameof(ButtonCloseText)];
        ButtonNoText ??= Localizer[nameof(ButtonNoText)];
        ButtonYesText ??= Localizer[nameof(ButtonYesText)];
    }

    private async Task ButtonClick(DialogResult dialogResult)
    {
        if (dialogResult == DialogResult.Yes && OnClickYes != null)
        {
            await OnClickYes();
        }
        if (dialogResult == DialogResult.No && OnClickNo != null)
        {
            await OnClickNo();
        }
        if (dialogResult == DialogResult.Close && OnClickClose != null)
        {
            await OnClickClose();
        }
    }
}
