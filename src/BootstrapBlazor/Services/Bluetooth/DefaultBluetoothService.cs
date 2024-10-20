// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

sealed class DefaultBluetoothService : IBluetoothService
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

    private readonly DotNetObjectReference<DefaultBluetoothService> _interop;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsRuntime"></param>
    public DefaultBluetoothService(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _deviceId = $"bb_bt_{GetHashCode()}";
        _interop = DotNetObjectReference.Create(this);
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
    public async Task<IBluetoothDevice?> RequestDevice(string[] optionalServices, CancellationToken token = default)
    {
        _module ??= await LoadModule();

        BluetoothDevice? device = null;
        if (IsSupport)
        {
            ErrorMessage = null;
            var parameters = await _module.InvokeAsync<string[]?>("requestDevice", token, _deviceId, optionalServices, _interop, nameof(OnError));
            if (parameters != null)
            {
                device = new BluetoothDevice(_module, _deviceId, parameters);
            }
        }
        return device;
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
