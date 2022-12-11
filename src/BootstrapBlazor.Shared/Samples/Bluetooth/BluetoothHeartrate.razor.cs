// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class BluetoothHeartRate
{
    Heartrate heartrate { get; set; } = new Heartrate();

    private string? message;
    private int? value;
    private string? statusmessage;
    private string? errmessage;

    private Task OnResult(string message)
    {
        this.message = message;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnUpdateValue(int value)
    {
        this.value = value;
        this.statusmessage = $"心率{value}";
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

    /// <summary>
    /// 获取心率
    /// </summary>
    public async void GetHeartrate()
    {
        await heartrate.GetHeartrate();
    }

    /// <summary>
    /// 停止获取心率
    /// </summary>
    public async void StopHeartrate()
    {
        await heartrate.StopHeartrate();
    } 
}
