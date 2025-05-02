// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// VideoDevice Component
/// </summary>
public partial class VideoDevices
{
    [Inject, NotNull]
    private IVideoDevice? VideoDeviceService { get; set; }

    private readonly List<IMediaDeviceInfo> _devices = [];

    private List<SelectedItem> _items = [];

    private string? _deviceId;

    private string? _previewUrl;

    private async Task OnRequestDevice()
    {
        var devices = await VideoDeviceService.GetDevices();
        if (devices != null)
        {
            _devices.Clear();
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
            await VideoDeviceService.Open(constraints);
        }
    }

    private async Task OnCloseVideo()
    {
        _previewUrl = "";
        await VideoDeviceService.Close(".bb-video");
    }

    private async Task OnCapture()
    {
        _previewUrl = await VideoDeviceService.GetPreviewUrl();
    }

    private async Task OnFlip()
    {
        await VideoDeviceService.Flip();
    }
}
