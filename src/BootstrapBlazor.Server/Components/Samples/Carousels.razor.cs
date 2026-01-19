// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Carousels
/// </summary>
public sealed partial class Carousels
{
    [NotNull]
    private ConsoleLogger? OnClickLogger { get; set; }

    private readonly List<string> _images = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _images.AddRange([
            $"{WebsiteOption.Value.AssetRootPath}images/Pic0.jpg",
            $"{WebsiteOption.Value.AssetRootPath}images/Pic1.jpg",
            $"{WebsiteOption.Value.AssetRootPath}images/Pic2.jpg"
        ]);
    }

    /// <summary>
    /// OnClick
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <returns></returns>
    private Task OnClick(string imageUrl)
    {
        OnClickLogger.Log($"Image Clicked: {imageUrl}");
        return Task.CompletedTask;
    }
}
