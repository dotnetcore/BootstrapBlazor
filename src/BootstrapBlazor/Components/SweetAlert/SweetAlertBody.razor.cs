// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SweetAlertBody 组件</para>
/// <para lang="en">SweetAlertBody Component</para>
/// </summary>
public partial class SweetAlertBody
{
    private string InternalCloseButtonText => IsConfirm ? CancelButtonText : CloseButtonText;

    [Inject]
    [NotNull]
    private IStringLocalizer<SweetAlert>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮文字 默认为 关闭</para>
    /// <para lang="en">Get/Set Close Button Text. Default Close</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮文字 默认为 确认</para>
    /// <para lang="en">Get/Set Confirm Button Text. Default Confirm</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 取消按钮文字 默认为 取消</para>
    /// <para lang="en">Get/Set Cancel Button Text. Default Cancel</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CancelButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 弹窗类别默认为 Success</para>
    /// <para lang="en">Get/Set Category. Default Success</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public SwalCategory Category { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示标题</para>
    /// <para lang="en">Get/Set Title</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示内容</para>
    /// <para lang="en">Get/Set Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Content { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示关闭按钮 默认 true 显示</para>
    /// <para lang="en">Get/Set Whether to show close button. Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowClose { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Footer 默认 false 不显示</para>
    /// <para lang="en">Get/Set Whether to show footer. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为确认弹窗模式 默认为 false</para>
    /// <para lang="en">Get/Set Whether is confirm dialog mode. Default false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsConfirm { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮图标</para>
    /// <para lang="en">Get/Set Close Button Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? CloseButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮图标</para>
    /// <para lang="en">Get/Set Confirm Button Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ConfirmButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 关闭按钮回调方法</para>
    /// <para lang="en">Get/Set Close Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnCloseAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 确认按钮回调方法</para>
    /// <para lang="en">Get/Set Confirm Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task>? OnConfirmAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示内容模板</para>
    /// <para lang="en">Get/Set Body Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 模板</para>
    /// <para lang="en">Get/Set Footer Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮模板</para>
    /// <para lang="en">Get/Set Button Template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ButtonTemplate { get; set; }

    [CascadingParameter]
    private Func<Task>? CloseModal { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? IconClassString => CssBuilder.Default("swal2-icon swal2-icon-show")
        .AddClass("swal2-success", Category == SwalCategory.Success)
        .AddClass("swal2-error", Category == SwalCategory.Error)
        .AddClass("swal2-info", Category == SwalCategory.Information)
        .AddClass("swal2-question", Category == SwalCategory.Question)
        .AddClass("swal2-warning", Category == SwalCategory.Warning)
        .Build();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        CancelButtonText ??= Localizer[nameof(CancelButtonText)];
        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];

        CloseButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SweetAlertCloseIcon);
        ConfirmButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.SweetAlertConfirmIcon);
    }

    private async Task OnClickClose()
    {
        if (OnCloseAsync != null)
        {
            await OnCloseAsync();
        }

        if (CloseModal != null)
        {
            await CloseModal();
        }
    }

    private async Task OnClickConfirm()
    {
        if (OnConfirmAsync != null)
        {
            await OnConfirmAsync();
        }

        if (CloseModal != null)
        {
            await CloseModal();
        }
    }
}
