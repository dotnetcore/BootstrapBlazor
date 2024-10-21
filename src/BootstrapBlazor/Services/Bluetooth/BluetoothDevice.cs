// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙设备
/// </summary>
sealed class BluetoothDevice : IBluetoothDevice
{
    private readonly JSModule _module;

    private readonly string _clientId;

    private readonly DotNetObjectReference<BluetoothDevice> _interop;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Name { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Id { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Connected { get; private set; }

    public BluetoothDevice(JSModule module, string clientId, string[] args)
    {
        _module = module;
        _clientId = clientId;
        _interop = DotNetObjectReference.Create(this);

        if (args.Length == 2)
        {
            Name = args[0];
            Id = args[1];
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Connect(CancellationToken token = default)
    {
        if (Connected == false)
        {
            ErrorMessage = null;
            Connected = await _module.InvokeAsync<bool>("connect", token, _clientId, _interop, nameof(OnError));
        }
        return Connected;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<bool> Disconnect(CancellationToken token = default)
    {
        var ret = false;
        if (Connected)
        {
            ErrorMessage = null;
            ret = await _module.InvokeAsync<bool>("disconnect", token, _clientId, _interop, nameof(OnError));
            if (ret)
            {
                Connected = false;
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<byte[]?> ReadValue(string serviceName, string characteristicName, CancellationToken token = default)
    {
        byte[]? ret = null;
        if (Connected)
        {
            ErrorMessage = null;
            ret = await _module.InvokeAsync<byte[]?>("readValue", token, _clientId, serviceName, characteristicName, _interop, nameof(OnError));
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<BluetoothDeviceInfo?> GetDeviceInfo(CancellationToken token = default)
    {
        BluetoothDeviceInfo? ret = null;
        if (Connected)
        {
            ErrorMessage = null;
            ret = await _module.InvokeAsync<BluetoothDeviceInfo?>("getDeviceInfo", token, _clientId, _interop, nameof(OnError));
        }
        return ret;
    }

    /// <summary>
    /// JavaScript 报错回调方法
    /// </summary>
    /// <param name="message"></param>
    [JSInvokable]
    public void OnError(string message)
    {
        ErrorMessage = message;
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await _module.InvokeVoidAsync("dispose", _clientId);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
