﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultGeoLocationService : IGeoLocationService
{
    private IJSRuntime JSRuntime { get; }

    private JSModule? Module { get; set; }

    private DotNetObjectReference<DefaultGeoLocationService>? Interop { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jsRuntime"></param>
    public DefaultGeoLocationService(IJSRuntime jsRuntime)
    {
        JSRuntime = jsRuntime;

        Interop = DotNetObjectReference.Create(this);
    }

    private Task<JSModule> LoadModule() => JSRuntime.LoadModule("./_content/BootstrapBlazor/modules/geo.js", false);

    private GeolocationPosition? LastPosition { get; set; }

    [NotNull]
    private TaskCompletionSource? GetPositionTaskSource { get; set; }

    /// <summary>
    /// get the current position of the device
    /// </summary>
    /// <returns></returns>
    public async Task<GeolocationPosition?> GetPositionAsync()
    {
        Module ??= await LoadModule();
        GetPositionTaskSource = new TaskCompletionSource();
        LastPosition = null;
        var result = await Module.InvokeAsync<bool>("getPosition", Interop, nameof(GeolocationPositionCallback));
        if (result)
        {
            await GetPositionTaskSource.Task;
        }
        return LastPosition;
    }

    /// <summary>
    /// 获得 当前设备地理位置由 JS 调用
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task GeolocationPositionCallback(GeolocationPosition position)
    {
        LastPosition = position;
        GetPositionTaskSource.SetResult();
        return Task.CompletedTask;
    }

    /// <summary>
    /// register a handler function that will be called automatically each time the position of the device changes
    /// </summary>
    /// <returns></returns>
    public async ValueTask<long> GetWatchPositionItemAsync()
    {
        Module ??= await LoadModule();
        return await Module.InvokeAsync<long>("watchPosition", Interop);
    }

    /// <summary>
    /// unregister location/error monitoring handlers previously installed using <see cref="GetWatchPositionItemAsync"/>
    /// </summary>
    /// <returns></returns>
    public async ValueTask<bool> SetClearWatchPositionAsync(long id)
    {
        Module ??= await LoadModule();
        return await Module.InvokeAsync<bool>("clearWatchLocation", id);
    }
}
