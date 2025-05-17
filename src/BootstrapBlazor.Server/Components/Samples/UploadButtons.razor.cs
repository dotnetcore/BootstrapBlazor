// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ButtonUpload sample code
/// </summary>
public partial class UploadButtons
{
    private static readonly Random Random = new();
    private CancellationTokenSource? ReadToken { get; set; }
    private static long MaxFileLength => 5 * 1024 * 1024;

    private bool _isMultiple = true;
    private bool _showProgress = true;
    private bool _showUploadFileList = true;
    private bool _showDownloadButton = true;
    private bool _isDirectory = false;
    private bool _isDisabled = false;

    private List<UploadFile> DefaultFormatFileList { get; } =
    [
        new() { FileName = "Test.xls" },
        new() { FileName = "Test.doc" },
        new() { FileName = "Test.ppt" },
        new() { FileName = "Test.mp3" },
        new() { FileName = "Test.mp4" },
        new() { FileName = "Test.pdf" },
        new() { FileName = "Test.cs" },
        new() { FileName = "Test.zip" },
        new() { FileName = "Test.txt" },
        new() { FileName = "Test.dat" }
    ];

    private async Task OnClickToUpload(UploadFile file)
    {
        // 示例代码，模拟 80% 几率保存成功
        var error = Random.Next(1, 100) > 80;
        if (error)
        {
            file.Code = 1;
            file.Error = Localizer["UploadsError"];
        }
        else
        {
            await SaveToFile(file);
        }
    }

    private async Task OnClickToUploadNoUploadList(UploadFile file)
    {
        await ToastService.Success("Upload", $"{file.OriginFileName} uploaded success.");
    }

    private async Task OnUploadFolder(UploadFile file)
    {
        // 上传文件夹时会多次回调此方法
        await SaveToFile(file);
    }

    private async Task OnDownload(UploadFile item)
    {
        await ToastService.Success("文件下载", $"下载 {item.FileName} 成功");
    }

    private async Task SaveToFile(UploadFile file)
    {
        // Server Side 使用
        // Web Assembly 模式下必须使用 WebApi 方式去保存文件到服务器或者数据库中
        // 生成写入文件名称
        if (!string.IsNullOrEmpty(WebsiteOption.CurrentValue.WebRootPath))
        {
            var uploaderFolder = Path.Combine(WebsiteOption.CurrentValue.WebRootPath,
                $"images{Path.DirectorySeparatorChar}uploader");
            file.FileName =
                $"{Path.GetFileNameWithoutExtension(file.OriginFileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(file.OriginFileName)}";
            var fileName = Path.Combine(uploaderFolder, file.FileName);

            ReadToken ??= new CancellationTokenSource();
            var ret = await file.SaveToFileAsync(fileName, MaxFileLength, ReadToken.Token);

            if (ret)
            {
                // 保存成功
                file.PrevUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/uploader/{file.FileName}";
            }
            else
            {
                var errorMessage = $"{Localizer["UploadsSaveFileError"]} {file.OriginFileName}";
                file.Code = 1;
                file.Error = errorMessage;
                await ToastService.Error(Localizer["UploadFile"], errorMessage);
            }
        }
        else
        {
            file.Code = 1;
            file.Error = Localizer["UploadsWasmError"];
            await ToastService.Information(Localizer["UploadsSaveFile"], Localizer["UploadsSaveFileMsg"]);
        }
    }

    private List<AttributeItem> GetAttributes() =>
    [
        new()
        {
            Name = "IsDirectory",
            Description = Localizer["UploadsIsDirectory"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsMultiple",
            Description = Localizer["UploadsIsMultiple"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowProgress",
            Description = Localizer["UploadsShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowUploadFileList",
            Description = Localizer["UploadsShowUploadFileList"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowDownloadButton",
            Description = Localizer["UploadsShowDeleteButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
