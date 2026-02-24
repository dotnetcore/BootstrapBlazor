// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">编辑弹窗组件</para>
/// <para lang="en">Edit Dialog Component</para>
/// </summary>
public partial class EditDialog<TModel>
{
    /// <summary>
    /// <para lang="zh">获得/设置 查询时是否显示正在加载中动画 默认为 false</para>
    /// <para lang="en">Gets or sets Whether to Show Loading Animation When Querying. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false</para>
    /// <para lang="en">Gets or sets Whether Component Uses Tracking Mode to Update Editing Items Directly. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsTracking { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 实体类编辑模式 Add 还是 Update</para>
    /// <para lang="en">Gets or sets Item Changed Type (Add or Update)</para>
    /// </summary>
    [Parameter]
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮图标</para>
    /// <para lang="en">Gets or sets Save Button Icon</para>
    /// </summary>
    [Parameter]
    public string? SaveButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存按钮文本</para>
    /// <para lang="en">Gets or sets Save Button Text</para>
    /// </summary>
    [Parameter]
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存回调委托 返回 false 时保持编辑弹窗 返回 true 时关闭编辑弹窗</para>
    /// <para lang="en">Gets or sets Save Callback Delegate. Return false to keep edit dialog, true to close it</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public Func<EditContext, Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标</para>
    /// <para lang="en">Gets or sets Close Button Icon</para>
    /// </summary>
    [Parameter]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮文本</para>
    /// <para lang="en">Gets or sets Close Button Text</para>
    /// </summary>
    [Parameter]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭弹窗回调方法</para>
    /// <para lang="en">Gets or sets Close Dialog Callback Method</para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Whether to Disable Auto Submit Form By Enter. Default is null</para>
    /// </summary>
    [Parameter]
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 标签宽度 默认为 120 </para>
    /// <para lang="en">Gets or sets Label Width. Default is 120</para>
    /// </summary>
    [Parameter]
    public int? LabelWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 DialogFooterTemplate 实例</para>
    /// <para lang="en">Gets or sets DialogFooterTemplate Instance</para>
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭弹窗确认弹窗。默认为 null 使用全局配置设置值 <see cref="BootstrapBlazorOptions.EditDialogSettings"/></para>
    /// <para lang="en">Gets or sets whether to show the close confirm dialog. Default is null to use global configuration <see cref="BootstrapBlazorOptions.EditDialogSettings"/></para>
    /// </summary>
    [Parameter]
    public bool? ShowConfirmCloseSwal { get; set; }

    [CascadingParameter]
    private Func<Task>? CloseAsync { get; set; }

    [CascadingParameter]
    private Modal? Modal { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<EditDialog<TModel>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    [Inject, NotNull]
    private SwalService? SwalService { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private bool _hasFieldValueChanged;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Modal?.RegisterOnClosingCallback(OnClosingCallback);
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet Method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        SaveButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
    }

    private async Task<bool> OnClosingCallback()
    {
        var ret = true;
        if (BootstrapBlazorOptions.Value.GetEditDialogShowConfirmSwal(ShowConfirmCloseSwal, _hasFieldValueChanged))
        {
            var op = new SwalOption()
            {
                Title = Localizer["CloseConfirmTitle"],
                Content = Localizer["CloseConfirmContent"],
                Category = SwalCategory.Question,
            };
            ret = await SwalService.ShowModal(op);
        }

        return ret;
    }

    private async Task OnValidSubmitAsync(EditContext context)
    {
        if (OnSaveAsync != null)
        {
            await ToggleLoading(true);
            var save = await OnSaveAsync(context);
            await ToggleLoading(false);

            if (save)
            {
                _hasFieldValueChanged = false;
            }

            if (save && CloseAsync != null)
            {
                await CloseAsync();
            }
        }
    }

    private void OnFieldValueChanged(string fieldName, object? value)
    {
        _hasFieldValueChanged = true;
    }

    /// <summary>
    /// <para lang="zh">显示/隐藏 Loading 遮罩</para>
    /// <para lang="en">Show/Hide Loading Mask</para>
    /// </summary>
    /// <param name="state"><para lang="zh">true 时显示，false 时隐藏</para><para lang="en">true to show, false to hide</para></param>
    public async ValueTask ToggleLoading(bool state)
    {
        if (ShowLoading)
        {
            await InvokeVoidAsync("execute", Id, state);
        }
    }

    private RenderFragment RenderFooter => builder =>
    {
        if (FooterTemplate != null)
        {
            builder.AddContent(1, FooterTemplate(Model));
        }
        else
        {
            if (!IsTracking)
            {
                builder.OpenComponent<DialogCloseButton>(20);
                builder.AddAttribute(21, nameof(Button.Icon), CloseButtonIcon);
                builder.AddAttribute(22, nameof(Button.Text), CloseButtonText);
                builder.AddAttribute(23, nameof(Button.OnClickWithoutRender), OnCloseAsync);
                builder.CloseComponent();
            }
            builder.OpenComponent<Button>(30);
            builder.AddAttribute(31, nameof(Button.Color), Color.Primary);
            builder.AddAttribute(32, nameof(Button.Icon), SaveButtonIcon);
            builder.AddAttribute(33, nameof(Button.Text), SaveButtonText);
            builder.AddAttribute(34, nameof(Button.ButtonType), ButtonType.Submit);
            builder.AddAttribute(35, nameof(Button.IsAsync), true);
            builder.CloseComponent();
        }
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            Modal?.UnRegisterOnClosingCallback(OnClosingCallback);
        }

        await base.DisposeAsync(disposing);
    }
}
