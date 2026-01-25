// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DefaultBluetoothCharacteristic 实现类</para>
/// <para lang="en">DefaultBluetoothCharacteristic Implementation Class</para>
/// </summary>
sealed class DefaultBluetoothCharacteristic : IBluetoothCharacteristic
{
    private readonly JSModule _module;

    private readonly DotNetObjectReference<DefaultBluetoothCharacteristic> _interop;

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
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsNotify { get; set; }

    private Func<byte[], Task>? _notifyCallback;

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
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
    public async Task<bool> StartNotifications(Func<byte[], Task> notificationCallback, CancellationToken token = default)
    {
        if (IsNotify)
        {
            return false;
        }

        ErrorMessage = null;
        var result = await _module.InvokeAsync<bool?>("startNotifications", token, Id, ServiceUUID, UUID, _interop, nameof(OnError), nameof(OnNotification));
        IsNotify = result is true;
        _notifyCallback = notificationCallback;
        return IsNotify;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="token"></param>
    public async Task<bool> StopNotifications(CancellationToken token = default)
    {
        ErrorMessage = null;
        var result = await _module.InvokeAsync<bool?>("stopNotifications", token, Id, UUID);
        var ret = result is true;
        if (ret)
        {
            IsNotify = false;
        }
        return ret;
    }

    [JSInvokable]
    public async Task OnNotification(string uuId, byte[] payload)
    {
        if (_notifyCallback != null)
        {
            await _notifyCallback(payload);
        }
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
}
