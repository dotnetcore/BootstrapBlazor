﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Bluetooth
/// </summary>
public partial class Bluetooth
{
    //Printer printer { get; set; } = new Printer();

    /// <summary>
    /// 显示内置界面
    /// </summary>
    bool ShowUI { get; set; } = false;

    private string? message;
    private string? statusMessage;
    private string? errorMessage;

    [Inject, NotNull]
    private IBluetoothService? BluetoothService { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private BluetoothDevice? _blueDevice;

    private string? _batteryValue = null;

    private async Task GetAvailability()
    {
        await BluetoothService.GetAvailability();
        if (BluetoothService.IsSupport == false)
        {
            await ToastService.Error(Localizer["NotSupportBluetoothTitle"], Localizer["NotSupportBluetoothContent"]);
        }
        if (BluetoothService.IsAvailable == false)
        {
            await ToastService.Error(Localizer["NoBluetoothAdapterTitle"], Localizer["NoBluetoothAdapterContent"]);
        }
    }

    private async Task RequestDevice()
    {
        if (BluetoothService.IsAvailable)
        {
            _blueDevice = await BluetoothService.RequestDevice();
        }
    }

    private async Task Connect()
    {
        if (_blueDevice != null)
        {
            await _blueDevice.Connect();
        }
    }

    private async Task Disconnect()
    {
        if (_blueDevice != null)
        {
            await _blueDevice.Disconnect();
        }
    }

    private async Task GetDevices()
    {
        if (BluetoothService.IsAvailable)
        {
            await BluetoothService.GetDevices();
        }
    }

    private async Task GetBatteryValue()
    {
        if (_blueDevice != null)
        {
            _batteryValue = await _blueDevice.GetBatteryValue();
        }
    }

    private Task OnResult(string? result)
    {
        message = result;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateStatus(string message)
    {
        statusMessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        errorMessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnGetDevices(List<string>? devices)
    {
        message = "";
        if (devices == null || devices!.Count == 0) return Task.CompletedTask;
        message += $"已配对设备{devices.Count}:{Environment.NewLine}";
        devices.ForEach(a => message += $"   {a}{Environment.NewLine}");
        //this.message = this.message.Replace(Environment.NewLine, "<br/>");
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 切换 UI 方法
    /// </summary>
    public void SwitchUI()
    {
        ShowUI = !ShowUI;
    }

    //Heartrate heartrate { get; set; } = new Heartrate();

    private Task OnUpdateValue(int v)
    {
        value = v;
        statusMessage = $"心率{value}";
        StateHasChanged();
        return Task.CompletedTask;
    }

    ///// <summary>
    ///// 获取心率
    ///// </summary>
    //public Task GetHeartrate() => heartrate.GetHeartrate();

    ///// <summary>
    ///// 停止获取心率
    ///// </summary>
    //public Task StopHeartrate() => heartrate.StopHeartrate();

    //BatteryLevel batteryLevel { get; set; } = new BatteryLevel();

    private decimal? value = 0;

    private Task OnUpdateValue(decimal v)
    {
        value = v;
        statusMessage = Localizer["DeviceBattery", value];
        StateHasChanged();
        return Task.CompletedTask;
    }

    //private Task OnUpdateStatus(BluetoothDevice device)
    //{
    //    statusMessage = device.Status;
    //    StateHasChanged();
    //    return Task.CompletedTask;
    //}

    ///// <summary>
    ///// 获取设备电量
    ///// </summary>
    //public Task GetBatteryLevel() => batteryLevel.GetBatteryLevel();

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Commands",
            Description = Localizer["CommandsAttr"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Print",
            Description = Localizer["PrintAttr"],
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateStatus",
            Description = Localizer["OnUpdateStatusAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateError",
            Description = Localizer["OnUpdateErrorAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "PrinterElement",
            Description = Localizer["PrinterElementAttr"],
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Opt",
            Description = Localizer["OptAttr"],
            Type = "PrinterOption",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "ShowUI",
            Description = Localizer["ShowUIAttr"],
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new()
        {
            Name = "Debug",
            Description = Localizer["DebugAttr"],
            Type = "bool",
            ValueList = "True|False",
            DefaultValue = "False"
        },
        new()
        {
            Name = "DeviceName",
            Description = Localizer["DeviceNameAttr"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
    ];

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetPrinterOptionAttributes() =>
    [
        new()
        {
            Name = "NamePrefix",
            Description = Localizer["NamePrefixAttr"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "MaxChunk",
            Description = Localizer["MaxChunkAttr"],
            Type = "int",
            ValueList = "-",
            DefaultValue = "100"
        },
    ];

    /// <summary>
    /// 获得蓝牙设备类
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetBluetoothDeviceAttributes() =>
    [
        new()
        {
            Name = "Name",
            Description = Localizer["NameAttr"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "Value",
            Description = Localizer["ValueAttr"],
            Type = "decimal?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "Status",
            Description = Localizer["StatusAttr"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "Error",
            Description = Localizer["ErrorAttr"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "null"
        }
    ];

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributesBatteryLevel() =>
    [
        new()
        {
            Name = "GetBatteryLevel",
            Description = Localizer["GetBatteryLevelAttr"],
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateValue",
            Description = Localizer["OnUpdateValueAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateStatus",
            Description = Localizer["OnUpdateStatusAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateError",
            Description = Localizer["OnUpdateErrorAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "BatteryLevelElement",
            Description = Localizer["BatteryLevelElementAttr"],
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        }
    ];


    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributesHeartrate() =>
    [
        new()
        {
            Name = "GetHeartrate",
            Description = Localizer["GetHeartrateAttr"],
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "StopHeartrate",
            Description = Localizer["StopHeartrateAttr"],
            Type = "async Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateValue",
            Description = Localizer["OnUpdateValueAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateStatus",
            Description = Localizer["OnUpdateStatusAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "OnUpdateError",
            Description = Localizer["OnUpdateErrorAttr"],
            Type = "Func<string, Task>?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "HeartrateElement",
            Description = Localizer["HeartrateElementAttr"],
            Type = "ElementReference",
            ValueList = "-",
            DefaultValue = "-"
        }
    ];
}
