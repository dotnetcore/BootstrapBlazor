// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Cameras
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = nameof(Camera.VideoWidth),
            Description = Localizer["VideoWidth"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "320"
        },
        new AttributeItem() {
            Name = nameof(Camera.VideoHeight),
            Description = Localizer["VideoHeight"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "240"
        },
        new AttributeItem() {
            Name = "ShowPreview",
            Description = Localizer["ShowPreview"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "AutoStart",
            Description = Localizer["AutoStart"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "DeviceLabel",
            Description = Localizer["DeviceLabel"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FrontText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BackText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "PlayText",
            Description = "",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "StopText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "PhotoText",
            Description = Localizer["FrontText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "InitDevicesString",
            Description = Localizer["InitDevicesString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["InitDevicesStringDefaultValue"]
        },
        new AttributeItem()
        {
            Name = "NotFoundDevicesString",
            Description = Localizer["NotFoundDevicesString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["NotFoundDevicesStringDefaultValue"]
        },
        new AttributeItem() {
            Name = "OnInit",
            Description = Localizer["OnInit"],
            Type = "Func<IEnumerable<DeviceItem>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnStart",
            Description = Localizer["OnStart"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnClose",
            Description = Localizer["OnClose"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnCapture",
            Description = Localizer["OnCapture"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(Camera.CaptureJpeg),
            Description = Localizer["CaptureJpeg"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(Camera.Quality),
            Description = Localizer["Quality"],
            Type = "double",
            ValueList = " — ",
            DefaultValue = " 0.9d"
        },

    };
}
