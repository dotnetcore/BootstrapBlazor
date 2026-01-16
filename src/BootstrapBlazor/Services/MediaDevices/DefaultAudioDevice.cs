// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

class DefaultAudioDevice(IMediaDevices deviceService) : IAudioDevice
{
    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    public async Task<List<IMediaDeviceInfo>?> GetDevices()
    {
        var ret = new List<IMediaDeviceInfo>();
        var devices = await deviceService.EnumerateDevices();
        if (devices != null)
        {
            ret.AddRange(devices.Where(d => d.Kind == "audioinput"));
        }
        return ret;
    }

    public Task<bool> Open(MediaTrackConstraints constraints)
    {
        return deviceService.Open("audio", constraints);
    }

    public Task<bool> Close(string? selector)
    {
        return deviceService.Close(selector);
    }

    public Task<Stream?> GetData()
    {
        return deviceService.GetAudioData();
    }
}
