// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// CardUpload component
/// </summary>
public partial class CardUpload<TValue>
{
    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(UploadFile item) => CssBuilder.Default(ItemClassString)
        .AddClass("is-valid", item is { Uploaded: true, Code: 0 })
        .AddClass("is-invalid", item.Code != 0)
        .Build();
    private string? ItemClassString => CssBuilder.Default("upload-item")
        .AddClass("disabled", CanUpload() == false)
        .Build();

    private string? BodyClassString => CssBuilder.Default("upload-body is-card")
        .AddClass("is-single", IsMultiple == false)
        .Build();

    private string? GetDisabledString(UploadFile item) => (!IsDisabled && item is { Uploaded: true, Code: 0 }) ? null : "disabled";

    private bool ShowPreviewList => Files.Count != 0;

    private List<string?> PreviewList => Files.Select(i => i.PrevUrl).ToList();

    private string? GetDeleteButtonDisabledString(UploadFile item) => (!IsDisabled && item.Uploaded) ? null : "disabled";

    private string? CardItemClass => CssBuilder.Default("upload-item")
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? StatusIconString => CssBuilder.Default("valid-icon valid")
        .AddClass(StatusIcon)
        .Build();

    private string? DeleteIconString => CssBuilder.Default("valid-icon invalid")
        .AddClass(DeleteIcon)
        .Build();

    private string PreviewerId => $"prev_{Id}";

    /// <summary>
    /// 获得/设置 是否允许预览回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, bool>? CanPreviewCallback { get; set; }

    /// <summary>
    /// 获得/设置 图标模板
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? IconTemplate { get; set; }

    /// <summary>
    /// 获得/设置 新建图标
    /// </summary>
    [Parameter]
    public string? AddIcon { get; set; }

    /// <summary>
    /// 获得/设置 状态图标
    /// </summary>
    [Parameter]
    public string? StatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 删除图标
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// 获得/设置 移除图标
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// 获得/设置 下载图标
    /// </summary>
    [Parameter]
    public string? DownloadIcon { get; set; }

    /// <summary>
    /// 获得/设置 放大图标
    /// </summary>
    [Parameter]
    public string? ZoomIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示放大按钮 默认 true
    /// </summary>
    [Parameter]
    public bool ShowZoomButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示删除按钮 默认 true 显示
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowDeleteButton 参数。Deprecated, please use the ShowDeleteButton parameter")]
    [ExcludeFromCodeCoverage]
    public bool ShowDeletedButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示删除按钮 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 继续上传按钮是否在列表前 默认 false
    /// </summary>
    [Parameter]
    public bool IsUploadButtonAtFirst { get; set; }

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
    /// 获得/设置 点击取消按钮回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnCancel { get; set; }

    /// <summary>
    /// 获得/设置 点击 Zoom 图标回调方法
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnZoomAsync { get; set; }

    /// <summary>
    /// 获得/设置 取消图标
    /// </summary>
    [Parameter]
    public string? CancelIcon { get; set; }

    /// <summary>
    /// 获得/设置 图标文件扩展名集合 ".png"
    /// </summary>
    [Parameter]
    public List<string>? AllowExtensions { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconExcel { get; set; }

    /// <summary>
    /// 获得/设置 Docx 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconDocx { get; set; }

    /// <summary>
    /// 获得/设置 PPT 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconPPT { get; set; }

    /// <summary>
    /// 获得/设置 Audio 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconAudio { get; set; }

    /// <summary>
    /// 获得/设置 Video 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconVideo { get; set; }

    /// <summary>
    /// 获得/设置 File 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconCode { get; set; }

    /// <summary>
    /// 获得/设置 Pdf 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconPdf { get; set; }

    /// <summary>
    /// 获得/设置 Zip 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconZip { get; set; }

    /// <summary>
    /// 获得/设置 Archive 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconArchive { get; set; }

    /// <summary>
    /// 获得/设置 Image 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconImage { get; set; }

    /// <summary>
    /// 获得/设置 File 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconFile { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? ActionClassString => CssBuilder.Default("upload-item-actions")
        .AddClass("btn-browser", IsDisabled == false)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AddIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadAddIcon);
        StatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadStatusIcon);
        DeleteIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadDeleteIcon);
        RemoveIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadRemoveIcon);
        DownloadIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadDownloadIcon);
        ZoomIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadZoomIcon);

        CancelIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadCancelIcon);
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

    private async Task OnCardFileDelete(UploadFile item)
    {
        await OnFileDelete(item);
        StateHasChanged();
    }

    private async Task OnClickZoom(UploadFile item)
    {
        if (OnZoomAsync != null)
        {
            await OnZoomAsync(item);
        }
    }

    private async Task OnClickDownload(UploadFile item)
    {
        if (OnDownload != null)
        {
            await OnDownload(item);
        }
    }

    private async Task OnClickCancel(UploadFile item)
    {
        if (OnCancel != null)
        {
            await OnCancel(item);
        }
    }
}
