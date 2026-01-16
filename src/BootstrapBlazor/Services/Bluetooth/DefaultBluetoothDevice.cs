// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">蓝牙设备</para>
/// <para lang="en">Bluetooth Device</para>
/// </summary>
sealed class DefaultBluetoothDevice : IBluetoothDevice
{
    private readonly JSModule _module;

    private readonly string _clientId;

    private readonly DotNetObjectReference<DefaultBluetoothDevice> _interop;

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

    public DefaultBluetoothDevice(JSModule module, string clientId, string[] args)
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
    /// <para lang="zh"><inheritdoc />
    ///</para>
    /// <para lang="en"><inheritdoc />
    ///</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<IBluetoothService>> GetPrimaryServices(CancellationToken token = default)
    {
        var ret = new List<IBluetoothService>();
        if (Connected)
        {
            ErrorMessage = null;
            var services = await _module.InvokeAsync<List<string>?>("getPrimaryServices", token, _clientId, _interop, nameof(OnError));
            if (services != null)
            {
                ret.AddRange(services.Select(serviceName => new DefaultBluetoothService(_module, _clientId, serviceName, serviceName)));
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="serviceUUID"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IBluetoothService?> GetPrimaryService(string serviceUUID, CancellationToken token = default)
    {
        IBluetoothService? ret = null;
        if (Connected)
        {
            ErrorMessage = null;
            var uuId = await _module.InvokeAsync<string?>("getPrimaryService", token, _clientId, serviceUUID, _interop, nameof(OnError));
            if (!string.IsNullOrEmpty(uuId))
            {
                ret = new DefaultBluetoothService(_module, _clientId, serviceUUID, uuId);
            }
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
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
    /// <para lang="zh"><inheritdoc />
    ///</para>
    /// <para lang="en"><inheritdoc />
    ///</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<DateTimeOffset?> GetCurrentTime(CancellationToken token = default)
    {
        DateTimeOffset? ret = null;
        if (Connected)
        {
            ErrorMessage = null;
            var timeString = await _module.InvokeAsync<string?>("getCurrentTime", token, _clientId, _interop, nameof(OnError));
            if (DateTimeOffset.TryParseExact(timeString, "yyyy-MM-ddTHH:mm:sszzz", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out var d))
            {
                ret = d;
            }
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc />
    ///</para>
    /// <para lang="en"><inheritdoc />
    ///</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<byte?> GetBatteryValue(CancellationToken token = default)
    {
        byte? ret = null;
        if (Connected)
        {
            ErrorMessage = null;
            var data = await _module.InvokeAsync<byte[]?>("readValue", token, _clientId, "battery_service", "battery_level", _interop, nameof(OnError));
            if (data is { Length: > 0 })
            {
                ret = data[0];
            }
        }
        return ret;
    }


    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<byte[]?> ReadValue(string serviceUUID, string characteristicUUID, CancellationToken token = default)
    {
        byte[]? ret = null;
        if (Connected)
        {
            ErrorMessage = null;
            ret = await _module.InvokeAsync<byte[]?>("readValue", token, _clientId, serviceUUID, characteristicUUID, _interop, nameof(OnError));
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">JavaScript 报错回调方法</para>
    /// <para lang="en">JavaScript Error Callback Method</para>
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
