// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultBluetoothService : IBluetoothService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool IsSupport { get; set; }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool IsAvailable { get; set; }

    [NotNull]
    private JSModule? _module = null;

    private readonly IJSRuntime _runtime;

    private readonly string _deviceId;

    public DefaultBluetoothService(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _deviceId = $"bb_bt_{GetHashCode()}";
    }

    private async Task<JSModule> LoadModule()
    {
        var module = await _runtime.LoadModule("./_content/BootstrapBlazor/modules/bt.js");

        IsSupport = await module.InvokeAsync<bool>("init");
        return module;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public async Task<bool> GetAvailability()
    {
        _module ??= await LoadModule();

        var ret = false;
        if (IsSupport)
        {
            ret = await _module.InvokeAsync<bool>("getAvailability", _deviceId);
            IsAvailable = ret;
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public async Task<IBluetoothDevice?> RequestDevice(string[] optionalServices)
    {
        BluetoothDevice? device = null;
        if (IsAvailable)
        {
            device = await _module.InvokeAsync<BluetoothDevice?>("requestDevice", _deviceId, optionalServices);
            device?.SetInvoker(_module, _deviceId);
        }
        return device;
    }
}
