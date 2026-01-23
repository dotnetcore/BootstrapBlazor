// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ModalDialog 组件</para>
/// <para lang="en">ModalDialog Component</para>
/// </summary>
public partial class ModalDialog
{
    private string MaximizeAriaLabel => MaximizeStatus ? "maximize" : "restore";

    /// <summary>
    /// <para lang="zh">获得 弹窗组件样式</para>
    /// <para lang="en">Get Popup Component Style</para>
    /// </summary>
    private string? ClassName => CssBuilder.Default("modal-dialog")
        .AddClass("modal-dialog-centered", IsCentered && !IsDraggable)
        .AddClass($"modal-{Size.ToDescriptionString()}", Size != Size.None && FullScreenSize != FullScreenSize.Always && !MaximizeStatus)
        .AddClass($"modal-{FullScreenSize.ToDescriptionString()}", FullScreenSize != FullScreenSize.None && !MaximizeStatus)
        .AddClass("modal-dialog-scrollable", IsScrolling)
        .AddClass("modal-fullscreen", MaximizeStatus)
        .AddClass("is-draggable", IsDraggable)
        .AddClass("is-draggable-center", IsCentered && IsDraggable && _firstRender)
        .AddClass("d-none", IsHidePreviousDialog && !IsShown)
        .AddClass(Class, !string.IsNullOrEmpty(Class))
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示对话框</para>
    /// <para lang="en">Gets or sets Whether to show dialog</para>
    /// </summary>
    internal bool IsShown { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗标题</para>
    /// <para lang="en">Gets or sets Popup Title</para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗自定义样式</para>
    /// <para lang="en">Gets or sets Popup Custom Style</para>
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可以 Resize 弹窗 默认 false</para>
    /// <para lang="en">Gets or sets Whether popup can be resized. Default false</para>
    /// </summary>
    [Parameter]
    public bool ShowResize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗大小 默认为 <see cref="Size.ExtraExtraLarge"/></para>
    /// <para lang="en">Gets or sets Popup Size. Default <see cref="Size.ExtraExtraLarge"/></para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.ExtraExtraLarge;

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗大小 默认为 <see cref="FullScreenSize.None"/></para>
    /// <para lang="en">Gets or sets Popup Full Screen Size. Default <see cref="FullScreenSize.None"/></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">为保证功能正常，设置值后 <see cref="ShowMaximizeButton"/> <seealso cref="ShowResize"/> <seealso cref="IsDraggable"/> 均不可用</para>
    /// <para lang="en">To ensure proper function, <see cref="ShowMaximizeButton"/> <seealso cref="ShowResize"/> <seealso cref="IsDraggable"/> are disabled after setting this value</para>
    /// </remarks>
    [Parameter]
    public FullScreenSize FullScreenSize { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否垂直居中 默认为 true</para>
    /// <para lang="en">Gets or sets Whether to center vertically. Default true</para>
    /// </summary>
    [Parameter]
    public bool IsCentered { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否弹窗正文超长时滚动 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to scroll when popup body is too long. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsScrolling { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否 to hide the previous dialog when opening a new one, default is false</para>
    /// <para lang="en">Gets or sets whether to hide the previous dialog when opening a new one, default is false</para>
    /// </summary>
    [Parameter]
    public bool IsHidePreviousDialog { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否可以拖拽弹窗 默认 false 不可以拖动</para>
    /// <para lang="en">Gets or sets Whether popup can be dragged. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsDraggable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示最大化按钮 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to show maximize button. Default false</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">为保证功能正常，设置值为 true 后 <seealso cref="ShowResize"/> <seealso cref="IsDraggable"/> 均不可用</para>
    /// <para lang="en">To ensure proper function, <seealso cref="ShowResize"/> <seealso cref="IsDraggable"/> are disabled after setting this value to true</para>
    /// </remarks>
    [Parameter]
    public bool ShowMaximizeButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮 默认为 true 显示</para>
    /// <para lang="en">Gets or sets Whether to show close button. Default true (Show)</para>
    /// </summary>
    [Parameter]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示保存按钮 默认为 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show save button. Default false (Not shown)</para>
    /// </summary>
    [Parameter]
    public bool ShowSaveButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示打印按钮 默认为 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show print button. Default false (Not shown)</para>
    /// </summary>
    [Parameter]
    public bool ShowPrintButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 中是否显示打印按钮 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show print button in Header. Default false (Not shown)</para>
    /// </summary>
    [Parameter]
    public bool ShowPrintButtonInHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 中打印按钮显示文字 默认为资源文件中 打印</para>
    /// <para lang="en">Gets or sets Print button text in Header. Default from resource file "Print"</para>
    /// </summary>
    [Parameter]
    public string? PrintButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 打印按钮图标 未设置 取当前图标主题下打印图标</para>
    /// <para lang="en">Gets or sets Print button icon. If not set, use print icon from current icon theme</para>
    /// </summary>
    [Parameter]
    public string? PrintButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 打印按钮颜色 默认 Color.Primary</para>
    /// <para lang="en">Gets or sets Print button color. Default Color.Primary</para>
    /// </summary>
    [Parameter]
    public Color PrintButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示导出 Pdf 按钮 默认为 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show Export PDF button. Default false (Not shown)</para>
    /// </summary>
    [Parameter]
    public bool ShowExportPdfButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 中是否显示导出 Pdf 按钮 默认 false 不显示</para>
    /// <para lang="en">Gets or sets Whether to show Export PDF button in Header. Default false (Not shown)</para>
    /// </summary>
    [Parameter]
    public bool ShowExportPdfButtonInHeader { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导出 Pdf 按钮配置项</para>
    /// <para lang="en">Gets or sets Export PDF button options</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public ExportPdfButtonOptions? ExportPdfButtonOptions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Header 关闭按钮</para>
    /// <para lang="en">Gets or sets Whether to show Header Close Button</para>
    /// </summary>
    [Parameter]
    public bool ShowHeaderCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Header 默认为 true</para>
    /// <para lang="en">Gets or sets Whether to show Header. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowHeader { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Footer 默认为 true</para>
    /// <para lang="en">Gets or sets Whether to show Footer. Default true</para>
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗内容相关数据 多用于传值</para>
    /// <para lang="en">Gets or sets Data related to popup content. Often used for passing values</para>
    /// </summary>
    [Parameter]
    public object? BodyContext { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Header 中按钮模板</para>
    /// <para lang="en">Gets or sets Button template in Header</para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderToolbarTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalBody 组件</para>
    /// <para lang="en">Gets or sets ModalBody Component</para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalFooter 组件</para>
    /// <para lang="en">Gets or sets ModalFooter Component</para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 the footer 内容 模板. 默认为 null.</para>
    /// <para lang="en">Gets or sets the footer content template. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterContentTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 ModalHeader 组件</para>
    /// <para lang="en">Gets or sets ModalHeader Component</para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮回调委托 返回 true 并且设置 <see cref="IsAutoCloseAfterSave"/> true 时自动关闭弹窗</para>
    /// <para lang="en">Gets or sets Save button callback delegate. Returns true and automatically closes popup if <see cref="IsAutoCloseAfterSave"/> is true</para>
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存成功后是否自动关闭弹窗 默认 true 自动关闭</para>
    /// <para lang="en">Gets or sets Whether to automatically close popup after successful save. Default true</para>
    /// </summary>
    [Parameter]
    public bool IsAutoCloseAfterSave { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮显示文字 资源文件设置为 关闭</para>
    /// <para lang="en">Gets or sets Close button text. Resource file set to Close</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮显示图标 未设置时 使用 fa-solid fa-fw fa-xmark</para>
    /// <para lang="en">Gets or sets Close button icon. Use fa-solid fa-fw fa-xmark if not set</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮显示文字 资源文件设置为 保存</para>
    /// <para lang="en">Gets or sets Save button text. Resource file set to Save</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮显示图标 未设置时 使用主题图标</para>
    /// <para lang="en">Gets or sets Save button icon. Use theme icon if not set</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最大化按钮图标</para>
    /// <para lang="en">Gets or sets Maximize button icon</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? MaximizeWindowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 恢复按钮图标</para>
    /// <para lang="en">Gets or sets Restore button icon</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? RestoreWindowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮图标</para>
    /// <para lang="en">Gets or sets Save button icon</para>
    /// </summary>
    [Parameter]
    [NotNull]
    [Obsolete("已弃用，请使用 SaveButtonIcon; Deprecated, please use SaveButtonIcon")]
    [ExcludeFromCodeCoverage]
    public string? SaveIcon { get => SaveButtonIcon; set => SaveButtonIcon = value; }

    /// <summary>
    /// <para lang="zh">获得/设置 模态弹窗任务 <see cref="TaskCompletionSource{TResult}"/> 实例 默认 null</para>
    /// <para lang="en">Gets or sets Modal Popup Task <see cref="TaskCompletionSource{TResult}"/> Instance. Default null</para>
    /// </summary>
    [Parameter]
    public TaskCompletionSource<DialogResult>? ResultTask { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 获得模态弹窗方法 默认 null</para>
    /// <para lang="en">Gets or sets Get Modal Popup Method. Default null</para>
    /// </summary>
    [Parameter]
    public Func<IResultDialog?>? GetResultDialog { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗容器实例</para>
    /// <para lang="en">Gets or sets Popup Container Instance</para>
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
    private DialogResult _result = DialogResult.Close;
    private bool _firstRender = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Modal.AddDialog(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ExportPdfButtonOptions ??= new()
        {
            Selector = $"#{Id} .modal-body"
        };

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
        PrintButtonText ??= Localizer[nameof(PrintButtonText)];
        ExportPdfButtonOptions.Text ??= Localizer["ExportPdfButtonText"];

        SaveButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);
        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        MaximizeWindowIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogMaximizeWindowIcon);
        RestoreWindowIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogRestoreWindowIcon);
        PrintButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.PrintButtonIcon);
        ExportPdfButtonOptions.Icon ??= IconTheme.GetIconByKey(ComponentIcons.TableExportPdfIcon);

        MaximizeIconString = MaximizeWindowIcon;

        if (FullScreenSize != FullScreenSize.None)
        {
            ShowMaximizeButton = false;
            ShowResize = false;
            IsDraggable = false;
        }
        else if (ShowMaximizeButton)
        {
            ShowResize = false;
            IsDraggable = false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (firstRender)
        {
            _firstRender = false;
        }
    }

    /// <summary>
    /// <para lang="zh">设置 Header 文字方法</para>
    /// <para lang="en">Set Header Text Method</para>
    /// </summary>
    /// <param name="text"></param>
    public void SetHeaderText(string text)
    {
        Title = text;
        StateHasChanged();
    }

    private Task SetResultAsync(DialogResult result)
    {
        _result = result;
        return Task.CompletedTask;
    }

    private async Task OnClickCloseAsync()
    {
        _result = DialogResult.Close;
        await CloseAsync();
    }

    private async Task CloseAsync()
    {
        if (GetResultDialog != null)
        {
            var dialog = GetResultDialog();
            if (dialog != null)
            {
                var result = await dialog.OnClosing(_result);
                if (result)
                {
                    await dialog.OnClose(_result);
                }
                else
                {
                    return;
                }
            }
        }
        ResultTask?.TrySetResult(_result);
        await Modal.Close();
    }

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
            ret = await OnSaveAsync();
        }
        if (IsAutoCloseAfterSave && ret)
        {
            await CloseAsync();
        }
    }

    private RenderFragment RenderBodyTemplate() => builder =>
    {
        if (ErrorLogger is not null)
        {
            builder.OpenComponent<ErrorLogger>(0);
            builder.AddAttribute(10, nameof(IErrorLogger.EnableErrorLogger), ErrorLogger.EnableErrorLogger);
            builder.AddAttribute(20, nameof(IErrorLogger.EnableILogger), ErrorLogger.EnableILogger);
            builder.AddAttribute(30, nameof(IErrorLogger.ShowToast), ErrorLogger.ShowToast);
            builder.AddAttribute(40, nameof(IErrorLogger.ToastTitle), ErrorLogger.ToastTitle);
            builder.AddAttribute(40, nameof(BootstrapBlazorErrorBoundary.ChildContent), BodyTemplate);
            builder.CloseComponent();
        }
        else
        {
            builder.AddContent(0, BodyTemplate);
        }
    };

    /// <summary>
    /// <para lang="zh">Dispose 方法</para>
    /// <para lang="en">Dispose Method</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        await base.DisposeAsync(disposing);

        if (disposing)
        {
            Modal.RemoveDialog(this);
        }
    }
}
