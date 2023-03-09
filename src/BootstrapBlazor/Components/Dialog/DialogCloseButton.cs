// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗内关闭按钮组件
/// </summary>
public partial class DialogCloseButton : Button
{
    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public override Color Color { get; set; } = Color.Secondary;

    [CascadingParameter]
    private Func<Task>? OnCloseAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ModalDialog>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ButtonIcon ??= "fa-solid fa-fw fa-xmark";
        Text ??= Localizer[nameof(ModalDialog.CloseButtonText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task HandlerClick()
    {
        await base.HandlerClick();

        if (OnCloseAsync != null)
        {
            await OnCloseAsync();
        }
    }
}
