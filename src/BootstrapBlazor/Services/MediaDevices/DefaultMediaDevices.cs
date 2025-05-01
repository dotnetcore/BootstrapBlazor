// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultMediaDevices(IJSRuntime jsRuntime) : IMediaDevices
{
    private DotNetObjectReference<DefaultMediaDevices>? _interop = null;
    private JSModule? _module = null;

    private async Task<JSModule> LoadModule()
    {
        _interop ??= DotNetObjectReference.Create(this);
        _module ??= await jsRuntime.LoadModuleByName("media");
        return _module;
    }

    public async Task<IEnumerable<IMediaDeviceInfo>?> EnumerateDevices()
    {
        var module = await LoadModule();
        return await module.InvokeAsync<List<MediaDeviceInfo>?>("enumerateDevices");
    }

    public Task<MediaStream> GetDisplayMedia(DisplayMediaOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public async Task Open(MediaTrackConstraints constraints)
    {
        var module = await LoadModule();
        await module.InvokeVoidAsync("open", constraints);
    }

    public async Task Close(string selector)
    {
        var module = await LoadModule();
        await module.InvokeVoidAsync("close", selector);
    }
}
