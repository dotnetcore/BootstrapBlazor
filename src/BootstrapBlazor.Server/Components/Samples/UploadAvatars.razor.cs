// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// AvatarUpload sample code
/// </summary>
public partial class UploadAvatars : IDisposable
{
    private static readonly long MaxFileLength = 5 * 1024 * 1024;
    private CancellationTokenSource? _token;
    private readonly List<UploadFile> _previewFileList = [];
    private readonly Person _foo = new();
    private bool _isMultiple = true;
    private bool _isUploadButtonAtFirst;
    private bool _isCircle;
    private int _radius = 49;
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
            new UploadFile { PrevUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Argo.png" }
        ]);
    }

    private async Task OnChange(UploadFile file)
    {
        // 示例代码，使用 base64 格式
        if (file is { File: not null })
        {
            var format = file.File.ContentType;
            if (file.IsImage())
            {
                _token ??= new CancellationTokenSource();
                if (_token.IsCancellationRequested)
                {
                    _token.Dispose();
                    _token = new CancellationTokenSource();
                }

                await file.RequestBase64ImageFileAsync(format, 640, 480, MaxFileLength, _token.Token);
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

    private Task OnAvatarValidSubmit(EditContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        _token?.Cancel();
        GC.SuppressFinalize(this);
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
        },
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
            Name = "Accept",
            Description = Localizer["UploadsAccept"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DefaultFileList",
            Description = Localizer["UploadsDefaultFileList"],
            Type = "List<UploadFile>",
            ValueList = " — ",
            DefaultValue = " — "
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
