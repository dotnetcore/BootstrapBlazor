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
    private IVideoDevice? MediaVideoService { get; set; }

    private readonly List<IMediaDeviceInfo> _devices = [];

    private List<SelectedItem> _items = [];

    private string? _deviceId;

    private async Task OnRequestDevice()
    {
        var devices = await MediaVideoService.GetDevices();
        if (devices != null)
        {
            _devices.AddRange(devices);
            _items = [.. _devices.Select(i => new SelectedItem(i.DeviceId, i.Label))];
        }
    }

    private async Task OnOpenVideo()
    {
        if (!string.IsNullOrEmpty(_deviceId))
        {
            var constraints = new MediaTrackConstraints
            {
                DeviceId = _deviceId,
                VideoSelector = ".bb-video"
            };
            await MediaVideoService.Open(constraints);
        }
    }

    private async Task OnCloseVideo()
    {
        await MediaVideoService.Close(".bb-video");
    }

    private async Task OnCapture()
    {
        await Task.Delay(1);
        //await VideoDeviceService.Capture(".bb-video");
    }
}
