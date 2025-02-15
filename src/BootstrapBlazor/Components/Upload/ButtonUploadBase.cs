// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Components;

/// <summary>
/// 按钮上传组件基类
/// </summary>
public abstract class ButtonUploadBase<TValue> : SingleUploadBase<TValue>
{
    /// <summary>
    /// 获得/设置 是否上传整个目录 默认为 false
    /// </summary>
    [Parameter]
    public bool IsDirectory { get; set; }

    /// <summary>
    /// 获得/设置 是否允许多文件上传 默认 false 不允许
    /// </summary>
    /// <remarks>多选文件时，所有文件处理完毕后，会额外触发一次 <see cref="OnAllFileUploaded"/> 回调</remarks>
    [Parameter]
    public bool IsMultiple { get; set; }

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

    /// <summary>
    /// 获得/设置 所有文件上传完毕回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<IReadOnlyCollection<UploadFile>, Task>? OnAllFileUploaded { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 上传文件夹时 开启 Multiple 属性
        if (IsDirectory)
        {
            IsMultiple = true;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

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
        CancelIcon ??= IconTheme.GetIconByKey(ComponentIcons.UploadCancelIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override async Task OnFileChange(InputFileChangeEventArgs args)
    {
        if (IsMultiple)
        {
            var items = args.GetMultipleFiles(args.FileCount).Select(f => new UploadFile()
            {
                OriginFileName = f.Name,
                Size = f.Size,
                File = f,
                FileCount = args.FileCount,
                Uploaded = OnChange == null,
                UpdateCallback = Update
            }).ToList();
            UploadFiles.AddRange(items);
            if (OnChange != null)
            {
                foreach (var item in items)
                {
                    await OnChange(item);
                    item.Uploaded = true;
                    StateHasChanged();
                }
            }
            if (OnAllFileUploaded != null)
            {
                await OnAllFileUploaded(UploadFiles);
            }
        }
        else
        {
            var file = new UploadFile()
            {
                OriginFileName = args.File.Name,
                Size = args.File.Size,
                File = args.File,
                Uploaded = false,
                UpdateCallback = Update
            };
            UploadFiles.Add(file);
            if (OnChange != null)
            {
                await OnChange(file);
            }
            file.Uploaded = true;
        }

        //触发 ValueChange，以支持 bind-value
        await base.OnFileChange(args);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected string? GetFileFormatClassString(UploadFile item)
    {
        var builder = CssBuilder.Default("file-icon");
        var fileExtension = Path.GetExtension(item.OriginFileName ?? item.FileName);
        if (!string.IsNullOrEmpty(fileExtension))
        {
            fileExtension = fileExtension.ToLowerInvariant();
        }
        var icon = OnGetFileFormat?.Invoke(fileExtension) ?? GetFileExtensions();
        builder.AddClass(icon);
        return builder.Build();

        // switch 关键字导致无法 100% 覆盖
        [ExcludeFromCodeCoverage]
        string? GetFileExtensions() => fileExtension switch
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

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override IDictionary<string, object> GetUploadAdditionalAttributes()
    {
        var ret = base.GetUploadAdditionalAttributes();

        if (IsMultiple)
        {
            ret.Add("multiple", "multiple");
        }

        if (IsDirectory)
        {
            ret.Add("directory", "dicrectory");
            ret.Add("webkitdirectory", "webkitdirectory");
        }
        return ret;
    }

    /// <summary>
    /// 点击下载按钮回调此方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected async Task OnClickDownload(UploadFile item)
    {
        if (OnDownload != null)
        {
            await OnDownload(item);
        }
    }

    /// <summary>
    /// 点击取消按钮回调此方法
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected async Task OnClickCancel(UploadFile item)
    {
        if (OnCancel != null)
        {
            await OnCancel(item);
        }
    }
}
