// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙设备
/// </summary>
class BluetoothDevice : IBluetoothDevice
{
    private JSModule? _module;

    private string? _clientId;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Connected { get; set; }

    /// <summary>
    /// <inheritdoc/>
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
    /// <inheritdoc/>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<string?> GetBatteryValue()
    {
        string? ret = null;
        if (Connected && _module != null)
        {
            ret = await _module.InvokeAsync<string?>("getBatteryValue", _clientId);
        }
        return ret;
    }

    /// <summary>
    /// 设置 Javascript 方法
    /// </summary>
    /// <param name="module"></param>
    /// <param name="clientId"></param>
    public void SetInvoker(JSModule module, string? clientId)
    {
        _module = module;
        _clientId = clientId;
    }
}
