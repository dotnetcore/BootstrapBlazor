// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

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
    /// <para><version>10.3.3</version></para>
    /// </summary>
    [Parameter]
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭确认弹窗标题</para>
    /// <para lang="en">Gets or sets Close Confirm Dialog Title</para>
    /// <para><version>10.3.3</version></para>
    /// </summary>
    [Parameter]
    public string? CloseConfirmTitle { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭确认弹窗内容</para>
    /// <para lang="en">Gets or sets Close Confirm Dialog Content</para>
    /// <para><version>10.3.3</version></para>
    /// </summary>
    [Parameter]
    public string? CloseConfirmContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存回调委托 返回 false 时保持编辑弹窗，返回 true 时根据 <see cref="KeepOpenAfterSave"/> 决定是否关闭编辑弹窗。开始关闭或组件释放后忽略延迟完成验证的提交</para>
    /// <para lang="en">Gets or sets Save Callback Delegate. Return false to keep the edit dialog, or true to determine whether to close it based on <see cref="KeepOpenAfterSave"/>. Submissions whose validation completes after closing starts or component disposal are ignored</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public Func<EditContext, Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存成功后是否保持编辑弹窗打开，默认为 false</para>
    /// <para lang="en">Gets or sets whether to keep the edit dialog open after a successful save. Default is false</para>
    /// </summary>
    [Parameter]
    public bool KeepOpenAfterSave { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存成功后获取下一个编辑模型的异步回调方法。初始化失败时显示错误并阻止重复保存当前模型</para>
    /// <para lang="en">Gets or sets the async callback that provides the next edit model after a successful save. Initialization failures are displayed and prevent saving the current model again</para>
    /// </summary>
    [Parameter]
    public Func<Task<TModel>>? CreateNextModelAsync { get; set; }

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
    /// <para><version>10.3.3</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowCloseConfirm { get; set; }

    [CascadingParameter]
    private Func<Task>? CloseAsync { get; set; }

    [CascadingParameter]
    private Modal? Modal { get; set; }

    [CascadingParameter]
    private ModalDialog? ModalDialog { get; set; }

    [CascadingParameter]
    private Drawer? Drawer { get; set; }

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

    [Inject, NotNull]
    private ILogger<EditDialog<TModel>>? Logger { get; set; }

    private bool _hasFieldValueChanged;
    private bool _isBusy;
    private bool _isDisposed;
    private string? _saveError;
    private TModel? _parameterModel;
    private object? _parameterPresentation;
    private TModel _currentModel = default!;
    private ValidateForm _validateForm = default!;

    internal bool IsBusy => _isBusy;

    private DialogCloseContext? CloseContext => Drawer?.CloseContext ?? ModalDialog?.CloseContext ?? Modal?.CloseContext;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CloseContext?.Register(this, OnClosingCallback, () => _isBusy);
    }

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet Method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Model == null)
        {
            throw new InvalidOperationException($"参数 {nameof(Model)} 未赋值; {nameof(Model)} can not be null.");
        }

        var modelChanged = !IsSameModel(_parameterModel, Model);
        var presentation = CloseContext?.Presentation;
        if (modelChanged || !ReferenceEquals(_parameterPresentation, presentation))
        {
            _parameterModel = Model;
            _parameterPresentation = presentation;
            _currentModel = Model;
            _hasFieldValueChanged = false;
            _saveError = null;
        }

        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        SaveButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];

        CloseConfirmTitle ??= Localizer[nameof(CloseConfirmTitle)];
        CloseConfirmContent ??= Localizer[nameof(CloseConfirmContent)];

        if (BodyTemplate == null)
        {
            Items ??= GetItemsByColumns();
        }
    }

    private async Task<bool> OnClosingCallback()
    {
        if (_isBusy)
        {
            return false;
        }

        var ret = true;
        if (BootstrapBlazorOptions.Value.GetEditDialogShowConfirmSwal(ShowCloseConfirm, _hasFieldValueChanged))
        {
            var op = new SwalOption()
            {
                Title = CloseConfirmTitle,
                Content = CloseConfirmContent,
                Category = SwalCategory.Question,
            };
            ret = await SwalService.ShowModal(op);
        }

        return ret && !_isBusy;
    }

    /// <summary>
    /// <para lang="zh">检查编辑弹窗是否允许关闭，保存或切换模型期间不允许关闭</para>
    /// <para lang="en">Checks whether the edit dialog can close. Closing is blocked while saving or switching models</para>
    /// </summary>
    public Task<bool> CanCloseAsync() => OnClosingCallback();

    private static bool IsSameModel(TModel? first, TModel? second) => typeof(TModel).IsValueType
        ? EqualityComparer<TModel>.Default.Equals(first, second)
        : ReferenceEquals(first, second);

    private Func<EditContext, Task> GetSubmitCallback()
    {
        var presentation = CloseContext?.Presentation;
        return context => OnValidSubmitAsync(context, presentation);
    }

    private async Task OnValidSubmitAsync(EditContext context, object? presentation)
    {
        if (_isDisposed || CloseContext?.IsClosing == true
            || !ReferenceEquals(presentation, CloseContext?.Presentation))
        {
            Logger.LogWarning("Ignoring a submission from a closing or disposed edit dialog.");
            return;
        }

        var onSaveAsync = OnSaveAsync;
        if (_isBusy || _saveError != null || onSaveAsync == null)
        {
            return;
        }

        if (!ReferenceEquals(context.Model, _validateForm.Model))
        {
            Logger.LogWarning("Ignoring a submission from an obsolete edit model.");
            return;
        }

        var close = false;
        var parameterModel = _parameterModel;
        bool IsCurrent() => !_isDisposed && CloseContext?.IsClosing != true
            && ReferenceEquals(presentation, CloseContext?.Presentation)
            && IsSameModel(parameterModel, _parameterModel);
        var saveState = new EditDialogSaveState();
        context.Properties[typeof(EditDialogSaveState)] = saveState;
        _isBusy = true;
        try
        {
            await InvokeAsync(StateHasChanged);
            close = await SaveAsync(context, saveState, onSaveAsync, IsCurrent);
        }
        catch (Exception exception) when (saveState.IsSaved)
        {
            if (IsCurrent())
            {
                _hasFieldValueChanged = false;
                _saveError = Localizer["SavePostProcessingError"];
            }
            Logger.LogError(exception, "The current item was saved, but completing the edit operation failed.");
            throw;
        }
        finally
        {
            context.Properties.Remove(typeof(EditDialogSaveState));
            _isBusy = false;
            await InvokeAsync(StateHasChanged);
        }

        if (close && CloseAsync != null)
        {
            await CloseAsync();
        }
    }

    private async Task<bool> SaveAsync(EditContext context, EditDialogSaveState saveState,
        Func<EditContext, Task<bool>> onSaveAsync, Func<bool> isCurrent)
    {
        if (!CheckCurrentSave(isCurrent))
        {
            return false;
        }
        var close = false;
        try
        {
            await ToggleLoading(true);
            if (!CheckCurrentSave(isCurrent))
            {
                return false;
            }
            if (await onSaveAsync(context))
            {
                saveState.IsSaved = true;
                if (!CheckCurrentSave(isCurrent))
                {
                    return false;
                }
                _hasFieldValueChanged = false;
                if (KeepOpenAfterSave && CreateNextModelAsync != null)
                {
                    await InitializeNextModelAsync(CreateNextModelAsync, isCurrent);
                }
                else
                {
                    close = !KeepOpenAfterSave;
                }
            }
        }
        finally
        {
            await ToggleLoading(false);
        }
        return close;
    }

    private bool CheckCurrentSave(Func<bool> isCurrent)
    {
        var current = isCurrent();
        if (!current)
        {
            Logger.LogWarning("Ignoring the continuation of an obsolete or disposed edit operation.");
        }
        return current;
    }

    private async Task InitializeNextModelAsync(Func<Task<TModel>> createModelAsync, Func<bool> isCurrent)
    {
        try
        {
            var model = await createModelAsync();
            if (model == null)
            {
                throw new InvalidOperationException($"{nameof(CreateNextModelAsync)} must return a non-null model.");
            }
            if (CheckCurrentSave(isCurrent))
            {
                _currentModel = model;
            }
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "The current item was saved, but initializing the next edit model failed.");
            if (isCurrent())
            {
                _saveError = Localizer["NextModelInitializationError"];
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
            builder.AddContent(1, FooterTemplate(_currentModel));
        }
        else
        {
            if (!IsTracking)
            {
                builder.OpenComponent<DialogCloseButton>(20);
                builder.AddAttribute(21, nameof(Button.Icon), CloseButtonIcon);
                builder.AddAttribute(22, nameof(Button.Text), CloseButtonText);
                builder.AddAttribute(23, nameof(Button.OnClickWithoutRender), OnCloseAsync);
                builder.AddAttribute(24, nameof(Button.IsDisabled), _isBusy);
                builder.CloseComponent();
            }
            builder.OpenComponent<Button>(30);
            builder.AddAttribute(31, nameof(Button.Color), Color.Primary);
            builder.AddAttribute(32, nameof(Button.Icon), SaveButtonIcon);
            builder.AddAttribute(33, nameof(Button.Text), SaveButtonText);
            builder.AddAttribute(34, nameof(Button.ButtonType), ButtonType.Submit);
            builder.AddAttribute(35, nameof(Button.IsAsync), true);
            builder.AddAttribute(36, nameof(Button.IsDisabled), _isBusy || _saveError != null);
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
            _isDisposed = true;
            CloseContext?.UnRegister(this);
        }

        await base.DisposeAsync(disposing);
    }
}

internal sealed class EditDialogSaveState
{
    internal bool IsSaved { get; set; }

    internal static void SetSaved(EditContext context, bool saved)
    {
        if (context.Properties.TryGetValue(typeof(EditDialogSaveState), out var value)
            && value is EditDialogSaveState state)
        {
            state.IsSaved = saved;
        }
    }
}
