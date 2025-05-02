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
    /// <returns></returns>
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
        return deviceService.Open(constraints);
    }

    public Task Close(string? videoSelector)
    {
        return deviceService.Close(videoSelector);
    }

    public Task Capture()
    {
        return deviceService.Capture();
    }

    public Task<string?> GetPreviewUrl()
    {
        return deviceService.GetPreviewUrl();
    }
}
