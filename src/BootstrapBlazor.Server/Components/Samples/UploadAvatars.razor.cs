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
    private CancellationTokenSource? ReadAvatarToken { get; set; }

    private static long MaxFileLength => 5 * 1024 * 1024;

    private List<UploadFile> PreviewFileList { get; } = [];

    private Person Foo2 { get; set; } = new Person();

    [NotNull]
    private ConsoleLogger? Logger2 { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PreviewFileList.AddRange(
        [
            new UploadFile { PrevUrl = $"{WebsiteOption.CurrentValue.AssetRootPath}images/Argo.png" }
        ]);
    }

    private async Task OnAvatarUpload(UploadFile file)
    {
        // 示例代码，使用 base64 格式
        if (file is { File: not null })
        {
            var format = file.File.ContentType;
            if (file.IsImage())
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

    private Task OnAvatarValidSubmit(EditContext context)
    {
        Logger2.Log(Foo2.Picture?.Name ?? "");
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        ReadAvatarToken?.Cancel();
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
            ValueList = "—",
            DefaultValue = "0"
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
            Name = "IsSingle",
            Description = Localizer["UploadsIsSingle"],
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
