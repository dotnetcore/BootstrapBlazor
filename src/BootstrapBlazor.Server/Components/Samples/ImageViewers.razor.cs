// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Images 示例类
/// </summary>
public partial class ImageViewers
{
    private List<string> PreviewList { get; } = [];

    [NotNull]
    private ImagePreviewer? ImagePreviewer { get; set; }

    private Task ShowImagePreviewer() => ImagePreviewer.Show();

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PreviewList.AddRange(
        [
            $"{WebsiteOption.Value.AssetRootPath}images/ImageList1.jpeg",
            $"{WebsiteOption.Value.AssetRootPath}images/ImageList2.jpeg"
        ]);
    }
}
