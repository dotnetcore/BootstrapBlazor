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
    private string? ClassString => CssBuilder.Default("btn-popover-confirm dropdown-toggle")
        .AddClass(InternalClassName, IsLink)
        .AddClass(ClassName, !IsLink)
        .Build();

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
    public string? CustomClass { get; set; }

    /// <summary>
    /// 获得/设置 PopoverConfirm 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private PopoverService? PopoverService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

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
                CustomClass = CustomClass,
                Callback = async () => await JSRuntime.InvokeVoidByIdAsync(identifier: "bb.Confirm.init", Id)
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
            IsDisabled = true;
            ButtonIcon = LoadingIcon;
            StateHasChanged();

            await Task.Run(() => InvokeAsync(OnConfirm));

            IsDisabled = false;
            ButtonIcon = Icon;
            await TrySubmit();
            StateHasChanged();
        }
        else
        {
            await OnConfirm();
            await TrySubmit();
        }
    }

    private async Task TrySubmit()
    {
        if (ButtonType == ButtonType.Submit)
        {
            await JSRuntime.InvokeVoidByIdAsync(identifier: "bb.Confirm.submit", Id);
        }
    }
}
