// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">PopConfirmButton 组件</para>
/// <para lang="en">PopConfirmButton component</para>
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
    /// <para lang="zh">获得/设置 按钮颜色</para>
    /// <para lang="en">Gets or sets the button color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public override Color Color { get; set; } = Color.None;

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    private bool _renderTooltip;

    /// <summary>
    /// <para lang="zh">OnParametersSet 方法</para>
    /// <para lang="en">OnParametersSet method</para>
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
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    public override Task ShowTooltip() => Task.CompletedTask;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    public override Task RemoveTooltip() => Task.CompletedTask;

    private string? ConfirmString => OnBeforeClick != null ? "true" : null;

    private string? TriggerCloseString => OnClose != null ? "true" : null;

    /// <summary>
    /// <para lang="zh">显示确认弹窗方法</para>
    /// <para lang="en">Show confirm popup method</para>
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
    /// <para lang="zh">确认回调方法</para>
    /// <para lang="en">Confirm callback method</para>
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
                IsDisabled = IsKeepDisabled;
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
