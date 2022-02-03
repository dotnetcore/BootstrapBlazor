// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Captchas
{
    /// <summary>
    /// 获得/设置 图床路径 默认值为 images
    /// </summary>
    public string ImagesPath { get; set; } = "_content/BootstrapBlazor.Shared/images";

    /// <summary>
    /// 获得/设置 图床路径 默认值为 Pic.jpg
    /// </summary>
    public string ImagesName { get; set; } = "Pic.jpg";

    /// <summary>
    /// 
    /// </summary>
    private Captcha? Captcha { get; set; }

    /// <summary>
    /// 
    /// </summary>
    private BlockLogger? Trace { get; set; }

    private void OnValid(bool ret)
    {
        var result = ret ? "成功" : "失败";
        Trace?.Log($"验证码结果 -> {result}");
        if (ret)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                Captcha?.Reset();
            });
        }
    }

    private static Random ImageRandomer { get; set; } = new Random();

    /// <summary>
    /// 
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
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ImagesPath",
                Description = Localizer["ImagesPath"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "images"
            },
            new AttributeItem() {
                Name = "ImagesName",
                Description = Localizer["ImagesName"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "Pic.jpg"
            },
            new AttributeItem() {
                Name = "HeaderText",
                Description = Localizer["HeaderText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["HeaderTextDefaultValue"]
            },
            new AttributeItem() {
                Name = "BarText",
                Description = Localizer["BarText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["BarTextDefaultValue"]
            },
            new AttributeItem() {
                Name = "FailedText",
                Description = Localizer["FailedText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["FailedTextDefaultValue"]
            },
            new AttributeItem() {
                Name = "LoadText",
                Description = Localizer["LoadText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["LoadTextDefaultValue"]
            },
            new AttributeItem() {
                Name = "TryText",
                Description = Localizer["TryText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["TryTextDefaultValue"]
            },
            new AttributeItem() {
                Name = "Offset",
                Description = Localizer["Offset"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "5"
            },
            new AttributeItem() {
                Name = "Width",
                Description = Localizer["Width"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "280"
            },
            new AttributeItem() {
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
    private IEnumerable<EventItem> GetEvents() => new[]
    {
            new EventItem()
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
    private IEnumerable<MethodItem> GetMethods() => new[]
    {
            new MethodItem()
            {
                Name = "GetImageName",
                Description = Localizer["GetImageName"],
                Parameters =" — ",
                ReturnValue = "string"
            }
        };
}
