// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// DropUpload sample code
/// </summary>
public partial class UploadDrops
{
    private bool _isMultiple = true;
    private bool _isDisabled = false;
    private bool _showProgress = true;
    private bool _showFooter = true;
    private bool _showUploadFileList = true;
    private bool _showDownloadButton = true;

    private Task OnDropUpload(UploadFile file)
    {
        // 模拟保存文件等处理
        if (file.File is { Size: > 5 * 1024 * 1024 })
        {
            file.Code = 1004;
            ToastService.Information("Error", Localizer["DropUploadFooterText"]);
        }
        return Task.CompletedTask;
    }

    private List<AttributeItem> GetAttributes() =>
    [
        new()
        {
            Name = "BodyTemplate",
            Description = Localizer["UploadsBodyTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IconTemplate",
            Description = Localizer["UploadsIconTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TextTemplate",
            Description = Localizer["UploadsTextTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "UploadIcon",
            Description = Localizer["UploadsUploadIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "UploadText",
            Description = Localizer["UploadsUploadText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowFooter",
            Description = Localizer["UploadsShowFooter"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "FooterTemplate",
            Description = Localizer["UploadsFooterTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "FooterText",
            Description = Localizer["UploadsFooterText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
