// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Uploads
/// </summary>
public sealed partial class Uploads
{
    [NotNull]
    private ConsoleLogger? Logger1 { get; set; }

    [NotNull]
    private ConsoleLogger? Logger2 { get; set; }

    private static readonly Random random = new();

    private CancellationTokenSource? ReadToken { get; set; }

    private static long MaxFileLength => 5 * 1024 * 1024;

    private Person Foo1 { get; set; } = new Person();

    private Person Foo2 { get; set; } = new Person();

    private List<UploadFile> PreviewFileList { get; } = [];

    private CancellationTokenSource? ReadAvatarToken { get; set; }

    private List<UploadFile> DefaultFormatFileList { get; } =
    [
       new UploadFile { FileName = "Test.xls" },
       new UploadFile { FileName = "Test.doc" },
       new UploadFile { FileName = "Test.ppt" },
       new UploadFile { FileName = "Test.mp3" },
       new UploadFile { FileName = "Test.mp4" },
       new UploadFile { FileName = "Test.pdf" },
       new UploadFile { FileName = "Test.cs" },
       new UploadFile { FileName = "Test.zip" },
       new UploadFile { FileName = "Test.txt" },
       new UploadFile { FileName = "Test.dat" }
    ];

    private List<UploadFile> Base64FormatFileList { get; } =
    [
        new UploadFile { FileName = "Test", PrevUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACoAAAAkCAYAAAD/yagrAAAE60lEQVR4AWJwL/AB9GIWvI0jURz/L1NomZmZP8p9kWNmhnIbKPdomZmZGcJgO7QsWMbce55Yl11t47FbVdKTYpjOr/+H4y4Z/MpYeJVV8KVvokFRylq9osKfVtGQ/NHyPl2CbNVGwau2oOlmgUDJtDJGzxtvvIA3vRH+7IgeA0VtYhQBtKPlToFgC6RY58aggfwLNKhr0Zry2NnPrpIM2YGW2+UBG1IEmdGVJEVXE+hQXt8joKhLjdGVZHd7TSD9DHmT3J1dheroSF4fhnvUVQwbZx9UOnHSzQjkTN0tIG+8hFdbxet4PQOG4WqJwN1hFVb+xUBmCblxvxRkIE+Q+Yf0T31NSrp4/RV4FhHgoSTchQRZBK4D1+Fe2q2g8CYXU6buQyNDaiZKZhn0IYXHVwbkRQydHyawOAGGisa/o3DvI/jF3QKKiuAy+LKs5CvaXMLdeXb35wbkKTjmReDZHysCBosWJqN7r0jZ/VfgXtIlUNQnVpK7D4nNJSC5BHGi1d108Ppr8MxlF0cJqBSyFJaUfUnvHLyO4cttgaI+NhGB7HE0MaSUks/gU5vwe1gv5tfhmUmAhxME8jbIUtiEDus+fhmDJlgCRY0ylZRZhybKWtNinmYlH9NvL5puO3m9UHOkgzb3xuF5HCkDyhYiSwrYVfQPTpYChTc+E35tG9VJBjGFFNmtVlNMDnjzbx0EBpKq1eTeh2awbArBRuHadBWu6WVB4U/NITfuobYoCZl7QL//wDtr+nTmsgzQh2Kwgtz7QAZWFeGw/RI8s94Kigp1PvzZg2i9x11FDtKn/oZCoZdZxhaAXmG4/ohJKHudLE1Gyu66DOfs10BRR5CU3TKQIrtzj9BAkF991dsMslTZEClLsI+jFmANZYFAdAJl9QG03jWH9GrcFh9QTP4Of6GfJGSpsv1J2aoEKRuWCANNJNqOFAaPAW36rVRMCjVf6ZCtqYF2p6Asxg5mWK6tQamY9RCs8z1wF+FxTR5UqYT/3GC7oCkMHcjxKguq6cnl+Egf2whip3C9Yu56jk+GXXOtv1XIa8L1f3AFkHe9az83AmNanwV/bhfa5JKJ3n1slCUL8dk7CNfvVFMfySRThoxbK7fh18tTZXI2QeySLk88IRGsbHkqQj6wUJ72G5CloCXKZrdbLPgVMgVfQMq5m97bfQWOOeVbaK02nSA2ofn2y662UJE47horLZTe38oDjdxQUhmawocxa0OJ5hXjnZE4w1y0uc/iULKGfk+xNuZxI/BnjhFs+THPq4phmTsbeYPXRjFsJJ+NSMlnInHkxrwzGDjR3uBcG1/B/b/TwZkhubb6tP2oVfTAD2G4kw9vYiA2h+zy4GwYxd7S4qGOgd6oqVn6re1DTXiO4e4wPF+yQlF4ZCBfheSPIjJn+ciS/w93qjA6aZYqeQ5D3dTqvuE6GZNTkov5vqsYvLh7j8u1sWWkLIdBQR+q+XdtaKGhJCWBDpkwgSw9gRpKyoNKw6rjCLCDP4yVflSgWFwt3C0HSUo2BTFYX28fVAaWPpDx7wtwjOSSUszawnUT0KTudlfrFQwZ3WNf867CNZQhk/C8kIFUhJLt/O3Jzn62IYNwrUvA8zws4W61CGlDSfugXMz5pJgiSFYyXMYiRXdH4GmyqaR9UHLxzxG41KAwpZxRPN4kRf85Z5I4MvYfFUFGfemJG40AAAAASUVORK5CYII=" },
    ];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PreviewFileList.AddRange(new[]
        {
            new UploadFile { PrevUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Argo.png" }
        });
    }

    private Task OnFileChange(UploadFile file)
    {
        // 未真正保存文件
        // file.SaveToFile()
        Logger1.Log($"{file.File!.Name} {Localizer["UploadsSuccess"]}");
        return Task.CompletedTask;
    }

    private Task<bool> OnFileDelete(UploadFile item)
    {
        Logger1.Log($"{item.OriginFileName} {Localizer["UploadsRemoveMsg"]}");
        return Task.FromResult(true);
    }

    private static Task OnSubmit(EditContext context)
    {
        // 示例代码请根据业务情况自行更改
        // var fileName = Foo.Picture?.Name;
        return Task.CompletedTask;
    }

    private async Task OnClickToUpload(UploadFile file)
    {
        // 示例代码，模拟 80% 几率保存成功
        var error = random.Next(1, 100) > 80;
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

    private async Task<bool> SaveToFile(UploadFile file)
    {
        // Server Side 使用
        // Web Assembly 模式下必须使用 WebApi 方式去保存文件到服务器或者数据库中
        // 生成写入文件名称
        var ret = false;
        if (!string.IsNullOrEmpty(WebsiteOption.CurrentValue.WebRootPath))
        {
            var uploaderFolder = Path.Combine(WebsiteOption.CurrentValue.WebRootPath, $"images{Path.DirectorySeparatorChar}uploader");
            file.FileName = $"{Path.GetFileNameWithoutExtension(file.OriginFileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(file.OriginFileName)}";
            var fileName = Path.Combine(uploaderFolder, file.FileName);

            ReadToken ??= new CancellationTokenSource();
            ret = await file.SaveToFileAsync(fileName, MaxFileLength, ReadToken.Token);

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
        return ret;
    }

    private async Task OnDownload(UploadFile item)
    {
        await ToastService.Success("文件下载", $"下载 {item.FileName} 成功");
    }

    private async Task OnUploadFolder(UploadFile file)
    {
        // 上传文件夹时会多次回调此方法
        await SaveToFile(file);
    }

    private async Task OnAvatarUpload(UploadFile file)
    {
        // 示例代码，使用 base64 格式
        if (file != null && file.File != null)
        {
            var format = file.File.ContentType;
            if (CheckValidAvatarFormat(format))
            {
                ReadAvatarToken ??= new CancellationTokenSource();
                if (ReadAvatarToken.IsCancellationRequested)
                {
                    ReadAvatarToken.Dispose();
                    ReadAvatarToken = new CancellationTokenSource();
                }

                await file.RequestBase64ImageFileAsync(format, 640, 480, MaxFileLength, ReadAvatarToken.Token);
            }
            else
            {
                file.Code = 1;
                file.Error = Localizer["UploadsFormatError"];
            }

            if (file.Code != 0)
            {
                await ToastService.Error(Localizer["UploadsAvatarMsg"], $"{file.Error} {format}");
            }
        }
    }

    private static bool CheckValidAvatarFormat(string format)
    {
        return "jpg;png;bmp;gif;jpeg".Split(';').Any(f => format.Contains(f, StringComparison.OrdinalIgnoreCase));
    }

    private Task OnAvatarValidSubmit(EditContext context)
    {
        Logger2.Log(Foo2.Picture?.Name ?? "");
        return Task.CompletedTask;
    }

    private async Task OnCardUpload(UploadFile file)
    {
        if (file != null && file.File != null)
        {
            // 服务器端验证当文件大于 5MB 时提示文件太大信息
            if (file.Size > MaxFileLength)
            {
                await ToastService.Information(Localizer["UploadsFileMsg"], Localizer["UploadsFileError"]);
                file.Code = 1;
                file.Error = Localizer["UploadsFileError"];
            }
            else
            {
                await SaveToFile(file);
            }
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        ReadToken?.Cancel();
        ReadAvatarToken?.Cancel();
        GC.SuppressFinalize(this);
    }

    private List<AttributeItem> GetInputAttributes() =>
    [
        new() {
            Name = "ShowDeleteButton",
            Description = Localizer["UploadsShowDeleteButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsDisabled",
            Description = Localizer["UploadsIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new() {
            Name = "PlaceHolder",
            Description = Localizer["UploadsPlaceHolder"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "BrowserButtonClass",
            Description = Localizer["UploadsBrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new() {
            Name = "BrowserButtonIcon",
            Description = Localizer["UploadsBrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-folder-open"
        },
        new() {
            Name = "BrowserButtonText",
            Description = Localizer["UploadsBrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new() {
            Name = "DeleteButtonClass",
            Description = Localizer["UploadsDeleteButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-danger"
        },
        new() {
            Name = "DeleteButtonIcon",
            Description = Localizer["UploadsDeleteButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-trash"
        },
        new() {
            Name = "DeleteButtonText",
            Description = Localizer["UploadsDeleteButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["UploadsDeleteButtonTextDefaultValue"]
        },
        new() {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    private List<AttributeItem> GetButtonAttributes() =>
    [
        new() {
            Name = "IsDirectory",
            Description = Localizer["UploadsIsDirectory"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsMultiple",
            Description = Localizer["UploadsIsMultiple"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsSingle",
            Description = Localizer["UploadsIsSingle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowProgress",
            Description = Localizer["UploadsShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "BrowserButtonClass",
            Description = Localizer["UploadsBrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new() {
            Name = "BrowserButtonIcon",
            Description = Localizer["UploadsBrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-folder-open"
        },
        new() {
            Name = "BrowserButtonText",
            Description = Localizer["UploadsBrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new() {
            Name = "DefaultFileList",
            Description = Localizer["UploadsDefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnGetFileFormat",
            Description = Localizer["UploadsOnGetFileFormat"],
            Type = "Func<string, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnDownload",
            Description = Localizer["UploadsOnDownload"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "IconTemplate",
            Description = Localizer["UploadsIconTemplate"],
            Type = "RenderFragment<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    private List<AttributeItem> GetAvatarAttributes() =>
    [
        new() {
            Name = "Width",
            Description = Localizer["UploadsWidth"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new() {
            Name = "Height",
            Description = Localizer["UploadsHeight"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "0"
        },
        new() {
            Name = "IsCircle",
            Description = Localizer["UploadsIsCircle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsSingle",
            Description = Localizer["UploadsIsSingle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowProgress",
            Description = Localizer["UploadsShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "DefaultFileList",
            Description = Localizer["UploadsDefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    private class Person
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; } = "Blazor";

        [Required]
        [FileValidation(Extensions = [".png", ".jpg", ".jpeg"], FileSize = 50 * 1024)]
        public IBrowserFile? Picture { get; set; }
    }
}
