// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class ButtonUpload<TValue>
{
    private bool IsUploadButtonDisabled => IsDisabled || (IsSingle && UploadFiles.Any());

    private string? BrowserButtonClassString => CssBuilder.Default("btn-browser")
        .AddClass(BrowserButtonClass, !string.IsNullOrEmpty(BrowserButtonClass))
        .Build();

    private string? LoadingIconString => CssBuilder.Default("loading-icon")
        .AddClass(LoadingIcon)
        .Build();

    private string? DeleteIconString => CssBuilder.Default("delete-icon")
        .AddClass(DeleteIcon)
        .Build();

    private string? ValidStatusIconString => CssBuilder.Default("valid-icon")
        .AddClass(ValidStatusIcon)
        .Build();

    private string? InvalidStatusIconString => CssBuilder.Default("invalid-icon")
        .AddClass(InvalidStatusIcon)
        .Build();

    private string? DownloadIconString => CssBuilder.Default("download-icon")
        .AddClass(DownloadIcon)
        .Build();

    /// <summary>
    /// 获得/设置 浏览按钮图标
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 下载按钮图标
    /// </summary>
    [Parameter]
    public string? DownloadIcon { get; set; }

    /// <summary>
    /// 获得/设置 上传失败状态图标
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 上传成功状态图标
    /// </summary>
    [Parameter]
    public string? ValidStatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮图标
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// 获得/设置 浏览按钮图标
    /// </summary>
    [Parameter]
    public string? BrowserButtonIcon { get; set; }

    /// <summary>
    /// 获得/设置 上传按钮样式 默认 null 使用 Button 默认 Color Primary
    /// </summary>
    [Parameter]
    public string? BrowserButtonClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示上传列表 默认 true
    /// </summary>
    [Parameter]
    public bool ShowUploadFileList { get; set; } = true;

    /// <summary>
    /// 获得/设置 浏览按钮显示文字
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BrowserButtonText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];
        BrowserButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadBrowserButtonIcon);
        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadLoadingIcon);
        InvalidStatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadInvalidStatusIcon);
        ValidStatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadValidStatusIcon);
        DownloadIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadDownloadIcon);
        DeleteIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadDeleteIcon);
    }
}
