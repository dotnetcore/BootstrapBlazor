// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

sealed class DefaultBluetooth : IBluetooth
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool IsSupport { get; private set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool IsAvailable { get; private set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public string? ErrorMessage { get; private set; }

    [NotNull]
    private JSModule? _module = null;

    private readonly IJSRuntime _runtime;

    private readonly string _deviceId;

    private readonly DotNetObjectReference<DefaultBluetooth> _interop;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsRuntime"></param>
    public DefaultBluetooth(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _deviceId = $"bb_bt_{GetHashCode()}";
        _interop = DotNetObjectReference.Create(this);
    }

    private async Task<JSModule> LoadModule()
    {
        var module = await _runtime.LoadModuleByName("bt");

        IsSupport = await module.InvokeAsync<bool>("init");
        return module;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> GetAvailability(CancellationToken token = default)
    {
        _module ??= await LoadModule();

        var ret = false;
        if (IsSupport)
        {
            ret = await _module.InvokeAsync<bool>("getAvailability", token);
            IsAvailable = ret;
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public async Task<IBluetoothDevice?> RequestDevice(BluetoothRequestOptions? options = null, CancellationToken token = default)
    {
        _module ??= await LoadModule();

        DefaultBluetoothDevice? device = null;
        if (IsSupport)
        {
            ErrorMessage = null;
            var parameters = await _module.InvokeAsync<string[]?>("requestDevice", token, _deviceId, options, _interop, nameof(OnError));
            if (parameters != null)
            {
                device = new DefaultBluetoothDevice(_module, _deviceId, parameters);
            }
        }
        return device;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="optionalServices"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<IBluetoothDevice?> RequestDevice(List<string> optionalServices, CancellationToken token = default)
    {
        var options = new BluetoothRequestOptions() { AcceptAllDevices = true, OptionalServices = optionalServices };
        return RequestDevice(options, token);
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
