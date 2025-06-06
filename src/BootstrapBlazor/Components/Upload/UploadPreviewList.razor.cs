﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// UploadPreviewList component
/// </summary>
public partial class UploadPreviewList
{
    /// <summary>
    /// Gets or sets the collection of files to be uploaded.
    /// </summary>
    [Parameter]
    [NotNull]
    public List<UploadFile>? Items { get; set; }

    /// <summary>
    /// Gets or sets the disable status of the upload list.
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether progress should be displayed during the operation.
    /// </summary>
    [Parameter]
    public bool ShowProgress { get; set; }

    /// <summary>
    /// Gets or sets the upload file format callback method.
    /// </summary>
    [Parameter]
    public Func<string?, string>? OnGetFileFormat { get; set; }

    /// <summary>
    /// <para>Gets or sets the callback method for the cancel button click event. Default is null</para>
    /// <para>获得/设置 点击取消按钮回调此方法 默认 null</para>
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnCancel { get; set; }

    /// <summary>
    /// 获得/设置 取消图标
    /// </summary>
    [Parameter]
    public string? CancelIcon { get; set; }

    /// <summary>
    /// <para>获得/设置 浏览按钮图标</para>
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
    /// 获得/设置 点击删除按钮时回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task<bool>>? OnDelete { get; set; }

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

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ItemClassString => CssBuilder.Default("upload-item")
         .AddClass("disabled", IsDisabled)
         .Build();

    private string? GetItemClassString(UploadFile item) => CssBuilder.Default(ItemClassString)
        .AddClass("is-valid", item is { Uploaded: true, Code: 0 })
        .AddClass("is-invalid", item.Code != 0)
        .Build();

    private string? LoadingIconString => CssBuilder.Default("loading-icon")
        .AddClass(LoadingIcon)
        .Build();

    private string? CancelIconString => CssBuilder.Default("cancel-icon")
        .AddClass(CancelIcon)
        .Build();

    private string? DownloadIconString => CssBuilder.Default("download-icon")
        .AddClass(DownloadIcon)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        LoadingIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadLoadingIcon);
        InvalidStatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadInvalidStatusIcon);
        ValidStatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadValidStatusIcon);

        FileIconExcel ??= IconTheme.GetIconByKey(ComponentIcons.FileIconExcel);
        FileIconDocx ??= IconTheme.GetIconByKey(ComponentIcons.FileIconDocx);
        FileIconPPT ??= IconTheme.GetIconByKey(ComponentIcons.FileIconPPT);
        FileIconAudio ??= IconTheme.GetIconByKey(ComponentIcons.FileIconAudio);
        FileIconVideo ??= IconTheme.GetIconByKey(ComponentIcons.FileIconVideo);
        FileIconCode ??= IconTheme.GetIconByKey(ComponentIcons.FileIconCode);
        FileIconPdf ??= IconTheme.GetIconByKey(ComponentIcons.FileIconPdf);
        FileIconZip ??= IconTheme.GetIconByKey(ComponentIcons.FileIconZip);
        FileIconArchive ??= IconTheme.GetIconByKey(ComponentIcons.FileIconArchive);
        FileIconImage ??= IconTheme.GetIconByKey(ComponentIcons.FileIconImage);
        FileIconFile ??= IconTheme.GetIconByKey(ComponentIcons.FileIconFile);
    }

    private async Task OnClickDownload(UploadFile item)
    {
        if (OnDownload != null)
        {
            await OnDownload(item);
        }
    }

    private async Task OnFileDelete(UploadFile item)
    {
        if (OnDelete != null)
        {
            await OnDelete(item);
        }
    }

    private bool GetShowProgress(UploadFile item) => ShowProgress && !item.Uploaded;

    private async Task OnClickCancel(UploadFile item)
    {
        if (OnCancel != null)
        {
            await OnCancel(item);
        }
    }

    private string? GetFileFormatClassString(UploadFile item)
    {
        var builder = CssBuilder.Default("file-icon");
        var fileExtension = Path.GetExtension(item.GetFileName());
        if (!string.IsNullOrEmpty(fileExtension))
        {
            fileExtension = fileExtension.ToLowerInvariant();
            var icon = OnGetFileFormat?.Invoke(fileExtension) ?? GetFileExtensions(fileExtension);
            builder.AddClass(icon);
        }
        return builder.Build();
    }

    private string? GetFileExtensions(string fileExtension) => fileExtension switch
    {
        ".csv" or ".xls" or ".xlsx" => FileIconExcel,
        ".doc" or ".docx" or ".dot" or ".dotx" => FileIconDocx,
        ".ppt" or ".pptx" => FileIconPPT,
        ".wav" or ".mp3" => FileIconAudio,
        ".mp4" or ".mov" or ".mkv" => FileIconVideo,
        ".cs" or ".html" or ".vb" => FileIconCode,
        ".pdf" => FileIconPdf,
        ".zip" or ".rar" or ".iso" => FileIconZip,
        ".txt" or ".log" => FileIconArchive,
        ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" => FileIconImage,
        _ => FileIconFile
    };
}
