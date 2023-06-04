// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class ModalDialog : IHandlerException, IDisposable
{
    private string MaximizeAriaLabel => MaximizeStatus ? "maximize" : "restore";

    /// <summary>
    /// 获得 弹窗组件样式
    /// </summary>
    private string? ClassName => CssBuilder.Default("modal-dialog")
        .AddClass("modal-dialog-centered", IsCentered && !IsDraggable)
        .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None && FullScreenSize != FullScreenSize.Always && !MaximizeStatus)
        .AddClass($"modal-{FullScreenSize.ToDescriptionString()}", FullScreenSize != FullScreenSize.None && !MaximizeStatus)
        .AddClass("modal-dialog-scrollable", IsScrolling)
        .AddClass("modal-fullscreen", MaximizeStatus)
        .AddClass("is-draggable", IsDraggable)
        .AddClass("d-none", !IsShown)
        .AddClass(Class, !string.IsNullOrEmpty(Class))
        .Build();

    /// <summary>
    /// 获得/设置 是否显示对话框
    /// </summary>
    internal bool IsShown { get; set; }

    /// <summary>
    /// 获得/设置 弹窗标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 弹窗自定义样式
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// 获得/设置 是否可以 Resize 弹窗 默认 false
    /// </summary>
    [Parameter]
    public bool ShowResize { get; set; } = true;

    /// <summary>
    /// 获得/设置 弹窗大小
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// 获得/设置 弹窗大小
    /// </summary>
    [Parameter]
    public FullScreenSize FullScreenSize { get; set; }

    /// <summary>
    /// 获得/设置 是否垂直居中 默认为 true
    /// </summary>
    [Parameter]
    public bool IsCentered { get; set; }

    /// <summary>
    /// 获得/设置 是否弹窗正文超长时滚动
    /// </summary>
    [Parameter]
    public bool IsScrolling { get; set; }

    /// <summary>
    /// 获得/设置 是否可以拖拽弹窗 默认 false 不可以拖动
    /// </summary>
    [Parameter]
    public bool IsDraggable { get; set; }

    /// <summary>
    /// 获得/设置 是否显示最大化按钮
    /// </summary>
    [Parameter]
    public bool ShowMaximizeButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示保存按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowSaveButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示打印按钮 默认为 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowPrintButton { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Header 关闭按钮
    /// </summary>
    [Parameter]
    public bool ShowHeaderCloseButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Header 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Footer 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// 获得/设置 Header 中是否显示打印按钮 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowPrintButtonInHeader { get; set; }

    /// <summary>
    /// 获得/设置 Header 中打印按钮显示文字 默认为资源文件中 打印
    /// </summary>
    [Parameter]
    public string? PrintButtonText { get; set; }

    /// <summary>
    /// 获得/设置 弹窗内容相关数据 多用于传值
    /// </summary>
    [Parameter]
    public object? BodyContext { get; set; }

    /// <summary>
    /// 获得/设置 Header 中按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderToolbarTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ModalBody 组件
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ModalFooter 组件
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 ModalHeader 组件
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮回调委托
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// 获得/设置 保存成功后是否自动关闭弹窗 默认 true 自动关闭
    /// </summary>
    [Parameter]
    public bool IsAutoCloseAfterSave { get; set; } = true;

    /// <summary>
    /// 获得/设置 关闭按钮显示文字 资源文件设置为 关闭
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮显示图标 未设置时 使用 fa-solid fa-fw fa-xmark
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮显示文字 资源文件设置为 保存
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// 获得/设置 最大化按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaximizeWindowIcon { get; set; }

    /// <summary>
    /// 获得/设置 恢复按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? RestoreWindowIcon { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveIcon { get; set; }

    /// <summary>
    /// 获得/设置 弹窗容器实例
    /// </summary>
    [CascadingParameter]
    [NotNull]
    protected Modal? Modal { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ModalDialog>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? MaximizeIconString { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ErrorLogger?.Register(this);
        Modal.AddDialog(this);
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
        PrintButtonText ??= Localizer[nameof(PrintButtonText)];

        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        MaximizeWindowIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogMaxminzeWindowIcon);
        SaveIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);
        RestoreWindowIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogRestoreWindowIcon);

        MaximizeIconString = MaximizeWindowIcon;
    }

    /// <summary>
    /// 设置 Header 文字方法
    /// </summary>
    /// <param name="text"></param>
    public void SetHeaderText(string text)
    {
        Title = text;
        StateHasChanged();
    }

    private async Task OnClickClose() => await Modal.Close();

    private bool MaximizeStatus { get; set; }

    private void OnToggleMaximize()
    {
        MaximizeStatus = !MaximizeStatus;
        MaximizeIconString = MaximizeStatus ? RestoreWindowIcon : MaximizeWindowIcon;
    }

    private async Task OnClickSave()
    {
        var ret = true;
        if (OnSaveAsync != null)
        {
            await OnSaveAsync();
        }
        if (IsAutoCloseAfterSave && ret)
        {
            await OnClickClose();
        }
    }

    private RenderFragment RenderBodyTemplate() => builder =>
    {
        builder.AddContent(0, _errorContent ?? BodyTemplate);
        _errorContent = null;
    };

    /// <summary>
    /// 上次渲染错误内容
    /// </summary>
    protected RenderFragment? _errorContent;

    /// <summary>
    /// HandlerException 错误处理方法
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="errorContent"></param>
    public virtual Task HandlerException(Exception ex, RenderFragment<Exception> errorContent)
    {
        _errorContent = errorContent(ex);
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            ErrorLogger?.UnRegister(this);
            Modal.RemoveDialog(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
