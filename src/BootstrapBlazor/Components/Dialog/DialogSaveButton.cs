// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">弹窗内保存按钮组件</para>
/// <para lang="en">Dialog Save Button Component</para>
/// </summary>
public partial class DialogSaveButton : Button
{
    [Inject]
    [NotNull]
    private IStringLocalizer<ModalDialog>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 保存回调方法 返回 true 时自动关闭弹窗</para>
    /// <para lang="en">Gets or sets Save Callback Method. Close Dialog when return true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<Task<bool>>? OnSaveAsync { get; set; }

    [CascadingParameter]
    private Func<Task>? OnCloseAsync { get; set; }

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task HandlerClick()
    {
        await base.HandlerClick();

        if (OnSaveAsync != null)
        {
            var ret = await OnSaveAsync();

            if (ret && OnCloseAsync != null)
            {
                await OnCloseAsync();
            }
        }
    }
}
