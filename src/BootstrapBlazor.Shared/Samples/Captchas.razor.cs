// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Captchas
{
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
    public string ImagesName { get; set; } = "Pic.jpg";

    /// <summary>
    /// 获得/设置 图床路径 默认值为 images
    /// </summary>
    public string ImagesPath { get; set; } = "./images";

    [NotNull]
    private Captcha? NormalCaptcha { get; set; }

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

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
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "ImagesPath",
            Description = Localizer["ImagesPath"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "images"
        },
        new()
        {
            Name = "ImagesName",
            Description = Localizer["ImagesName"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "Pic.jpg"
        },
        new()
        {
            Name = "HeaderText",
            Description = Localizer["HeaderText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["HeaderTextDefaultValue"]
        },
        new()
        {
            Name = "BarText",
            Description = Localizer["BarText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["BarTextDefaultValue"]
        },
        new()
        {
            Name = "FailedText",
            Description = Localizer["FailedText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["FailedTextDefaultValue"]
        },
        new()
        {
            Name = "LoadText",
            Description = Localizer["LoadText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["LoadTextDefaultValue"]
        },
        new()
        {
            Name = "TryText",
            Description = Localizer["TryText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["TryTextDefaultValue"]
        },
        new()
        {
            Name = "Offset",
            Description = Localizer["Offset"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "5"
        },
        new()
        {
            Name = "Width",
            Description = Localizer["Width"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "280"
        },
        new()
        {
            Name = "Height",
            Description = Localizer["Height"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "155"
        }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new()
        {
            Name = "OnValid",
            Description = Localizer["OnValid"],
            Type ="Action<bool>"
        }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {
        new()
        {
            Name = "GetImageName",
            Description = Localizer["GetImageName"],
            Parameters =" — ",
            ReturnValue = "string"
        }
    };
}
