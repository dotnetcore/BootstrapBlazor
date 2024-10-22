// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

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
    /// <inheritdoc />
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
