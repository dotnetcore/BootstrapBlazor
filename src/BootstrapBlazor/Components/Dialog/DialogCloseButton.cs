// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">弹窗内关闭按钮组件</para>
/// <para lang="en">Dialog Close Button Component</para>
/// </summary>
public partial class DialogCloseButton : Button
{
    /// <summary>
    /// <para lang="zh">获得/设置 按钮颜色</para>
    /// <para lang="en">Get/Set Button Color</para>
    /// <para><version>10.2.2</version></para>
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
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.DialogCloseButtonIcon);
        Text ??= Localizer[nameof(ModalDialog.CloseButtonText)];

        base.OnParametersSet();
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
