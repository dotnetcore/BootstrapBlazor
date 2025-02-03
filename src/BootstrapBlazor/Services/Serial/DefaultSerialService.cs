// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        var module = await _runtime.LoadModuleByName("serial");

        IsSupport = await module.InvokeAsync<bool>("init", _serialPortId);
        return module;
    }

    /// <summary>
    /// get the current position of the device
    /// </summary>
    /// <returns></returns>
    public async Task<ISerialPort?> GetPort(CancellationToken token = default)
    {
        _module ??= await LoadModule();

        if (IsSupport)
        {
            var ret = await _module.InvokeAsync<bool>("getPort", token, _serialPortId);
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
