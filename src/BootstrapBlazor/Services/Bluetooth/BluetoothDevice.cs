// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 蓝牙设备
/// </summary>
class BluetoothDevice : IBluetoothDevice
{
    private readonly JSModule _module;

    private readonly string _clientId;

    private readonly DotNetObjectReference<BluetoothDevice> _interop;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Name { get; private set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? Id { get; private set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool Connected { get; set; }

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
        if (Connected == false && _module != null)
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
        if (Connected && _module != null)
        {
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
        if (Connected && _module != null)
        {
            var buffer = await _module.InvokeAsync<IJSStreamReference?>("readValue", token, _clientId, serviceName, characteristicName, _interop, nameof(OnError));
            if (buffer != null)
            {
                using var stream = await buffer.OpenReadStreamAsync(buffer.Length, token);
                var data = new byte[stream.Length];
                var length = await stream.ReadAsync(data, token);
                if (length > 0)
                {
                    ret = data;
                }
            }
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
