// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultMediaVideo(IMediaDevices deviceService) : IMediaVideo
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

    public Task Open(MediaTrackConstraints constraints)
    {
        return deviceService.Open(constraints);
    }

    public Task Close()
    {
        return deviceService.Close();
    }

    public Task Capture()
    {
        return deviceService.Capture();
    }

    public Task Preview()
    {
        throw new NotImplementedException();
    }

    public Task<Stream?> GetPreviewImage()
    {
        throw new NotImplementedException();
    }

    public Task<string?> GetPreviewUrl()
    {
        throw new NotImplementedException();
    }
}
