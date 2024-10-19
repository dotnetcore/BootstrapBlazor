// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙设备
/// </summary>
public class BluetoothDevice
{
    private JSModule? _module;

    private string? _clientId;

    /// <summary>
    /// 获得 设备名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 获得 设备 Id
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 获得 当前设备连接状态
    /// </summary>
    public bool Connected { get; private set; }

    /// <summary>
    /// 连接方法
    /// </summary>
    /// <returns></returns>
    public async Task Connect()
    {
        if (Connected == false && _module != null)
        {
            Connected = await _module.InvokeAsync<bool>("connect", _clientId);
        }
    }

    /// <summary>
    /// 断开连接方法
    /// </summary>
    /// <returns></returns>
    public async Task Disconnect()
    {
        if (Connected && _module != null)
        {
            var ret = await _module.InvokeAsync<bool>("disconnect", _clientId);
            if (ret)
            {
                Connected = false;
            }
        }
    }

    public Task GetPrimaryService() { return Task.CompletedTask; }

    internal void SetInvoker(JSModule module, string? clientId)
    {
        _module = module;
        _clientId = clientId;
    }
}
