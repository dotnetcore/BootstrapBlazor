// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ImageCropper 组件示例
/// </summary>
public partial class ImageCroppers
{
    private ImageCropper _cropper = default!;

    private readonly string[] images = ["./images/picture.jpg", "./images/ImageList2.jpeg"];

    private int index = 0;

    private string? _base64String;

    private async Task OnClickReplace()
    {
        index = index == 0 ? 1 : 0;
        await _cropper.Replace(images[index]);
    }

    private async Task Crop()
    {
        _base64String = await _cropper.Crop();
    }

    private Task Rotate() => _cropper.Rotate(90);

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
            Name = "IsDisabled",
            Description = Localizer["AttributesImageCropperIsDisabled"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnCropAsync",
            Description = Localizer["AttributesImageCropperOnCropAsync"],
            Type = "Func<ImageCropperResult, Task>",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Options",
            Description = Localizer["AttributesImageCropperOptions"],
            Type = "ImageCropperOption",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "CropperShape",
            Description = Localizer["AttributesImageCropperShape"],
            Type = "ImageCropperShape",
            ValueList = "-",
            DefaultValue = "-"
        }
    ];
}
