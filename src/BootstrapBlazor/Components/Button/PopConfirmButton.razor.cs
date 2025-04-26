// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// PopConfirmButton 组件
/// </summary>
public partial class PopConfirmButton
{
    private string? ClassString => CssBuilder.Default("pop-confirm")
        .AddClass("disabled", IsDisabled)
        .AddClass(InternalClassName, IsLink)
        .AddClass(ClassName, !IsLink)
        .Build();

    private string? InternalClassName => CssBuilder.Default()
        .AddClass($"link-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string TagName => IsLink ? "a" : "div";

    private string? CustomClassString => CssBuilder.Default(CustomClass)
        .AddClass("shadow", ShowShadow)
        .Build();

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public override Color Color { get; set; } = Color.None;

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    private bool _renderTooltip;

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        Content ??= Localizer[nameof(Content)];

        _renderTooltip = Tooltip == null && !string.IsNullOrEmpty(TooltipText);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override Task ShowTooltip() => Task.CompletedTask;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override Task RemoveTooltip() => Task.CompletedTask;

    private string? ConfirmString => OnBeforeClick != null ? "true" : null;

    private string? TriggerCloseString => OnClose != null ? "true" : null;

    /// <summary>
    /// 显示确认弹窗方法
    /// </summary>
    private async Task Show()
    {
        // 回调消费者逻辑 判断是否需要弹出确认框
        var show = true;
        if (OnBeforeClick != null)
        {
            show = await OnBeforeClick();
        }
        if (show)
        {
            await InvokeVoidAsync("showConfirm", Id);
        }
    }

    /// <summary>
    /// 确认回调方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClickConfirm()
    {
        if (IsAsync)
        {
            IsDisabled = true;
            IsAsyncLoading = true;
            StateHasChanged();

            if (OnConfirm != null)
            {
                await OnConfirm();
            }

            if (ButtonType == ButtonType.Submit)
            {
                await TrySubmit();
            }
            else
            {
                IsDisabled = false;
                IsAsyncLoading = false;
                StateHasChanged();
            }
        }
        else
        {
            if (OnConfirm != null)
            {
                await OnConfirm();
            }
            if (ButtonType == ButtonType.Submit)
            {
                await TrySubmit();
            }
        }
    }

    private async Task TrySubmit()
    {
        await InvokeVoidAsync("submit", Id);
    }
}
