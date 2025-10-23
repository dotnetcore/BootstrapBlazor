// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// AvatarUpload sample code
/// </summary>
public partial class UploadAvatars
{
    private readonly List<UploadFile> _previewFileList = [];
    private readonly Person _foo = new();
    private bool _isUploadButtonAtFirst;
    private bool _isCircle;
    private int _radius = 49;
    private bool _isMultiple = true;
    private bool _isDisabled = false;

    private string? RadiusString => $"{_radius}px";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _previewFileList.AddRange(
        [
            new UploadFile { PrevUrl = $"{WebsiteOption.Value.AssetRootPath}images/Argo.png" }
        ]);
    }

    private Task OnAvatarValidSubmit(EditContext context)
    {
        return ToastService.Success(Localizer["UploadsValidateFormTitle"], Localizer["UploadsValidateFormValidContent"]);
    }

    private Task OnAvatarInValidSubmit(EditContext context)
    {
        return ToastService.Error(Localizer["UploadsValidateFormTitle"], Localizer["UploadsValidateFormInValidContent"]);
    }

    private List<AttributeItem> GetAttributes() =>
    [
        new()
        {
            Name = "Width",
            Description = Localizer["UploadsWidth"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "Height",
            Description = Localizer["UploadsHeight"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsCircle",
            Description = Localizer["UploadsIsCircle"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "BorderRadius",
            Description = Localizer["UploadsBorderRadius"],
            Type = "string?",
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
        public List<IBrowserFile>? Picture { get; set; }
    }
}
