// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// InputUpload sample code
/// </summary>
public partial class UploadInputs
{
    private Person Foo1 { get; set; } = new Person();

    private async Task OnFileChange(UploadFile file)
    {
        // 未真正保存文件
        // file.SaveToFile()
        await Task.Delay(200);
        await ToastService.Information(Localizer["UploadsSaveFile"], $"{file.File!.Name} {Localizer["UploadsSuccess"]}");
    }

    private static Task OnSubmit(EditContext context)
    {
        // 示例代码请根据业务情况自行更改
        // var fileName = Foo.Picture?.Name;
        return Task.CompletedTask;
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
            Name = "ShowDeleteButton",
            Description = Localizer["UploadsShowDeleteButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["UploadsIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "PlaceHolder",
            Description = Localizer["UploadsPlaceHolder"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "BrowserButtonClass",
            Description = Localizer["UploadsBrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new()
        {
            Name = "BrowserButtonIcon",
            Description = Localizer["UploadsBrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-folder-open"
        },
        new()
        {
            Name = "BrowserButtonText",
            Description = Localizer["UploadsBrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new()
        {
            Name = "DeleteButtonClass",
            Description = Localizer["UploadsDeleteButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-danger"
        },
        new()
        {
            Name = "DeleteButtonIcon",
            Description = Localizer["UploadsDeleteButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-trash"
        },
        new()
        {
            Name = "DeleteButtonText",
            Description = Localizer["UploadsDeleteButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["UploadsDeleteButtonTextDefaultValue"]
        },
        new()
        {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    class Person
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; } = "Blazor";

        [Required]
        [FileValidation(Extensions = [".png", ".jpg", ".jpeg"], FileSize = 5 * 1024 * 1024)]
        public IBrowserFile? Picture { get; set; }
    }
}
