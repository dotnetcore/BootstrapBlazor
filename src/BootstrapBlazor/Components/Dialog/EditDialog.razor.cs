// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 编辑弹窗组件
/// </summary>
public partial class EditDialog<TModel>
{
    /// <summary>
    /// 获得/设置 保存回调委托
    /// </summary>
    [Parameter]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public Func<EditContext, Task>? OnSaveAsync { get; set; }

    /// <summary>
    /// 获得/设置 获得/设置 重置按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 查询时是否显示正在加载中动画 默认为 false
    /// </summary>
    [Parameter]
    public bool ShowLoading { get; set; }

    /// <summary>
    /// 获得/设置 组件是否采用 Tracking 模式对编辑项进行直接更新 默认 false
    /// </summary>
    [Parameter]
    public bool IsTracking { get; set; }

    /// <summary>
    /// 获得/设置 实体类编辑模式 Add 还是 Update
    /// </summary>
    [Parameter]
    public ItemChangedType ItemChangedType { get; set; }

    /// <summary>
    /// 获得/设置 查询按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? SaveButtonText { get; set; }

    /// <summary>
    /// 获得/设置 关闭弹窗回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用表单内回车自动提交功能 默认 null 未设置
    /// </summary>
    [Parameter]
    public bool? DisableAutoSubmitFormByEnter { get; set; }

    /// <summary>
    /// 获得/设置 DialogFooterTemplate 实例
    /// </summary>
    [Parameter]
    public RenderFragment<TModel>? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 保存按钮图标
    /// </summary>
    [Parameter]
    public string? SaveButtonIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<EditDialog<TModel>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SaveButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        SaveButtonText ??= Localizer[nameof(SaveButtonText)];
    }

    private async Task OnValidSubmitAsync(EditContext context)
    {
        if (OnSaveAsync != null)
        {
            await ToggleLoading(true);
            await OnSaveAsync(context);
            await ToggleLoading(false);
        }
    }

    /// <summary>
    /// 显示/隐藏 Loading 遮罩
    /// </summary>
    /// <param name="state">true 时显示，false 时隐藏</param>
    /// <returns></returns>
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
                builder.AddAttribute(21, nameof(Button.Text), CloseButtonText);
                builder.AddAttribute(22, nameof(Button.OnClickWithoutRender), OnCloseAsync);
                builder.CloseComponent();
            }
            builder.OpenComponent<Button>(30);
            builder.AddAttribute(31, nameof(Button.Color), Color.Primary);
            builder.AddAttribute(32, nameof(Button.Icon), SaveButtonIcon);
            builder.AddAttribute(33, nameof(Button.Text), SaveButtonText);
            builder.AddAttribute(34, nameof(Button.ButtonType), ButtonType.Submit);
            builder.CloseComponent();
        }
    };
}
