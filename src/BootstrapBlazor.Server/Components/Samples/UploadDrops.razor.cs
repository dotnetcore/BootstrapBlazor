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

    private readonly List<UploadFile> _dropFiles = [];

    private async Task OnDropUpload(UploadFile file)
    {
        // 模拟保存文件等处理
        await Task.Delay(1000);

        // 添加文件到集合中，用于 UI 呈现上传列表
        _dropFiles.Add(file);
        StateHasChanged();
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
