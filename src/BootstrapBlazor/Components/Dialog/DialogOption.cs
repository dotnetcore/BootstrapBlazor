// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Dialog 组件配置类
/// </summary>
public class DialogOption
{
    /// <summary>
    /// 获得/设置 相关弹窗实例
    /// </summary>
    internal Modal? Modal { get; set; }

    /// <summary>
    /// 获得/设置 弹窗标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 弹窗自定义样式
    /// </summary>
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 弹窗大小
    /// </summary>
    public Size Size { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// 获得/设置 全屏弹窗 默认 None
    /// </summary>
    public FullScreenSize FullScreenSize { get; set; } = FullScreenSize.None;

    /// <summary>
    /// 获得/设置 是否显示最大化按钮 默认 false 不显示
    /// </summary>
    public bool ShowMaximizeButton { get; set; }

    /// <summary>
    /// 获得/设置 是否垂直居中 默认为 true
    /// </summary>
    public bool IsCentered { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否弹窗正文超长时滚动 默认为 false
    /// </summary>
    public bool IsScrolling { get; set; } = false;

    /// <summary>
    /// 获得/设置 是否显示调整大小按钮 默认为 false
    /// </summary>
    public bool ShowResize { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认为 true
    /// </summary>
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Header 关闭按钮 默认为 true
    /// </summary>
    public bool ShowHeaderCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否支持键盘 ESC 关闭当前弹窗 默认 true 支持
    /// </summary>
    public bool IsKeyboard { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否支持点击遮罩关闭弹窗 默认 false
    /// </summary>
    public bool IsBackdrop { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Footer 默认为 true
    /// </summary>
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示打印按钮 默认 false 不显示
    /// </summary>
    public bool ShowPrintButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示保存按钮 默认 false 不显示
    /// </summary>
    public bool ShowSaveButton { get; set; }

    /// <summary>
    /// 获得/设置 打印按钮是否显示在 Header 中 默认 false 不显示
    /// </summary>
    public bool ShowPrintButtonInHeader { get; set; }

    /// <summary>
    /// 获得/设置 Header 中打印按钮显示文字 默认为资源文件中 打印 
    /// </summary>
    public string? PrintButtonText { get; set; }

    /// <summary>
    /// 获得/设置 相关连数据，多用于传值使用
    /// </summary>
    public object? BodyContext { get; set; }

    /// <summary>
    /// 获得/设置 ModalBody 组件
    /// </summary>
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ModalFooter 组件
    /// </summary>
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ModalHeader 组件模板
    /// </summary>
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ModalHeader 组件自定义按钮
    /// </summary>
    public RenderFragment? HeaderToolbarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 自定义组件
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }

    /// <summary>
    /// 获得/设置 关闭弹窗回调方法
    /// </summary>
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮回调方法
    /// </summary>
    public Func<Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮文本
    /// </summary>
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 查询按钮文本
    /// </summary>
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// 获得/设置 保存成功后是否自动关闭弹窗 默认 true 自动关闭
    /// </summary>
    public bool IsAutoCloseAfterSave { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否可以拖拽弹窗 默认 false 不可以拖动
    /// </summary>
    public bool IsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 弹窗已显示时回调此方法
    /// </summary>
    public Func<Task>? OnShownAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否显示导出 Pdf 按钮 默认为 false 不显示
    /// </summary>
    public bool ShowExportPdfButton { get; set; }

    /// <summary>
    /// 获得/设置 Header 中是否显示导出 Pdf 按钮 默认 false 不显示
    /// </summary>
    public bool ShowExportPdfButtonInHeader { get; set; }

    /// <summary>
    /// 获得/设置 导出 Pdf 按钮配置项
    /// </summary>
    public ExportPdfButtonOptions? ExportPdfButtonOptions { get; set; }

    /// <summary>
    /// 关闭弹窗方法
    /// </summary>
    public async Task CloseDialogAsync()
    {
        if (Modal != null)
        {
            await Modal.Close();
        }
    }

    /// <summary>
    /// 将参数转换为组件属性方法
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, object> ToAttributes()
    {
        var ret = new Dictionary<string, object>
        {
            [nameof(ModalDialog.Size)] = Size,
            [nameof(ModalDialog.FullScreenSize)] = FullScreenSize,
            [nameof(ModalDialog.IsCentered)] = IsCentered,
            [nameof(ModalDialog.IsScrolling)] = IsScrolling,
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
