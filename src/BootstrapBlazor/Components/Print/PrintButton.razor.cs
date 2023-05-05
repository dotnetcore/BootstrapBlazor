// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// PrintButton 打印按钮
/// </summary>
public partial class PrintButton
{
    /// <summary>
    /// 获得/设置 预览模板地址 默认为空
    /// </summary>
    [Parameter]
    public string? PreviewUrl { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PrintButton>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Text ??= Localizer[nameof(Text)];
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.PrintButtonIcon);
        ButtonIcon = Icon;
    }
}
