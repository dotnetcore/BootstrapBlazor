// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Uploads : IDisposable
{
    private static readonly Random random = new();

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? SiteOptions { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Uploads>? Localizer { get; set; }

    private List<UploadFile> PreviewFileList { get; } = new(new[] { new UploadFile { PrevUrl = "_content/BootstrapBlazor.Shared/images/Argo.png" } });

    private BlockLogger? Trace { get; set; }

    private List<UploadFile> DefaultFormatFileList { get; } = new List<UploadFile>()
    {
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
    };

    private List<UploadFile> Base64FormatFileList { get; } = new List<UploadFile>()
    {
        new UploadFile { FileName = "Test", PrevUrl = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACoAAAAkCAYAAAD/yagrAAAE60lEQVR4AWJwL/AB9GIWvI0jURz/L1NomZmZP8p9kWNmhnIbKPdomZmZGcJgO7QsWMbce55Yl11t47FbVdKTYpjOr/+H4y4Z/MpYeJVV8KVvokFRylq9osKfVtGQ/NHyPl2CbNVGwau2oOlmgUDJtDJGzxtvvIA3vRH+7IgeA0VtYhQBtKPlToFgC6RY58aggfwLNKhr0Zry2NnPrpIM2YGW2+UBG1IEmdGVJEVXE+hQXt8joKhLjdGVZHd7TSD9DHmT3J1dheroSF4fhnvUVQwbZx9UOnHSzQjkTN0tIG+8hFdbxet4PQOG4WqJwN1hFVb+xUBmCblxvxRkIE+Q+Yf0T31NSrp4/RV4FhHgoSTchQRZBK4D1+Fe2q2g8CYXU6buQyNDaiZKZhn0IYXHVwbkRQydHyawOAGGisa/o3DvI/jF3QKKiuAy+LKs5CvaXMLdeXb35wbkKTjmReDZHysCBosWJqN7r0jZ/VfgXtIlUNQnVpK7D4nNJSC5BHGi1d108Ppr8MxlF0cJqBSyFJaUfUnvHLyO4cttgaI+NhGB7HE0MaSUks/gU5vwe1gv5tfhmUmAhxME8jbIUtiEDus+fhmDJlgCRY0ylZRZhybKWtNinmYlH9NvL5puO3m9UHOkgzb3xuF5HCkDyhYiSwrYVfQPTpYChTc+E35tG9VJBjGFFNmtVlNMDnjzbx0EBpKq1eTeh2awbArBRuHadBWu6WVB4U/NITfuobYoCZl7QL//wDtr+nTmsgzQh2Kwgtz7QAZWFeGw/RI8s94Kigp1PvzZg2i9x11FDtKn/oZCoZdZxhaAXmG4/ohJKHudLE1Gyu66DOfs10BRR5CU3TKQIrtzj9BAkF991dsMslTZEClLsI+jFmANZYFAdAJl9QG03jWH9GrcFh9QTP4Of6GfJGSpsv1J2aoEKRuWCANNJNqOFAaPAW36rVRMCjVf6ZCtqYF2p6Asxg5mWK6tQamY9RCs8z1wF+FxTR5UqYT/3GC7oCkMHcjxKguq6cnl+Egf2whip3C9Yu56jk+GXXOtv1XIa8L1f3AFkHe9az83AmNanwV/bhfa5JKJ3n1slCUL8dk7CNfvVFMfySRThoxbK7fh18tTZXI2QeySLk88IRGsbHkqQj6wUJ72G5CloCXKZrdbLPgVMgVfQMq5m97bfQWOOeVbaK02nSA2ofn2y662UJE47horLZTe38oDjdxQUhmawocxa0OJ5hXjnZE4w1y0uc/iULKGfk+xNuZxI/BnjhFs+THPq4phmTsbeYPXRjFsJJ+NSMlnInHkxrwzGDjR3uBcG1/B/b/TwZkhubb6tP2oVfTAD2G4kw9vYiA2h+zy4GwYxd7S4qGOgd6oqVn6re1DTXiO4e4wPF+yQlF4ZCBfheSPIjJn+ciS/w93qjA6aZYqeQ5D3dTqvuE6GZNTkov5vqsYvLh7j8u1sWWkLIdBQR+q+XdtaKGhJCWBDpkwgSw9gRpKyoNKw6rjCLCDP4yVflSgWFwt3C0HSUo2BTFYX28fVAaWPpDx7wtwjOSSUszawnUT0KTudlfrFQwZ3WNf867CNZQhk/C8kIFUhJLt/O3Jzn62IYNwrUvA8zws4W61CGlDSfugXMz5pJgiSFYyXMYiRXdH4GmyqaR9UHLxzxG41KAwpZxRPN4kRf85Z5I4MvYfFUFGfemJG40AAAAASUVORK5CYII=" },
    };

    private Task OnFileChange(UploadFile file)
    {
        // 未真正保存文件
        // file.SaveToFile()
        Trace?.Log($"{file.File!.Name} {Localizer["Success"]}");
        return Task.FromResult("");
    }

    private async Task OnClickToUpload(UploadFile file)
    {
        // 示例代码，模拟 80% 几率保存成功
        var error = random.Next(1, 100) > 80;
        if (error)
        {
            file.Code = 1;
            file.Error = Localizer["Error"];
        }
        else
        {
            await SaveToFile(file);
        }
    }

    private CancellationTokenSource? UploadFolderToken { get; set; }
    private async Task OnUploadFolder(UploadFile file)
    {
        // 上传文件夹时会多次回调此方法
        await SaveToFile(file);
    }

    private CancellationTokenSource? ReadAvatarToken { get; set; }
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
                file.Error = Localizer["FormatError"];
            }

            if (file.Code != 0)
            {
                await ToastService.Error(Localizer["AvatarMsg"], $"{file.Error} {format}");
            }
        }
    }

    private CancellationTokenSource? ReadToken { get; set; }

    private static long MaxFileLength => 200 * 1024 * 1024;

    private async Task OnCardUpload(UploadFile file)
    {
        if (file != null && file.File != null)
        {
            // 服务器端验证当文件大于 2MB 时提示文件太大信息
            if (file.Size > MaxFileLength)
            {
                await ToastService.Information(Localizer["FileMsg"], Localizer["FileError"]);
                file.Code = 1;
                file.Error = Localizer["FileError"];
            }
            else
            {
                await SaveToFile(file);
            }
        }
    }

    private async Task<bool> SaveToFile(UploadFile file)
    {
        // Server Side 使用
        // Web Assembly 模式下必须使用 webapi 方式去保存文件到服务器或者数据库中
        // 生成写入文件名称
        var ret = false;
        if (!string.IsNullOrEmpty(SiteOptions.Value.WebRootPath))
        {
            var uploaderFolder = Path.Combine(SiteOptions.Value.WebRootPath, $"images{Path.DirectorySeparatorChar}uploader");
            file.FileName = $"{Path.GetFileNameWithoutExtension(file.OriginFileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(file.OriginFileName)}";
            var fileName = Path.Combine(uploaderFolder, file.FileName);

            ReadToken ??= new CancellationTokenSource();
            ret = await file.SaveToFile(fileName, MaxFileLength, ReadToken.Token);

            if (ret)
            {
                // 保存成功
                file.PrevUrl = $"images/uploader/{file.FileName}";
            }
            else
            {
                var errorMessage = $"{Localizer["SaveFileError"]} {file.OriginFileName}";
                file.Code = 1;
                file.Error = errorMessage;
                await ToastService.Error(Localizer["UploadFile"], errorMessage);
            }
        }
        else
        {
            file.Code = 1;
            file.Error = Localizer["WasmError"];
            await ToastService.Information(Localizer["SaveFile"], Localizer["SaveFileMsg"]);
        }
        return ret;
    }

    private static bool CheckValidAvatarFormat(string format)
    {
        return "jpg;png;bmp;gif;jpeg".Split(';').Any(f => format.Contains(f, StringComparison.OrdinalIgnoreCase));
    }

    private Task<bool> OnFileDelete(UploadFile item)
    {
        Trace?.Log($"{item.OriginFileName} {Localizer["RemoveMsg"]}");
        return Task.FromResult(true);
    }

    private Person Foo { get; set; } = new Person();

    private static Task OnSubmit(EditContext context)
    {
        // 示例代码请根据业务情况自行更改
        // var fileName = Foo.Picture?.Name;
        return Task.CompletedTask;
    }

    [NotNull]
    private BlockLogger? AvatarTrace { get; set; }

    private Task OnAvatarValidSubmit(EditContext context)
    {
        AvatarTrace.Log(Foo.Picture?.Name ?? "");
        return Task.CompletedTask;
    }

    private class Person
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; } = "Blazor";

        [Required]
        [FileValidation(Extensions = new string[] { ".png", ".jpg", ".jpeg" }, FileSize = 50 * 1024)]
        public IBrowserFile? Picture { get; set; }
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetInputAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowDeleteButton",
            Description = Localizer["ShowDeleteButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["IsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "PlaceHolder",
            Description = Localizer["PlaceHolder"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Accept",
            Description = Localizer["Accept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BrowserButtonClass",
            Description = Localizer["BrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new AttributeItem() {
            Name = "BrowserButtonIcon",
            Description = Localizer["BrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa fa-folder-open-o"
        },
        new AttributeItem() {
            Name = "BrowserButtonText",
            Description = Localizer["BrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "DeleteButtonClass",
            Description = Localizer["DeleteButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-danger"
        },
        new AttributeItem() {
            Name = "DeleteButtonIcon",
            Description = Localizer["DeleteButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa fa-trash-o"
        },
        new AttributeItem() {
            Name = "DeleteButtonText",
            Description = Localizer["DeleteButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["DeleteButtonTextDefaultValue"]
        },
        new AttributeItem() {
            Name = "OnDelete",
            Description = Localizer["OnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnChange",
            Description = Localizer["OnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private IEnumerable<AttributeItem> GetButtonAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "IsDirectory",
            Description = Localizer["IsDirectory"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsMultiple",
            Description = Localizer["IsMultiple"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsSingle",
            Description = Localizer["IsSingle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowProgress",
            Description = Localizer["ShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Accept",
            Description = Localizer["Accept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BrowserButtonClass",
            Description = Localizer["BrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new AttributeItem() {
            Name = "BrowserButtonIcon",
            Description = Localizer["BrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa fa-folder-open-o"
        },
        new AttributeItem() {
            Name = "BrowserButtonText",
            Description = Localizer["BrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "DefaultFileList",
            Description = Localizer["DefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnGetFileFormat",
            Description = Localizer["OnGetFileFormat"],
            Type = "Func<string, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnDelete",
            Description = Localizer["OnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnChange",
            Description = Localizer["OnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " - "
        }
    };

    private IEnumerable<AttributeItem> GetAvatarAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Width",
            Description = Localizer["Width"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "Height",
            Description = Localizer["Height"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "IsCircle",
            Description = Localizer["IsCircle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsSingle",
            Description = Localizer["IsSingle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowProgress",
            Description = Localizer["ShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Accept",
            Description = Localizer["Accept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "DefaultFileList",
            Description = Localizer["DefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnDelete",
            Description = Localizer["OnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnChange",
            Description = Localizer["OnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        UploadFolderToken?.Dispose();
        ReadAvatarToken?.Cancel();
        ReadToken?.Cancel();
        GC.SuppressFinalize(this);
    }
}
