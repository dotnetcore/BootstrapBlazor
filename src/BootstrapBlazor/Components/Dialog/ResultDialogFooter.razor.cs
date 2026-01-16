// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ResultDialog 对话框类</para>
/// <para lang="en">ResultDialog Component</para>
/// </summary>
public partial class ResultDialogFooter
{
    /// <summary>
    /// <para lang="zh">显示确认按钮</para>
    /// <para lang="en">Show Yes Button</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowYesButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">确认按钮文本</para>
    /// <para lang="en">Yes Button Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonYesText { get; set; }

    /// <summary>
    /// <para lang="zh">确认按钮图标</para>
    /// <para lang="en">Yes Button Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ButtonYesIcon { get; set; }

    /// <summary>
    /// <para lang="zh">确认按钮颜色</para>
    /// <para lang="en">Yes Button Color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter] public Color ButtonYesColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">显示取消按钮</para>
    /// <para lang="en">Show No Button</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public bool ShowNoButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">取消按钮文本</para>
    /// <para lang="en">No Button Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonNoText { get; set; }

    /// <summary>
    /// <para lang="zh">取消按钮图标</para>
    /// <para lang="en">No Button Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ButtonNoIcon { get; set; }

    /// <summary>
    /// <para lang="zh">取消按钮颜色</para>
    /// <para lang="en">No Button Color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color ButtonNoColor { get; set; } = Color.Danger;

    /// <summary>
    /// <para lang="zh">显示关闭按钮</para>
    /// <para lang="en">Show Close Button</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public bool ShowCloseButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">关闭按钮文本</para>
    /// <para lang="en">Close Button Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseText { get; set; }

    /// <summary>
    /// <para lang="zh">关闭按钮图标</para>
    /// <para lang="en">Close Button Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public string? ButtonCloseIcon { get; set; }

    /// <summary>
    /// <para lang="zh">关闭按钮颜色</para>
    /// <para lang="en">Close Button Color</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Color ButtonCloseColor { get; set; } = Color.Secondary;

    /// <summary>
    /// <para lang="zh">获得/设置 点击关闭按钮回调方法</para>
    /// <para lang="en">Get/Set Click Close Button Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Func<Task>? OnClickClose { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击确认按钮回调方法</para>
    /// <para lang="en">Get/Set Click Yes Button Callback Method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，删除即可; Deprecated. Just delete it.")]
    [ExcludeFromCodeCoverage]
    public Func<Task>? OnClickYes { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击取消按钮回调方法</para>
    /// <para lang="en">Get/Set Click No Button Callback Method</para>
    /// <para><version>10.2.2</version></para>
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
