// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultGeoLocationService : IGeoLocationService
{
    private IJSRuntime JSRuntime { get; }

    private JSModule? Module { get; set; }

    private DotNetObjectReference<DefaultGeoLocationService> Interop { get; }

    private GeolocationPosition? LastPosition { get; set; }

    private long WatchId { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsRuntime"></param>
    public DefaultGeoLocationService(IJSRuntime jsRuntime)
    {
        JSRuntime = jsRuntime;

        Interop = DotNetObjectReference.Create(this);
    }

    private Task<JSModule> LoadModule() => JSRuntime.LoadModuleByName("geo");

    /// <summary>
    /// get the current position of the device
    /// </summary>
    /// <returns></returns>
    public async Task<GeolocationPosition?> GetPositionAsync()
    {
        Module ??= await LoadModule();
        return await Module.InvokeAsync<GeolocationPosition>("getPosition");
    }

    private Func<GeolocationPosition, Task>? WatchPositionCallback { get; set; }

    /// <summary>
    /// register a handler function that will be called automatically each time the position of the device changes
    /// </summary>
    /// <returns></returns>
    public async ValueTask<long> WatchPositionAsync(Func<GeolocationPosition, Task> callback)
    {
        Module ??= await LoadModule();
        if (WatchId != 0)
        {
            await ClearWatchPositionAsync(WatchId);
        }
        WatchPositionCallback = callback;
        WatchId = await Module.InvokeAsync<long>("watchPosition", Interop, nameof(WatchCallback));
        return WatchId;
    }

    /// <summary>
    /// unregister location/error monitoring handlers previously installed using <see cref="WatchPositionAsync"/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> ClearWatchPositionAsync(long id)
    {
        Module ??= await LoadModule();
        return await Module.InvokeAsync<bool>("clearWatchLocation", id);
    }

    /// <summary>
    /// 获得 当前设备地理位置由 JS 调用
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    [JSInvokable]
    public async Task WatchCallback(GeolocationPosition position)
    {
        LastPosition = position;

        if (WatchPositionCallback != null)
        {
            await WatchPositionCallback(LastPosition);
        }
    }

    /// <summary>
    /// DisposeAsync 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (WatchId != 0)
            {
                await ClearWatchPositionAsync(WatchId);
            }

            Interop.Dispose();

            if (Module != null)
            {
                await Module.DisposeAsync();
                Module = null;
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
