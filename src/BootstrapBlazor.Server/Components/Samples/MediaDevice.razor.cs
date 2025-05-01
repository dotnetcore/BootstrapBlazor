// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class MediaDevice
{
    [Inject, NotNull]
    private IMediaDevices? MediaDevices { get; set; }

    private readonly List<MediaDeviceInfo> _devices = [];

    private async Task OnRequestDevice()
    {
        var devices = await MediaDevices.EnumerateDevices();
        if (devices != null)
        {
            _devices.AddRange(devices);
        }
    }
}
