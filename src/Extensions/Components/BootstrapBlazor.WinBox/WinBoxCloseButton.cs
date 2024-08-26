// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WinBox 组件关闭弹窗按钮组件
/// </summary>
public class WinBoxCloseButton : Button
{
    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public override Color Color { get; set; } = Color.Secondary;

    [CascadingParameter]
    private Func<WinBoxOption, Task>? OnCloseAsync { get; set; }

    [CascadingParameter]
    private WinBoxOption? Option { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ModalDialog>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        Text ??= Localizer[nameof(ModalDialog.CloseButtonText)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task HandlerClick()
    {
        await base.HandlerClick();

        if (OnCloseAsync != null && Option != null)
        {
            await OnCloseAsync(Option);
        }
    }
}
