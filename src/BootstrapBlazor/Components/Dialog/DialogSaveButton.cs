// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 弹窗内保存按钮组件
/// </summary>
public partial class DialogSaveButton : Button
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ModalDialog>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DialogSaveButtonIcon);
        Text ??= Localizer[nameof(ModalDialog.SaveButtonText)];
        ButtonType = ButtonType.Submit;
    }
}
