// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultMediaDevices(IJSRuntime jsRuntime) : IMediaDevices
{
    private JSModule? _module = null;

    private async Task<JSModule> LoadModule()
    {
        _module ??= await jsRuntime.LoadModuleByName("media");
        return _module;
    }

    public async Task<IEnumerable<IMediaDeviceInfo>?> EnumerateDevices()
    {
        var module = await LoadModule();
        return await module.InvokeAsync<List<MediaDeviceInfo>?>("enumerateDevices");
    }

    public async Task<bool> Open(string type, MediaTrackConstraints constraints)
    {
        var module = await LoadModule();
        return await module.InvokeAsync<bool>("open", type, constraints);
    }

    public async Task<bool> Close(string? selector)
    {
        var module = await LoadModule();
        return await module.InvokeAsync<bool>("close", selector);
    }

    public async Task Capture()
    {
        var module = await LoadModule();
        await module.InvokeVoidAsync("capture");
    }

    public async Task<string?> GetPreviewUrl()
    {
        var module = await LoadModule();
        return await module.InvokeAsync<string?>("getPreviewUrl");
    }

    public async Task<Stream?> GetPreviewData()
    {
        Stream? ret = null;
        var module = await LoadModule();
        var stream = await module.InvokeAsync<IJSStreamReference?>("getPreviewData");
        if (stream != null)
        {
            ret = await stream.OpenReadStreamAsync(stream.Length);
        }
        return ret;
    }

    public async Task<bool> Apply(MediaTrackConstraints constraints)
    {
        var module = await LoadModule();
        return await module.InvokeAsync<bool>("apply", constraints);
    }

    public async Task<Stream?> GetAudioData()
    {
        Stream? ret = null;
        var module = await LoadModule();
        var stream = await module.InvokeAsync<IJSStreamReference?>("getAudioData");
        if (stream != null)
        {
            ret = await stream.OpenReadStreamAsync(stream.Length);
        }
        return ret;
    }
}
