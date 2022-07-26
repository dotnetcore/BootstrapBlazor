// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class PopConfirmButton
{
    private string? PopButtonClassName => IsLink ? InternalClassName : ClassName;

    private string? InternalClassName => CssBuilder.Default()
        .AddClass($"link-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public override Color Color { get; set; } = Color.None;

    /// <summary>
    /// 获得/设置 自定义样式 默认 null
    /// </summary>
    /// <remarks>由 data-bs-custom-class 实现</remarks>
    [Parameter]
    public string? CssClass { get; set; }

    /// <summary>
    /// 获得/设置 PopoverConfirm 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private PopoverService? PopoverService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    private bool Submit { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        Content ??= Localizer[nameof(Content)];
    }

    /// <summary>
    /// OnAfterRenderAsync 方法
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (Submit)
        {
            Submit = false;
            await JSRuntime.InvokeVoidAsync(Id, "bb_confirm_submit");
        }
    }

    /// <summary>
    /// 显示确认弹窗方法
    /// </summary>
    private async Task Show()
    {
        // 回调消费者逻辑 判断是否需要弹出确认框
        if (await OnBeforeClick())
        {
            // 生成客户端弹窗
            await PopoverService.Show(new PopoverConfirmOption()
            {
                ButtonId = Id,
                Title = Title,
                Content = Content,
                CloseButtonText = CloseButtonText,
                CloseButtonColor = CloseButtonColor,
                ConfirmButtonText = ConfirmButtonText,
                ConfirmButtonColor = ConfirmButtonColor,
                Icon = ConfirmIcon,
                OnConfirm = Confirm,
                OnClose = OnClose,
                CssClass = CssClass,
                Callback = async () => await JSRuntime.InvokeVoidAsync(Id, "bb_confirm")
            });
        }
    }

    /// <summary>
    /// 确认回调方法
    /// </summary>
    /// <returns></returns>
    private async Task Confirm()
    {
        if (IsAsync)
        {
            var icon = Icon;
            IsDisabled = true;
            Icon = LoadingIcon;
            StateHasChanged();

            await Task.Run(() => InvokeAsync(OnConfirm));

            IsDisabled = false;
            Icon = icon;

            if (ButtonType == ButtonType.Submit)
            {
                Submit = true;
            }
            StateHasChanged();
        }
        else
        {
            await OnConfirm();
            if (ButtonType == ButtonType.Submit)
            {
                Submit = true;
                StateHasChanged();
            }
        }
    }
}
