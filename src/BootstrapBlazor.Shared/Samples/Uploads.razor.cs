// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Uploads
/// </summary>
public sealed partial class Uploads
{
    private IEnumerable<AttributeItem> GetInputAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ShowDeleteButton",
            Description = Localizer["UploadsShowDeleteButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["UploadsUploadsIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "PlaceHolder",
            Description = Localizer["UploadsPlaceHolder"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BrowserButtonClass",
            Description = Localizer["UploadsBrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new AttributeItem() {
            Name = "BrowserButtonIcon",
            Description = Localizer["UploadsBrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-folder-open"
        },
        new AttributeItem() {
            Name = "BrowserButtonText",
            Description = Localizer["UploadsBrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "DeleteButtonClass",
            Description = Localizer["UploadsDeleteButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-danger"
        },
        new AttributeItem() {
            Name = "DeleteButtonIcon",
            Description = Localizer["UploadsDeleteButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-trash"
        },
        new AttributeItem() {
            Name = "DeleteButtonText",
            Description = Localizer["UploadsDeleteButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["UploadsDeleteButtonTextDefaultValue"]
        },
        new AttributeItem() {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private IEnumerable<AttributeItem> GetButtonAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "IsDirectory",
            Description = Localizer["UploadsIsDirectory"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsMultiple",
            Description = Localizer["UploadsIsMultiple"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsSingle",
            Description = Localizer["UploadsIsSingle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowProgress",
            Description = Localizer["UploadsShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BrowserButtonClass",
            Description = Localizer["UploadsBrowserButtonClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "btn-primary"
        },
        new AttributeItem() {
            Name = "BrowserButtonIcon",
            Description = Localizer["UploadsBrowserButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-regular fa-folder-open"
        },
        new AttributeItem() {
            Name = "BrowserButtonText",
            Description = Localizer["UploadsBrowserButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = ""
        },
        new AttributeItem() {
            Name = "DefaultFileList",
            Description = Localizer["UploadsDefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnGetFileFormat",
            Description = Localizer["UploadsOnGetFileFormat"],
            Type = "Func<string, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnDownload",
            Description = Localizer["UploadsOnDownload"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IconTemplate",
            Description = Localizer["UploadsIconTemplate"],
            Type = "RenderFragment<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    private IEnumerable<AttributeItem> GetAvatarAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Width",
            Description = Localizer["UploadsWidth"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "Height",
            Description = Localizer["UploadsHeight"],
            Type = "int",
            ValueList = "—",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "IsCircle",
            Description = Localizer["UploadsIsCircle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsSingle",
            Description = Localizer["UploadsIsSingle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowProgress",
            Description = Localizer["UploadsShowProgress"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "DefaultFileList",
            Description = Localizer["UploadsDefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnDelete",
            Description = Localizer["UploadsOnDelete"],
            Type = "Func<string, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnChange",
            Description = Localizer["UploadsOnChange"],
            Type = "Func<UploadFile, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
