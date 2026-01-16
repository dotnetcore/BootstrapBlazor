// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">网络状态服务</para>
/// <para lang="en">Network State Service</para>
/// </summary>
class DefaultNetowrkMonitorService : INetworkMonitorService, IAsyncDisposable
{
    [NotNull]
    private JSModule? _module = null;
    [NotNull]
    private JSModule? _networkModule = null;
    private readonly IJSRuntime _runtime;
    private readonly DotNetObjectReference<DefaultNetowrkMonitorService> _interop;
    private readonly ConcurrentDictionary<IComponent, Func<NetworkMonitorState, Task>> _callbacks = new();
    private bool _init = false;
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
    private NetworkMonitorState? _state;

    public DefaultNetowrkMonitorService(IJSRuntime jsRuntime)
    {
        _runtime = jsRuntime;
        _interop = DotNetObjectReference.Create(this);
    }

    private Task<JSModule> LoadModule() => _runtime.LoadUtility();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<NetworkMonitorState?> GetNetworkMonitorState(CancellationToken token = default)
    {
        _module ??= await LoadModule();
        return await _module.InvokeAsync<NetworkMonitorState?>("getNetworkInfo", token);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="component"></param>
    /// <param name="callback"></param>
    public async Task RegisterStateChangedCallback(IComponent component, Func<NetworkMonitorState, Task> callback)
    {
        _callbacks.AddOrUpdate(component, key => callback, (k, v) => callback);
        if (_state != null)
        {
            await callback(_state);
        }

        if (!_init)
        {
            await _semaphoreSlim.WaitAsync(3000);
            if (!_init)
            {
                try
                {
                    _init = true;

                    _networkModule ??= await _runtime.LoadModuleByName("net");
                    await _networkModule.InvokeVoidAsync("init", new
                    {
                        Invoke = _interop,
                        OnNetworkStateChangedCallback = nameof(TriggerNetworkStateChanged)
                    });
                }
                finally
                {
                    _semaphoreSlim.Release();
                }
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="component"></param>
    public void UnregisterStateChangedCallback(IComponent component)
    {
        _callbacks.TryRemove(component, out _);
    }

    /// <summary>
    /// <para lang="zh">JSInvoke 回调方法</para>
    /// <para lang="en">JSInvoke Callback Method</para>
    /// </summary>
    /// <returns></returns>
    [JSInvokable]
    public async Task TriggerNetworkStateChanged(NetworkMonitorState state)
    {
        _state = state;
        foreach (var callback in _callbacks.Values)
        {
            if (callback != null)
            {
                await callback(state);
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (_module != null)
        {
            await _module.DisposeAsync();
            _module = null;
        }
        if (_networkModule != null)
        {
            await _networkModule.DisposeAsync();
            _networkModule = null;
        }
        _interop.Dispose();
    }
}
