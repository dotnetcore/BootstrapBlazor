// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ImageCroppers
/// </summary>
public partial class ImageCroppers
{

    [NotNull]
    ImageCropper? Cropper { get; set; }

    private string[] images = ["./images/picture.jpg", "./images/ImageList2.jpeg"];

    private int index = 0;

    private string? Base64 { get; set; }

    private async Task OnClickReplace()
    {
        index = index == 0 ? 1 : 0;
        await Cropper.Replace(images[index]);
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    protected AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Url",
            Description = Localizer["AttributesImageCropperUrl"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "DefaultButton",
            Description = Localizer["AttributesImageCropperDefaultButton"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Preview",
            Description = Localizer["AttributesImageCropperPreview"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new()
        {
            Name = "OnResult()",
            Description = Localizer["AttributesImageCropperOnResult"],
            Type = "Func",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnBase64Result()",
            Description = Localizer["AttributesImageCropperOnBase64Result"],
            Type = "Func",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Crop()",
            Description = Localizer["AttributesImageCropperCrop"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "CropToStream()",
            Description = Localizer["AttributesImageCropperCropToStream"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
    ];
}
