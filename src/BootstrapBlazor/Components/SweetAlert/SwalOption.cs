// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// SweetAlert Option 配置类
/// </summary>
public class SwalOption : PopupOptionBase
{
    /// <summary>
    /// 获得/设置 相关弹窗实例
    /// </summary>
    internal Modal? Modal { get; set; }

    /// <summary>
    /// 获得/设置 模态弹窗返回值任务实例
    /// </summary>
    internal TaskCompletionSource<bool> ReturnTask { get; } = new TaskCompletionSource<bool>();

    /// <summary>
    /// 获得/设置 是否为确认弹窗模式 此属性给模态弹窗时使用 默认为 false
    /// </summary>
    internal bool IsConfirm { get; set; }

    /// <summary>
    /// 获得/设置 提示类型 默认为 Success
    /// </summary>
    public SwalCategory Category { get; set; }

    /// <summary>
    /// 获得/设置 弹窗标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 ModalBody 组件
    /// </summary>
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Footer 组件
    /// </summary>
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认为 true 显示
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Footer 默认 false 不显示
    /// </summary>
    public bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 按钮模板
    /// </summary>
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮图标 默认 fa-solid fa-xmark
    /// </summary>
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮图标 默认 fa-solid fa-check
    /// </summary>
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮文字 默认为 关闭
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮文字 默认为 确认
    /// </summary>
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮文字 默认为 取消
    /// </summary>
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// 获得/设置 弹窗自定义样式
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 关闭弹窗回调方法
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 点击 Confirm 按钮回调方法 用于模态对话框
    /// </summary>
    public Func<Task>? OnConfirmAsync { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public SwalOption()
    {
        IsAutoHide = false;
    }

    /// <summary>
    /// 将参数转换为组件属性方法
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object> ToAttributes()
    {
        var parameters = new Dictionary<string, object>
        {
            [nameof(Size)] = Size.Medium,
            [nameof(ModalDialog.IsCentered)] = true,
            [nameof(ModalDialog.IsScrolling)] = false,
            [nameof(ModalDialog.ShowCloseButton)] = false,
            [nameof(ModalDialog.ShowHeader)] = false,
            [nameof(ModalDialog.ShowFooter)] = false
        };

        if (!string.IsNullOrEmpty(Title))
        {
            parameters.Add(nameof(ModalDialog.Title), Title);
        }

        if (!string.IsNullOrEmpty(Class))
        {
            parameters.Add(nameof(ModalDialog.Class), Class);
        }
        return parameters;
    }

    /// <summary>
    /// 关闭弹窗方法
    /// </summary>
    /// <param name="returnValue">模态弹窗返回值 默认为 true</param>
    public async Task CloseAsync(bool returnValue = true)
    {
        if (Modal != null)
        {
            await Modal.Close();
        }

        if (IsConfirm)
        {
            ReturnTask.TrySetResult(returnValue);
        }
    }
}
