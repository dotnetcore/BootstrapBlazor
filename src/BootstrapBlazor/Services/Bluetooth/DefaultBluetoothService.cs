// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DefaultBluetoothService 实现类</para>
/// <para lang="en">DefaultBluetoothService Implementation Class</para>
/// </summary>
sealed class DefaultBluetoothService : IBluetoothService
{
    private readonly JSModule _module;

    private readonly DotNetObjectReference<DefaultBluetoothService> _interop;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string UUID { get; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    public string? ErrorMessage { get; private set; }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="module"></param>
    /// <param name="clientId"></param>
    /// <param name="serviceName"></param>
    /// <param name="serviceUUID"></param>
    public DefaultBluetoothService(JSModule module, string clientId, string serviceName, string serviceUUID)
    {
        Name = serviceName;
        Id = clientId;
        UUID = serviceUUID;
        _module = module;
        _interop = DotNetObjectReference.Create(this);
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc /></para>
    /// <para lang="en"><inheritdoc /></para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<IBluetoothCharacteristic>> GetCharacteristics(CancellationToken token = default)
    {
        var ret = new List<IBluetoothCharacteristic>();
        ErrorMessage = null;
        var characteristics = await _module.InvokeAsync<List<string>?>("getCharacteristics", token, Id, Name, _interop, nameof(OnError));
        if (characteristics != null)
        {
            ret.AddRange(characteristics.Select(characteristics => new DefaultBluetoothCharacteristic(_module, Id, Name, characteristics)));
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="characteristicUUID"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IBluetoothCharacteristic?> GetCharacteristic(string characteristicUUID, CancellationToken token = default)
    {
        IBluetoothCharacteristic? characteristic = null;
        ErrorMessage = null;
        var uuId = await _module.InvokeAsync<string?>("getCharacteristic", token, Id, Name, characteristicUUID, _interop, nameof(OnError));
        if (!string.IsNullOrEmpty(uuId))
        {
            characteristic = new DefaultBluetoothCharacteristic(_module, Id, Name, uuId);
        }
        return characteristic;
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
