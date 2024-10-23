// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// DefaultBluetoothCharacteristic 实现类
/// </summary>
sealed class DefaultBluetoothCharacteristic : IBluetoothCharacteristic
{
    private readonly JSModule _module;

    private readonly DotNetObjectReference<DefaultBluetoothCharacteristic> _interop;

    private readonly Dictionary<string, Func<byte[], Task>> _callbackCache = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string ServiceUUID { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string UUID { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="module"></param>
    /// <param name="clientId"></param>
    /// <param name="serviceUUID"></param>
    /// <param name="characteristicUUID"></param>
    public DefaultBluetoothCharacteristic(JSModule module, string clientId, string serviceUUID, string characteristicUUID)
    {
        ServiceUUID = serviceUUID;
        UUID = characteristicUUID;
        Id = clientId;

        _module = module;
        _interop = DotNetObjectReference.Create(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async Task<byte[]?> ReadValue(CancellationToken token = default)
    {
        byte[]? ret = null;
        ErrorMessage = null;
        ret = await _module.InvokeAsync<byte[]?>("readValue", token, Id, ServiceUUID, UUID, _interop, nameof(OnError));
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="notificationCallback"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> StartNotifications(Func<byte[], Task> notificationCallback, CancellationToken token = default)
    {
        if (_callbackCache.TryGetValue(UUID, out _))
        {
            ErrorMessage = $"the {UUID} characteristic already started.";
            return false;
        }

        var ret = false;
        ErrorMessage = null;
        var result = await _module.InvokeAsync<bool?>("startNotifications", token, Id, ServiceUUID, UUID, _interop, nameof(OnError), nameof(OnNotification));
        ret = result is true;
        if (ret)
        {
            _callbackCache.TryAdd(UUID, notificationCallback);
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> StopNotifications(CancellationToken token = default)
    {
        var ret = false;
        if (_callbackCache.TryGetValue(UUID, out _))
        {
            ErrorMessage = null;
            var result = await _module.InvokeAsync<bool?>("stopNotifications", token, Id, UUID);
            ret = result is true;
            if (ret)
            {
                _callbackCache.Remove(UUID);
            }
        }
        return ret;
    }

    [JSInvokable]
    public async Task OnNotification(string uuId, byte[] payload)
    {
        if (_callbackCache.TryGetValue(uuId, out var notificationCallback))
        {
            await notificationCallback(payload);
        }
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
}
