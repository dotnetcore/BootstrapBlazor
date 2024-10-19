// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultBluetoothService : IBluetoothService
{
    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public bool IsSupport { get; }

    private JSModule? _module;

    private readonly IJSRuntime _runtime;

    private readonly string _bluetoothId;

    public DefaultSerialService(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _bluetoothId = $"bb_serial_{GetHashCode()}";
    }

    private async Task<JSModule> LoadModule()
    {
        var module = await _runtime.LoadModule("./_content/BootstrapBlazor/modules/serial.js");

        IsSupport = await module.InvokeAsync<bool>("init", _bluetoothId);
        return module;
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    public Task<bool> GetAvailability()
    {

    }
}
