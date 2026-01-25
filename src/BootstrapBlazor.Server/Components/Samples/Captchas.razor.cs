// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Captchas
{
    [Inject]
    [NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    private static Random ImageRandomer { get; set; } = new Random();

    private string GetImageName()
    {
        var index = Convert.ToInt32(ImageRandomer.Next(0, 8) / 1.0);
        var imageName = Path.GetFileNameWithoutExtension(ImagesName);
        var extendName = Path.GetExtension(ImagesName);
        var fileName = $"{imageName}{index}{extendName}";
        return Path.Combine(ImagesPath, fileName);
    }

    private string ImagesName { get; set; } = "Pic.jpg";

    [NotNull]
    private string? ImagesPath { get; set; }

    [NotNull]
    private Captcha? NormalCaptcha { get; set; }

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ImagesPath = $"{WebsiteOption.Value.AssetRootPath}images";
    }

    private async Task OnValidAsync(bool ret)
    {
        var result = ret ? "成功" : "失败";
        NormalLogger.Log($"验证码结果 -> {result}");
        if (ret)
        {
            await Task.Delay(1000);
            await NormalCaptcha.Reset();
        }
    }
}
