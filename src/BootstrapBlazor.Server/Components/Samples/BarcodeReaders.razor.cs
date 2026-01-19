// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class BarcodeReaders
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnInit(IEnumerable<DeviceItem> devices)
    {
        var cams = string.Join("", devices.Select(i => i.Label));
        Logger.Log($"{Localizer["InitLog"]} {cams}");
        return Task.CompletedTask;
    }
    private Task OnResult(string barcode)
    {
        Logger.Log($"{Localizer["ScanCodeLog"]} {barcode}");
        return Task.CompletedTask;
    }

    private Task OnError(string error)
    {
        Logger.Log($"{Localizer["ErrorLog"]} {error}");
        return Task.CompletedTask;
    }

    private Task OnStart()
    {
        Logger.Log(Localizer["OpenCameraLog"]);
        return Task.CompletedTask;
    }

    private Task OnClose()
    {
        Logger.Log(Localizer["CloseCameraLog"]);
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? ImageLogger { get; set; }

    private Task OnImageResult(string barcode)
    {
        ImageLogger.Log($"{Localizer["ScanCodeLog"]} {barcode}");
        return Task.CompletedTask;
    }

    private Task OnImageError(string err)
    {
        ImageLogger.Log($"{Localizer["ErrorLog"]} {err}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ButtonScanText",
            Description = Localizer["ButtonScanText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["ButtonScanTextDefaultValue"]
        },
        new()
        {
            Name = "ButtonStopText",
            Description = Localizer["ButtonStopText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["ButtonStopTextDefaultValue"]
        },
        new()
        {
            Name = "AutoStopText",
            Description = Localizer["AutoStopText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["AutoStopTextDefaultValue"]
        },
        new()
        {
            Name = "DeviceLabel",
            Description = Localizer["DeviceLabel"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["DeviceLabelDefaultValue"]
        },
        new()
        {
            Name = "InitDevicesString",
            Description = Localizer["InitDevicesString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["InitDevicesStringDefaultValue"]
        },
        new()
        {
            Name = "NotFoundDevicesString",
            Description = Localizer["NotFoundDevicesString"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["NotFoundDevicesStringDefaultValue"]
        },
        new()
        {
            Name = "AutoStart",
            Description = Localizer["AutoStart"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "AutoStop",
            Description = Localizer["AutoStart"],
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ScanType",
            Description = "",
            Type = "ScanType",
            ValueList = "Camera|Image",
            DefaultValue = "Camera"
        },
        new()
        {
            Name = "OnInit",
            Description = Localizer["OnInit"],
            Type = "Func<IEnumerable<Camera>, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnResult",
            Description = Localizer["OnResult"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnStart",
            Description = Localizer["OnStart"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClose",
            Description = Localizer["OnClose"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnError",
            Description = Localizer["OnError"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnDeviceChanged",
            Description = Localizer["OnDeviceChanged"],
            Type = "Func<DeviceItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
