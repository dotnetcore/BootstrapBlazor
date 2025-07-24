// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ButtonUpload sample code
/// </summary>
public partial class UploadButtons : IDisposable
{
    private static readonly Random Random = new();
    private static readonly long MaxFileLength = 5 * 1024 * 1024;
    private CancellationTokenSource? _token;

    private bool _isMultiple = true;
    private bool _showProgress = true;
    private bool _showUploadFileList = true;
    private bool _showDownloadButton = true;
    private bool _isDirectory = false;
    private bool _isDisabled = false;

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

    private async Task OnDownload(UploadFile item)
    {
        await ToastService.Success("文件下载", $"下载 {item.FileName} 成功");
    }

    private async Task<bool> OnDelete(UploadFile item)
    {
        await ToastService.Success("文件操作", $"删除文件 {item.FileName} 成功");
        return true;
    }

    private async Task SaveToFile(UploadFile file)
    {
        // Server Side 使用
        // Web Assembly 模式下必须使用 WebApi 方式去保存文件到服务器或者数据库中
        // 生成写入文件名称
        if (!string.IsNullOrEmpty(WebsiteOption.Value.WebRootPath))
        {
            var uploaderFolder = Path.Combine(WebsiteOption.Value.WebRootPath, "images", "uploader");
            file.FileName = $"{Path.GetFileNameWithoutExtension(file.OriginFileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(file.OriginFileName)}";
            var fileName = Path.Combine(uploaderFolder, file.FileName);

            _token ??= new CancellationTokenSource();
            try
            {
                var ret = await file.SaveToFileAsync(fileName, MaxFileLength, token: _token.Token);

                if (ret)
                {
                    // 保存成功
                    file.PrevUrl = $"{WebsiteOption.Value.AssetRootPath}images/uploader/{file.FileName}";
                }
                else
                {
                    var errorMessage = $"{Localizer["UploadsSaveFileError"]} {file.OriginFileName}";
                    file.Code = 1;
                    file.Error = errorMessage;
                    await ToastService.Error(Localizer["UploadFile"], errorMessage);
                }
            }
            catch (OperationCanceledException)
            {

            }
        }
        else
        {
            file.Code = 1;
            file.Error = Localizer["UploadsWasmError"];
            await ToastService.Information(Localizer["UploadsSaveFile"], Localizer["UploadsSaveFileMsg"]);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        _token?.Cancel();
        _token?.Dispose();
        _token = null;
        GC.SuppressFinalize(this);
    }
}
