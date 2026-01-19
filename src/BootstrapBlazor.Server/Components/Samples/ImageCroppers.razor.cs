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

    private readonly ImageCropperOption _roundOptions1 = new() { AspectRatio = 16 / 9f, Preview = ".bb-cropper-preview1" };

    private readonly ImageCropperOption _roundOptions2 = new() { IsRound = true, Preview = ".bb-cropper-preview-round" };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _images.AddRange(
        [
            $"{WebsiteOption.Value.AssetRootPath}images/picture.jpg",
            $"{WebsiteOption.Value.AssetRootPath}images/ImageList2.jpeg"
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

    private ImageCropperData _data = new();
    private Task OnCropChangedAsync(ImageCropperData data)
    {
        _data = data;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
