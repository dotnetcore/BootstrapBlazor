// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ImageCropper 组件示例
/// </summary>
public partial class ImageCroppers
{
    private ImageCropper _cropper = default!;

    private ImageCropper _roundCropper = default!;

    private readonly List<string> _images = [];

    private int index = 0;

    private string? _base64String;

    private string? _base64String2;

    private readonly ImageCropperOption _roundOptions = new() { IsRound = true, Radius = "50%", AspectRatio = 3/4f };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _images.AddRange(
        [
            $"{WebsiteOption.CurrentValue.AssetRootPath}images/picture.jpg",
            $"{WebsiteOption.CurrentValue.AssetRootPath}images/ImageList2.jpeg"
        ]);
    }

    private async Task OnClickReplace()
    {
        index = index == 0 ? 1 : 0;
        await _cropper.Replace(_images[index]);
    }

    private async Task Crop()
    {
        _base64String = await _cropper.Crop();
    }

    private async Task RoundCrop()
    {
        _base64String2 = await _roundCropper.Crop();
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
