﻿// Licensed to the .NET Foundation under one or more agreements.
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
            $"{WebsiteOption.CurrentValue.AssetRootPath}images/Pic0.jpg",
            $"{WebsiteOption.CurrentValue.AssetRootPath}images/Pic1.jpg",
            $"{WebsiteOption.CurrentValue.AssetRootPath}images/Pic2.jpg"
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

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Images",
            Description = Localizer["Images"],
            Type = "IEnumerable<string>",
            ValueList = "—",
            DefaultValue = "—"
        },
        new()
        {
            Name = "IsFade",
            Description = Localizer["IsFade"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "HoverPause",
            Description = Localizer["HoverPause"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Width",
            Description = Localizer["Width"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new()
        {
            Name = "OnClick",
            Description = Localizer["OnClick"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PlayMode",
            Description = Localizer["PlayMode"],
            Type = "CarouselPlayMode",
            ValueList = "AutoPlayOnload|AutoPlayAfterManually|Manually",
            DefaultValue = "AutoPlayOnload"
        }
    ];
}
