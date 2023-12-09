// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Carousels
/// </summary>
public sealed partial class Carousels
{
    [NotNull]
    private ConsoleLogger? OnClickLogger { get; set; }

    /// <summary>
    /// Images
    /// </summary>
    private static List<string> Images =>
    [
        "./images/Pic0.jpg",
        "./images/Pic1.jpg",
        "./images/Pic2.jpg"
    ];

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
