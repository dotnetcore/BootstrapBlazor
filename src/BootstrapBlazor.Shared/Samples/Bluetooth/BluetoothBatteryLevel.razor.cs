// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class BluetoothBatteryLevel
{
    BatteryLevel batteryLevel { get; set; } = new BatteryLevel();

    private decimal? value = 0;
    private string? message;
    private string? statusmessage;
    private string? errmessage;

    private Task OnResult(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateValue(decimal value)
    {
        this.value = value;
        this.statusmessage = $"设备电量{value}%";
        StateHasChanged();
        return Task.CompletedTask;
    }


    private Task OnUpdateStatus(BluetoothDevice device)
    {
        this.statusmessage = device.Status;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        this.errmessage = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    public async void GetBatteryLevel()
    {
        await batteryLevel.GetBatteryLevel();
    }
 
}
