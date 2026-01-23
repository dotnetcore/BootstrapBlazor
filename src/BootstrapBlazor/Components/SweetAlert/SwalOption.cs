// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SweetAlert Option 配置类</para>
/// <para lang="en">SweetAlert Option Configuration Class</para>
/// </summary>
public class SwalOption : PopupOptionBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 相关弹窗实例</para>
    /// <para lang="en">Gets or sets Modal Instance</para>
    /// </summary>
    internal Modal? Modal { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 模态弹窗任务上下文</para>
    /// <para lang="en">Gets or sets Modal Task Context</para>
    /// </summary>
    [NotNull]
    internal SweetContext? ConfirmContext { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为确认弹窗模式 此属性给模态弹窗时使用 默认为 false</para>
    /// <para lang="en">Gets or sets Whether is confirm dialog mode. Used for modal dialog. Default false</para>
    /// </summary>
    public bool IsConfirm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 提示类型 默认为 Success</para>
    /// <para lang="en">Gets or sets Category. Default Success</para>
    /// </summary>
    public SwalCategory Category { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗标题</para>
    /// <para lang="en">Gets or sets Title</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalBody 组件</para>
    /// <para lang="en">Gets or sets ModalBody Component</para>
    /// </summary>
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 组件</para>
    /// <para lang="en">Gets or sets Footer Component</para>
    /// </summary>
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮 默认为 true 显示</para>
    /// <para lang="en">Gets or sets Whether to show close button. Default true</para>
    /// </summary>
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Footer 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show footer. Default false</para>
    /// </summary>
    public bool ShowFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮模板</para>
    /// <para lang="en">Gets or sets Button Template</para>
    /// </summary>
    public RenderFragment? ButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标 默认 fa-solid fa-xmark</para>
    /// <para lang="en">Gets or sets Close Button Icon. Default fa-solid fa-xmark</para>
    /// </summary>
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮图标 默认 fa-solid fa-check</para>
    /// <para lang="en">Gets or sets Confirm Button Icon. Default fa-solid fa-check</para>
    /// </summary>
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮文字 默认为 关闭</para>
    /// <para lang="en">Gets or sets Close Button Text. Default Close</para>
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮文字 默认为 确认</para>
    /// <para lang="en">Gets or sets Confirm Button Text. Default Confirm</para>
    /// </summary>
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮文字 默认为 取消</para>
    /// <para lang="en">Gets or sets Cancel Button Text. Default Cancel</para>
    /// </summary>
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗自定义样式</para>
    /// <para lang="en">Gets or sets Custom Class</para>
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭弹窗回调方法</para>
    /// <para lang="en">Gets or sets Callback method when closing</para>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击 Confirm 按钮回调方法 用于模态对话框</para>
    /// <para lang="en">Gets or sets Callback method when clicking Confirm button. Used for modal dialog</para>
    /// </summary>
    public Func<Task>? OnConfirmAsync { get; set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public SwalOption()
    {
        IsAutoHide = false;
    }

    /// <summary>
    /// <para lang="zh">将参数转换为组件属性方法</para>
    /// <para lang="en">Convert parameters to component attributes method</para>
    /// </summary>
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
    /// <para lang="zh">关闭弹窗方法</para>
    /// <para lang="en">Close Dialog Method</para>
    /// </summary>
    /// <param name="returnValue">
    /// <para lang="zh">模态弹窗返回值 默认为 true</para>
    /// <para lang="en">Modal dialog return value. Default true</para>
    /// </param>
    public async Task CloseAsync(bool returnValue = true)
    {
        if (Modal != null)
        {
            await Modal.Close();
        }

        if (IsConfirm)
        {
            ConfirmContext.Value = returnValue;
        }
    }
}
