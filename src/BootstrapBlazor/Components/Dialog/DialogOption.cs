// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Dialog 对话框组件</para>
/// <para lang="en">Dialog component</para>
/// </summary>
public class DialogOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 关联的 Modal 实例</para>
    /// <para lang="en">Gets or sets the related modal instance</para>
    /// </summary>
    internal Modal? Modal { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 对话框标题</para>
    /// <para lang="en">Gets or sets the dialog title</para>
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 对话框自定义样式</para>
    /// <para lang="en">Gets or sets the custom style of the dialog</para>
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 对话框尺寸</para>
    /// <para lang="en">Gets or sets the size of the dialog</para>
    /// </summary>
    public Size Size { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// <para lang="zh">获得/设置 对话框全屏尺寸 默认值为 None</para>
    /// <para lang="en">Gets or sets the full screen size of the dialog, default is None</para>
    /// </summary>
    public FullScreenSize FullScreenSize { get; set; } = FullScreenSize.None;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示最大化按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the maximize button, default is false</para>
    /// </summary>
    public bool ShowMaximizeButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否垂直居中 默认值为 true</para>
    /// <para lang="en">Gets or sets whether the dialog is vertically centered, default is true</para>
    /// </summary>
    public bool IsCentered { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 内容过长时是否滚动 默认值为 false</para>
    /// <para lang="en">Gets or sets whether the dialog content scrolls when it is too long, default is false</para>
    /// </summary>
    public bool IsScrolling { get; set; } = false;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示调整大小按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the resize button, default is false</para>
    /// </summary>
    public bool ShowResize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮 默认值为 true</para>
    /// <para lang="en">Gets or sets whether to show the close button, default is true</para>
    /// </summary>
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示标题栏关闭按钮 默认值为 true</para>
    /// <para lang="en">Gets or sets whether to show the header close button, default is true</para>
    /// </summary>
    public bool ShowHeaderCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否启用渐显动画 默认值为 null</para>
    /// <para lang="en">Gets or sets whether to enable fade animation, default is null</para>
    /// </summary>
    public bool? IsFade { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否支持通过 ESC 关闭对话框 默认值为 true</para>
    /// <para lang="en">Gets or sets whether to support closing the dialog with the ESC key, default is true</para>
    /// </summary>
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否支持点击背景关闭对话框 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to support closing the dialog by clicking the backdrop, default is false</para>
    /// </summary>
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示页脚 默认值为 true</para>
    /// <para lang="en">Gets or sets whether to show the footer, default is true</para>
    /// </summary>
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示打印按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the print button, default is false</para>
    /// </summary>
    public bool ShowPrintButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示保存按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the save button, default is false</para>
    /// </summary>
    public bool ShowSaveButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在标题栏显示打印按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the print button in the header, default is false</para>
    /// </summary>
    public bool ShowPrintButtonInHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标题栏打印按钮文本 默认值为资源文件中的 "Print"</para>
    /// <para lang="en">Gets or sets the text of the print button in the header, default is "Print" from the resource file</para>
    /// </summary>
    public string? PrintButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关联数据多用于传值</para>
    /// <para lang="en">Gets or sets the related data, mostly used for passing values</para>
    /// </summary>
    public object? BodyContext { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalBody 组件</para>
    /// <para lang="en">Gets or sets the ModalBody component</para>
    /// </summary>
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalFooter 组件</para>
    /// <para lang="en">Gets or sets the ModalFooter component</para>
    /// </summary>
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalHeader 组件模板</para>
    /// <para lang="en">Gets or sets the ModalHeader component template</para>
    /// </summary>
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalHeader 组件自定义按钮</para>
    /// <para lang="en">Gets or sets the custom buttons in the ModalHeader component</para>
    /// </summary>
    public RenderFragment? HeaderToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义组件</para>
    /// <para lang="en">Gets or sets the custom component</para>
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮图标 默认值为 null 且使用当前主题图标</para>
    /// <para lang="en">Gets or sets the icon of the save button, default is null and uses the current theme icon</para>
    /// </summary>
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮文本</para>
    /// <para lang="en">Gets or sets the text of the save button</para>
    /// </summary>
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮回调方法</para>
    /// <para lang="en">Gets or sets the callback method for the save button</para>
    /// </summary>
    public Func<Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标 默认值为 null 且使用当前主题图标</para>
    /// <para lang="en">Gets or sets the icon of the close button, default is null and uses the current theme icon</para>
    /// </summary>
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮文本</para>
    /// <para lang="en">Gets or sets the text of the close button</para>
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭对话框回调方法</para>
    /// <para lang="en">Gets or sets the callback method for closing the dialog</para>
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动关闭对话框（保存成功后） 默认值为 true</para>
    /// <para lang="en">Gets or sets whether to automatically close the dialog after saving successfully, default is true</para>
    /// </summary>
    public bool IsAutoCloseAfterSave { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许拖拽对话框 默认值为 false</para>
    /// <para lang="en">Gets or sets whether the dialog can be dragged, default is false</para>
    /// </summary>
    public bool IsDraggable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 对话框显示回调方法</para>
    /// <para lang="en">Gets or sets the callback method when the dialog is shown</para>
    /// </summary>
    public Func<Task>? OnShownAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示导出 PDF 按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the export PDF button, default is false</para>
    /// </summary>
    public bool ShowExportPdfButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在标题栏显示导出 PDF 按钮 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to show the export PDF button in the header, default is false</para>
    /// </summary>
    public bool ShowExportPdfButtonInHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 PDF 按钮配置</para>
    /// <para lang="en">Gets or sets the configuration options for the export PDF button</para>
    /// </summary>
    public ExportPdfButtonOptions? ExportPdfButtonOptions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否隐藏上一个对话框 默认值为 false</para>
    /// <para lang="en">Gets or sets whether to hide the previous dialog when opening a new one, default is false</para>
    /// </summary>
    public bool IsHidePreviousDialog { get; set; }

    /// <summary>
    /// <para lang="zh">关闭对话框方法</para>
    /// <para lang="en">Closes the dialog</para>
    /// </summary>
    public async Task CloseDialogAsync()
    {
        if (Modal != null)
        {
            await Modal.Close();
        }
    }

    /// <summary>
    /// <para lang="zh">将参数转换为组件特性</para>
    /// <para lang="en">Converts parameters to component attributes</para>
    /// </summary>
    public Dictionary<string, object> ToAttributes()
    {
        var ret = new Dictionary<string, object>
        {
            [nameof(ModalDialog.Size)] = Size,
            [nameof(ModalDialog.FullScreenSize)] = FullScreenSize,
            [nameof(ModalDialog.IsCentered)] = IsCentered,
            [nameof(ModalDialog.IsScrolling)] = IsScrolling,
            [nameof(ModalDialog.IsHidePreviousDialog)] = IsHidePreviousDialog,
            [nameof(ModalDialog.ShowCloseButton)] = ShowCloseButton,
            [nameof(ModalDialog.ShowSaveButton)] = ShowSaveButton,
            [nameof(ModalDialog.ShowHeaderCloseButton)] = ShowHeaderCloseButton,
            [nameof(ModalDialog.ShowFooter)] = ShowFooter,
            [nameof(ModalDialog.ShowResize)] = ShowResize,
            [nameof(ModalDialog.ShowPrintButton)] = ShowPrintButton,
            [nameof(ModalDialog.ShowPrintButtonInHeader)] = ShowPrintButtonInHeader,
            [nameof(ModalDialog.IsAutoCloseAfterSave)] = IsAutoCloseAfterSave,
            [nameof(ModalDialog.IsDraggable)] = IsDraggable,
            [nameof(ModalDialog.ShowMaximizeButton)] = ShowMaximizeButton,
            [nameof(ModalDialog.ShowExportPdfButton)] = ShowExportPdfButton,
            [nameof(ModalDialog.ShowExportPdfButtonInHeader)] = ShowExportPdfButtonInHeader,
        };
        if (ExportPdfButtonOptions != null)
        {
            ret.Add(nameof(ModalDialog.ExportPdfButtonOptions), ExportPdfButtonOptions);
        }
        if (!string.IsNullOrEmpty(PrintButtonText))
        {
            ret.Add(nameof(ModalDialog.PrintButtonText), PrintButtonText);
        }
        if (!string.IsNullOrEmpty(Title))
        {
            ret.Add(nameof(ModalDialog.Title), Title);
        }
        if (BodyContext != null)
        {
            ret.Add(nameof(ModalDialog.BodyContext), BodyContext);
        }
        return ret;
    }
}
