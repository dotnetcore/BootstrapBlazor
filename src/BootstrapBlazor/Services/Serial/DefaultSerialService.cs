// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultSerialService : ISerialService, IAsyncDisposable
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsSupport { get; set; }

    private JSModule? _module;

    private readonly IJSRuntime _runtime;

    private readonly string _serialPortId;

    private SerialPort? _serialPort;

    public DefaultSerialService(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _serialPortId = $"bb_serial_{GetHashCode()}";
    }

    private async Task<JSModule> LoadModule()
    {
        var module = await _runtime.LoadModule("./_content/BootstrapBlazor/modules/serial.js");

        IsSupport = await module.InvokeAsync<bool>("init", _serialPortId);
        return module;
    }

    /// <summary>
    /// get the current position of the device
    /// </summary>
    /// <returns></returns>
    public async Task<ISerialPort?> GetPort()
    {
        _module ??= await LoadModule();

        if (IsSupport)
        {
            var ret = await _module.InvokeAsync<bool>("getPort", _serialPortId);
            if (ret)
            {
                _serialPort = new SerialPort(_module, _serialPortId);
            }
        }
        return _serialPort;
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (_module != null)
            {
                await _module.InvokeVoidAsync("dispose", _serialPortId);
                await _module.DisposeAsync();
                _module = null;
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
