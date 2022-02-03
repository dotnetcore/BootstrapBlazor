// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class SweetAlertBody
{
    /// <summary>
    /// 获得/设置 关闭按钮文字 默认为 关闭
    /// </summary>
    [Parameter]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮文字 默认为 确认
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// 获得/设置 取消按钮文字 默认为 取消
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// 获得/设置 弹窗类别默认为 Success
    /// </summary>
    [Parameter]
    public SwalCategory Category { get; set; }

    /// <summary>
    /// 获得/设置 显示标题
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// 获得/设置 显示内容
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// 获得/设置 是否显示关闭按钮 默认显示
    /// </summary>
    [Parameter]
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示 Footer 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 是否为确认弹窗模式 默认为 false
    /// </summary>
    [Parameter]
    public bool IsConfirm { get; set; }

    /// <summary>
    /// 获得/设置 关闭按钮回调方法
    /// </summary>
    [Parameter]
    public Action? OnClose { get; set; }

    /// <summary>
    /// 获得/设置 确认按钮回调方法
    /// </summary>
    [Parameter]
    public Action? OnConfirm { get; set; }

    /// <summary>
    /// 获得/设置 显示内容模板
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Footer 模板
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SweetAlert>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    private string? IconClassString => CssBuilder.Default("swal2-icon")
        .AddClass("swal2-success swal2-animate-success-icon", Category == SwalCategory.Success)
        .AddClass("swal2-error swal2-animate-error-icon", Category == SwalCategory.Error)
        .AddClass("swal2-info", Category == SwalCategory.Information)
        .AddClass("swal2-question", Category == SwalCategory.Question)
        .AddClass("swal2-warning", Category == SwalCategory.Warning)
        .Build();

    /// <summary>
    /// 将配置信息转化为参数集合
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    internal static IDictionary<string, object?> Parse(SwalOption option) => new Dictionary<string, object?>()
    {
        [nameof(SweetAlertBody.Category)] = option.Category,
        [nameof(SweetAlertBody.ShowClose)] = option.ShowClose,
        [nameof(SweetAlertBody.IsConfirm)] = option.IsConfirm,
        [nameof(SweetAlertBody.ShowFooter)] = option.ShowFooter,
        [nameof(SweetAlertBody.OnClose)] = new Action(async () => await option.Close(false)),
        [nameof(SweetAlertBody.OnConfirm)] = new Action(async () => await option.Close(true)),
        [nameof(SweetAlertBody.Title)] = option.Title,
        [nameof(SweetAlertBody.Content)] = option.Content,
        [nameof(SweetAlertBody.BodyTemplate)] = option.BodyTemplate,
        [nameof(SweetAlertBody.FooterTemplate)] = option.FooterTemplate,
        [nameof(SweetAlertBody.ButtonTemplate)] = option.ButtonTemplate
    };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        CancelButtonText ??= Localizer[nameof(CancelButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
    }

    private Task OnClickClose()
    {
        if (OnClose != null)
        {
            OnClose.Invoke();
        }

        return Task.CompletedTask;
    }

    private Task OnClickConfirm()
    {
        if (OnConfirm != null)
        {
            OnConfirm.Invoke();
        }

        return Task.CompletedTask;
    }
}
