// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ResultDialog 对话框类
/// </summary>
public partial class ResultDialogFooter
{
    /// <summary>
    /// 显示确认按钮
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// 确认按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// 确认按钮图标
    /// </summary>
    [Parameter]
    public string? ButtonYesIcon { get; set; }

    /// <summary>
    /// 确认按钮颜色
    /// </summary>
    [Parameter] public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// 显示取消按钮
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// 取消按钮文本
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// 取消按钮图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonNoIcon { get; set; }

    /// <summary>
    /// 取消按钮颜色
    /// </summary>
    [Parameter]
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// 显示关闭按钮
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// 关闭按钮文本
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// 关闭按钮图标
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseIcon { get; set; }

    /// <summary>
    /// 关闭按钮颜色
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// 获得/设置 点击关闭按钮回调方法
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Func<Task>? OnClickClose { get; set; }

    /// <summary>
    /// 获得/设置 点击确认按钮回调方法
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Func<Task>? OnClickYes { get; set; }

    /// <summary>
    /// 获得/设置 点击取消按钮回调方法
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Func<Task>? OnClickNo { get; set; }

    [CascadingParameter(Name = "ResultDialogContext")]
    private Func<DialogResult, Task>? SetResultAsync { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ResultDialogOption>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ButtonNoText ??= Localizer[nameof(ButtonNoText)];
        ButtonYesText ??= Localizer[nameof(ButtonYesText)];

        ButtonNoIcon ??= IconTheme.GetIconByKey(ComponentIcons.ResultDialogNoIcon);
        ButtonYesIcon ??= IconTheme.GetIconByKey(ComponentIcons.ResultDialogYesIcon);
    }

    private async Task OnClick(DialogResult dialogResult)
    {
        if (SetResultAsync != null)
        {
            await SetResultAsync(dialogResult);
        }
    }
}
