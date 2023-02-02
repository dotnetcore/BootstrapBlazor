// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Captchas
{
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
