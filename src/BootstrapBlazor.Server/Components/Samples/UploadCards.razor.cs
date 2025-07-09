// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CardUpload sample code
/// </summary>
public partial class UploadCards : IDisposable
{
    private bool _isMultiple = true;
    private bool _isDirectory = false;
    private bool _isDisabled = false;
    private bool _isUploadButtonAtFirst = false;
    private bool _showProgress = true;
    private bool _showZoomButton = true;
    private bool _showDeleteButton = true;
    private bool _showDownloadButton = true;

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

    private List<UploadFile> Base64FormatFileList { get; } =
    [
        new() { FileName = "Test", PrevUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACoAAAAkCAYAAAD/yagrAAAE60lEQVR4AWJwL/AB9GIWvI0jURz/L1NomZmZP8p9kWNmhnIbKPdomZmZGcJgO7QsWMbce55Yl11t47FbVdKTYpjOr/+H4y4Z/MpYeJVV8KVvokFRylq9osKfVtGQ/NHyPl2CbNVGwau2oOlmgUDJtDJGzxtvvIA3vRH+7IgeA0VtYhQBtKPlToFgC6RY58aggfwLNKhr0Zry2NnPrpIM2YGW2+UBG1IEmdGVJEVXE+hQXt8joKhLjdGVZHd7TSD9DHmT3J1dheroSF4fhnvUVQwbZx9UOnHSzQjkTN0tIG+8hFdbxet4PQOG4WqJwN1hFVb+xUBmCblxvxRkIE+Q+Yf0T31NSrp4/RV4FhHgoSTchQRZBK4D1+Fe2q2g8CYXU6buQyNDaiZKZhn0IYXHVwbkRQydHyawOAGGisa/o3DvI/jF3QKKiuAy+LKs5CvaXMLdeXb35wbkKTjmReDZHysCBosWJqN7r0jZ/VfgXtIlUNQnVpK7D4nNJSC5BHGi1d108Ppr8MxlF0cJqBSyFJaUfUnvHLyO4cttgaI+NhGB7HE0MaSUks/gU5vwe1gv5tfhmUmAhxME8jbIUtiEDus+fhmDJlgCRY0ylZRZhybKWtNinmYlH9NvL5puO3m9UHOkgzb3xuF5HCkDyhYiSwrYVfQPTpYChTc+E35tG9VJBjGFFNmtVlNMDnjzbx0EBpKq1eTeh2awbArBRuHadBWu6WVB4U/NITfuobYoCZl7QL//wDtr+nTmsgzQh2Kwgtz7QAZWFeGw/RI8s94Kigp1PvzZg2i9x11FDtKn/oZCoZdZxhaAXmG4/ohJKHudLE1Gyu66DOfs10BRR5CU3TKQIrtzj9BAkF991dsMslTZEClLsI+jFmANZYFAdAJl9QG03jWH9GrcFh9QTP4Of6GfJGSpsv1J2aoEKRuWCANNJNqOFAaPAW36rVRMCjVf6ZCtqYF2p6Asxg5mWK6tQamY9RCs8z1wF+FxTR5UqYT/3GC7oCkMHcjxKguq6cnl+Egf2whip3C9Yu56jk+GXXOtv1XIa8L1f3AFkHe9az83AmNanwV/bhfa5JKJ3n1slCUL8dk7CNfvVFMfySRThoxbK7fh18tTZXI2QeySLk88IRGsbHkqQj6wUJ72G5CloCXKZrdbLPgVMgVfQMq5m97bfQWOOeVbaK02nSA2ofn2y662UJE47horLZTe38oDjdxQUhmawocxa0OJ5hXjnZE4w1y0uc/iULKGfk+xNuZxI/BnjhFs+THPq4phmTsbeYPXRjFsJJ+NSMlnInHkxrwzGDjR3uBcG1/B/b/TwZkhubb6tP2oVfTAD2G4kw9vYiA2h+zy4GwYxd7S4qGOgd6oqVn6re1DTXiO4e4wPF+yQlF4ZCBfheSPIjJn+ciS/w93qjA6aZYqeQ5D3dTqvuE6GZNTkov5vqsYvLh7j8u1sWWkLIdBQR+q+XdtaKGhJCWBDpkwgSw9gRpKyoNKw6rjCLCDP4yVflSgWFwt3C0HSUo2BTFYX28fVAaWPpDx7wtwjOSSUszawnUT0KTudlfrFQwZ3WNf867CNZQhk/C8kIFUhJLt/O3Jzn62IYNwrUvA8zws4W61CGlDSfugXMz5pJgiSFYyXMYiRXdH4GmyqaR9UHLxzxG41KAwpZxRPN4kRf85Z5I4MvYfFUFGfemJG40AAAAASUVORK5CYII=" },
    ];

    private static long MaxFileLength => 5 * 1024 * 1024;
    private CancellationTokenSource? _token;

    private async Task OnCardUpload(UploadFile file)
    {
        if (file is { File: not null })
        {
            // 服务器端验证当文件大于 5MB 时提示文件太大信息
            if (file.Size > MaxFileLength)
            {
                await ToastService.Information(Localizer["UploadsFileTitle"], Localizer["UploadsFileError"]);
                file.Code = 1;
                file.Error = Localizer["UploadsFileError"];
            }
            else
            {
                // 模拟保存成功
                await Task.Delay(100);
                await SaveToFile(file);
                await ToastService.Success(Localizer["UploadsFileTitle"], $"{file.File!.Name} {Localizer["UploadsSuccess"]}");
            }
        }
    }

    private async Task SaveToFile(UploadFile file)
    {
        // Server Side 使用
        // Web Assembly 模式下必须使用 WebApi 方式去保存文件到服务器或者数据库中
        // 生成写入文件名称
        if (!string.IsNullOrEmpty(WebsiteOption.CurrentValue.WebRootPath))
        {
            var uploaderFolder = Path.Combine(WebsiteOption.CurrentValue.WebRootPath, "images", "uploader");
            file.FileName = $"{Path.GetFileNameWithoutExtension(file.OriginFileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(file.OriginFileName)}";
            var fileName = Path.Combine(uploaderFolder, file.FileName);

            _token ??= new CancellationTokenSource();
            try
            {
                var ret = await file.SaveToFileAsync(fileName, MaxFileLength, token: _token.Token);

                if (ret)
                {
                    // 保存成功
                    file.PrevUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/uploader/{file.FileName}";
                }
                else
                {
                    var errorMessage = Localizer["UploadsSaveFileError"];
                    file.Code = 1;
                    file.Error = errorMessage;
                    await ToastService.Error(Localizer["UploadsFileTitle"], errorMessage);
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
            await ToastService.Error(Localizer["UploadsFileTitle"], Localizer["UploadsWasmError"]);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        if (_token != null)
        {
            _token.Cancel();
            _token.Dispose();
            _token = null;
        }
        GC.SuppressFinalize(this);
    }
}
