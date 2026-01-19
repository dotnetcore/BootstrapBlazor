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

    /// <summary>
    /// GetImageName
    /// </summary>
    /// <returns></returns>
    private string GetImageName()
    {
        var index = Convert.ToInt32(ImageRandomer.Next(0, 8) / 1.0);
        var imageName = Path.GetFileNameWithoutExtension(ImagesName);
        var extendName = Path.GetExtension(ImagesName);
        var fileName = $"{imageName}{index}{extendName}";
        return Path.Combine(ImagesPath, fileName);
    }

    /// <summary>
    /// 获得/设置 图床路径 默认值为 Pic.jpg 通过设置 Max 取 Pic0.jpg ... Pic8.jpg
    /// </summary>
    private string ImagesName { get; set; } = "Pic.jpg";

    /// <summary>
    /// 获得/设置 图床路径 默认值为 images
    /// </summary>
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

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnValid",
            Description = Localizer["OnValid"],
            Type ="Action<bool>"
        }
    ];

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private MethodItem[] GetMethods() =>
    [
        new()
        {
            Name = "GetImageName",
            Description = Localizer["GetImageName"],
            Parameters =" — ",
            ReturnValue = "string"
        }
    ];
}
