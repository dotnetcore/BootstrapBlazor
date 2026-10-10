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
public partial class EditDialog<TModel> where TModel : class
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
    /// <remarks>
    /// <para lang="zh">未赋值时读取资源文件 默认文本为「保存」 <see cref="ShowKeepAddingButton"/> 为 true 时默认为「保存并关闭」</para>
    /// <para lang="en">Read from resource file when not set. The default text is "Save", or "Save and Close" when <see cref="ShowKeepAddingButton"/> is true</para>
    /// </remarks>
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
    /// <para lang="zh">获得/设置 保存回调委托 返回 false 时保持编辑弹窗 返回 true 时关闭编辑弹窗</para>
    /// <para lang="en">Gets or sets Save Callback Delegate. Return false to keep edit dialog, true to close it</para>
    /// </summary>
    [Parameter]
    [EditorRequired]
    public Func<EditContext, Task<bool>>? OnSaveAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否在 Footer 中显示「保存并新增」按钮 默认为 false</para>
    /// <para lang="en">Gets or sets whether to show the "Save and Add" button in the Footer. Default is false</para>
    /// <para>v<version>11.0.1</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">点击「保存并新增」按钮时先保存当前编辑模型，保存成功后调用 <see cref="OnKeepAddingAsync"/> 创建下一条数据模型并重新绑定表单，弹窗/抽屉保持打开状态以便连续录入</para>
    /// <para lang="en">Clicking the "Save and Add" button saves the current edit model first, then calls <see cref="OnKeepAddingAsync"/> to create the next model and rebinds the form while keeping the dialog/drawer open for continuous input</para>
    /// <para lang="zh">未设置 <see cref="OnKeepAddingAsync"/> 时点击该按钮等同于点击主保存按钮 保存成功后直接关闭弹窗/抽屉</para>
    /// <para lang="en">When <see cref="OnKeepAddingAsync"/> is not set, clicking this button behaves the same as the main save button and the dialog/drawer is closed after saving successfully</para>
    /// <para lang="zh">「保存并新增」按钮位于主保存按钮之前 并作为表单默认按钮 因此在未禁用回车提交（<see cref="DisableAutoSubmitFormByEnter"/>）时 输入框内回车提交等价于点击「保存并新增」</para>
    /// <para lang="en">The "Save and Add" button is rendered before the main save button as the form default button, so pressing Enter inside an input equals clicking "Save and Add" unless Enter submission is disabled by <see cref="DisableAutoSubmitFormByEnter"/></para>
    /// </remarks>
    [Parameter]
    public bool ShowKeepAddingButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 「保存并新增」按钮文本 默认 null 读取资源文件设置文本</para>
    /// <para lang="en">Gets or sets the "Save and Add" button text. Default is null (Read from resource file)</para>
    /// <para>v<version>11.0.1</version></para>
    /// </summary>
    [Parameter]
    public string? KeepAddingButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 「保存并新增」按钮图标 默认 null 使用当前主题图标</para>
    /// <para lang="en">Gets or sets the "Save and Add" button icon. Default is null (Use current theme icon)</para>
    /// <para>v<version>11.0.1</version></para>
    /// </summary>
    [Parameter]
    public string? KeepAddingButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 「保存并新增」按钮颜色 默认 <see cref="Color.Info"/></para>
    /// <para lang="en">Gets or sets the "Save and Add" button color. Default is <see cref="Color.Info"/></para>
    /// <para>v<version>11.0.1</version></para>
    /// </summary>
    [Parameter]
    public Color KeepAddingButtonColor { get; set; } = Color.Info;

    /// <summary>
    /// <para lang="zh">获得/设置 点击「保存并新增」按钮保存成功后创建下一个编辑模型的回调方法 参数为刚刚保存成功的模型实例</para>
    /// <para lang="en">Gets or sets the callback which creates the next edit model after the "Save and Add" button saved successfully. The parameter is the model saved just now</para>
    /// <para>v<version>11.0.1</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">参数为刚刚保存成功的模型实例，如需修改请克隆或者新建实例，避免直接修改该实例以免影响已保存的数据</para>
    /// <para lang="en">The parameter is the model saved just now. Please clone it or create a new instance if you want to modify it, in case the saved data is affected</para>
    /// <para lang="zh">回调必须返回非空模型实例，返回 null 时表单内容将被清空</para>
    /// <para lang="en">The callback must return a non-null model, otherwise the form content is cleared</para>
    /// </remarks>
    [Parameter]
    public Func<TModel, Task<TModel>>? OnKeepAddingAsync { get; set; }

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

    private bool _isSubmitting;

    private bool _isDisposed;

    private bool _keepAddingRequested;

    private TModel? _parameterModel;

    private TModel _currentModel = default!;

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

        if (Model == null)
        {
            throw new InvalidOperationException($"参数 {nameof(Model)} 未赋值; {nameof(Model)} can not be null.");
        }

        if (_parameterModel != Model)
        {
            // 父层重渲染会反复下发 Model 参数 仅在 Model 真正变化时重新绑定
            // 避免把「保存并新增」已经切换的下一条模型打回旧值
            _parameterModel = Model;
            _currentModel = Model;
            _hasFieldValueChanged = false;
        }

        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        SaveButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);
        KeepAddingButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogKeepAddingButtonIcon);

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];

        // 连续新增模式下主保存按钮语义为「保存并关闭」 其它情况保持原有「保存」文本
        SaveButtonText ??= ShowKeepAddingButton ? Localizer["SaveAndCloseButtonText"] : Localizer[nameof(SaveButtonText)];

        KeepAddingButtonText ??= Localizer[nameof(KeepAddingButtonText)];

        CloseConfirmTitle ??= Localizer[nameof(CloseConfirmTitle)];
        CloseConfirmContent ??= Localizer[nameof(CloseConfirmContent)];

        if (BodyTemplate == null)
        {
            Items ??= GetItemsByColumns();
        }
    }

    private async Task<bool> OnClosingCallback()
    {
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

        return ret;
    }

    private async Task OnValidSubmitAsync(EditContext context)
    {
        if (_isDisposed || _isSubmitting || OnSaveAsync == null)
        {
            return;
        }

        // 记录本次提交是否为「保存并新增」 并立即复位防止下次提交沿用
        var keepAdding = _keepAddingRequested;
        _keepAddingRequested = false;
        _isSubmitting = true;
        try
        {
            await ToggleLoading(true);
            var save = await OnSaveAsync(context);
            await ToggleLoading(false);

            if (!save)
            {
                // 保存失败时保持弹窗打开状态
                return;
            }

            _hasFieldValueChanged = false;
            if (keepAdding && OnKeepAddingAsync != null)
            {
                _currentModel = await OnKeepAddingAsync(_currentModel);
                return;
            }

            // 非「保存并新增」提交 保存成功后关闭弹窗
            if (CloseAsync != null)
            {
                await CloseAsync();
            }
        }
        finally
        {
            _isSubmitting = false;
            if (!_isDisposed)
            {
                await InvokeAsync(StateHasChanged);
            }
        }
    }

    private Task OnInvalidSubmitAsync(EditContext context)
    {
        // 验证失败时复位「保存并新增」标记
        _keepAddingRequested = false;
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">点击「保存并新增」按钮时置位「保存并新增」标记 本次提交保存成功后将保持弹窗打开并创建下一条数据模型</para>
    /// <para lang="en">Sets the "Save and Add" flag when the "Save and Add" button is clicked. After this submission is saved successfully the dialog stays open and the next data model is created</para>
    /// </summary>
    private void OnKeepAddingClick() => _keepAddingRequested = true;

    /// <summary>
    /// <para lang="zh">点击主保存按钮时复位「保存并新增」标记 确保主保存按钮始终按「保存并关闭」处理 不受上一次点击残留标记的影响</para>
    /// <para lang="en">Resets the "Save and Add" flag when the main save button is clicked so that the main save button always behaves as "Save and Close", unaffected by a leftover flag from the previous click</para>
    /// </summary>
    private void OnSaveClick() => _keepAddingRequested = false;

    /// <summary>
    /// <para lang="zh">获得 是否显示「保存并新增」按钮 <see cref="ShowKeepAddingButton"/> 为 true 且非 Tracking 模式时为 true</para>
    /// <para lang="en">Gets whether to show the "Save and Add" button. It is true when <see cref="ShowKeepAddingButton"/> is true and <see cref="IsTracking"/> is false</para>
    /// </summary>
    private bool IsKeepAdding => ShowKeepAddingButton && !IsTracking;

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            _isDisposed = true;
            Modal?.UnRegisterOnClosingCallback(OnClosingCallback);
        }

        await base.DisposeAsync(disposing);
    }
}
