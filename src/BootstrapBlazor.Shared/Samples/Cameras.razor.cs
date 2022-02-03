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
public sealed partial class Cameras
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    private Task OnInit(IEnumerable<DeviceItem> devices)
    {
        var cams = string.Join("", devices.Select(i => i.Label));
        Trace.Log($"初始化摄像头完成 {cams}");
        return Task.CompletedTask;
    }

    private Task OnError(string err)
    {
        Trace.Log($"发生错误 {err}");
        return Task.CompletedTask;
    }

    private Task OnStart()
    {
        ImageUrl = null;
        Trace.Log("打开摄像头");
        return Task.CompletedTask;
    }

    private Task OnClose()
    {
        Trace.Log("关闭摄像头");
        return Task.CompletedTask;
    }

    private string? ImageUrl { get; set; }

    private Task OnCapture(string url)
    {
        ImageUrl = url;
        Trace.Log("拍照完成");
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
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
            }
    };
}
