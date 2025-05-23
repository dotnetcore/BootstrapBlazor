// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 按钮上传组件
/// <para>ButtonUpload Component</para>
/// </summary>
public partial class ButtonUpload<TValue>
{
    /// <summary>
    /// 获得/设置 浏览按钮加载中图标
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

    /// <summary>
    /// 获得/设置 浏览按钮颜色
    /// </summary>
    [Parameter]
    public Color BrowserButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 Size 大小
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 子组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 设置文件格式图标回调委托
    /// </summary>
    [Parameter]
    public Func<string?, string>? OnGetFileFormat { get; set; }

    /// <summary>
    /// 获得/设置 是否显示下载按钮 默认 false
    /// </summary>
    [Parameter]
    public bool ShowDownloadButton { get; set; }

    /// <summary>
    /// 获得/设置 点击下载按钮回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnDownload { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconExcel { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconDocx { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconPPT { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconAudio { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconVideo { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconCode { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconPdf { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconZip { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconArchive { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconImage { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconFile { get; set; }

    /// <summary>
    /// 获得/设置 取消图标
    /// </summary>
    [Parameter]
    public string? CancelIcon { get; set; }

    /// <summary>
    /// 获得/设置 点击取消按钮回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnCancel { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? BrowserButtonClassString => CssBuilder.Default("btn-browser")
        .AddClass(BrowserButtonClass, !string.IsNullOrEmpty(BrowserButtonClass))
        .Build();

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];
        BrowserButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadBrowserButtonIcon);
    }

    private bool CheckStatus()
    {
        if (IsDisabled)
        {
            return true;
        }

        // 允许多上传
        if (IsMultiple)
        {
            return MaxFileCount.HasValue && Files.Count >= MaxFileCount;
        }

        // 只允许单个上传
        return Files.Count > 0;
    }
}
