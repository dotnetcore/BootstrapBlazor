// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 复杂对话框配置类
/// </summary>
public class ResultDialogOption : DialogOption
{
    /// <summary>
    /// 
    /// </summary>
    public ResultDialogOption()
    {
        base.ShowCloseButton = false;
    }

    /// <summary>
    /// 显示确认按钮
    /// </summary>
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// 确认按钮文本
    /// </summary>
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// 确认按钮图标
    /// </summary>
    public string ButtonYesIcon { get; set; } = "fa fa-check";

    /// <summary>
    /// 确认按钮颜色
    /// </summary>
    public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// 显示取消按钮
    /// </summary>
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// 取消按钮文本
    /// </summary>
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// 取消按钮图标
    /// </summary>
    public string ButtonNoIcon { get; set; } = "fa fa-close";

    /// <summary>
    /// 取消按钮颜色
    /// </summary>
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// 关闭按钮文本
    /// </summary>
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// 关闭按钮图标
    /// </summary>
    public string ButtonCloseIcon { get; set; } = "fa fa-close";

    /// <summary>
    /// 
    /// </summary>
    public new bool ShowCloseButton { get; set; } = false;

    /// <summary>
    /// 关闭按钮颜色
    /// </summary>
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 
    /// </summary>
    public Dictionary<string, object>? ComponentParamters { get; set; }

    /// <summary>
    /// 获得/设置 模态弹窗返回值任务实例
    /// </summary>
    internal TaskCompletionSource<DialogResult> ReturnTask { get; } = new TaskCompletionSource<DialogResult>();
}
