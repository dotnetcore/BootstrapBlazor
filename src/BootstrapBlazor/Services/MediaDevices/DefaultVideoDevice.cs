// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultVideoDevice(IMediaDevices deviceService) : IVideoDevice
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async Task<List<IMediaDeviceInfo>?> GetDevices()
    {
        var ret = new List<IMediaDeviceInfo>();
        var devices = await deviceService.EnumerateDevices();
        if (devices != null)
        {
            ret.AddRange(devices.Where(d => d.Kind == "videoinput"));
        }
        return ret;
    }

    public Task<bool> Open(MediaTrackConstraints constraints)
    {
        return deviceService.Open("video", constraints);
    }

    public Task<bool> Close(string? selector)
    {
        return deviceService.Close(selector);
    }

    public Task Capture()
    {
        return deviceService.Capture();
    }

    public Task<Stream?> GetPreviewData()
    {
        return deviceService.GetPreviewData();
    }

    public Task<string?> GetPreviewUrl()
    {
        return deviceService.GetPreviewUrl();
    }

    public Task<bool> Apply(MediaTrackConstraints constraints)
    {
        return deviceService.Apply(constraints);
    }
}
