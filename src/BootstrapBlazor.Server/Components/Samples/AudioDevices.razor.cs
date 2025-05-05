// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// AudioDevice Component
/// </summary>
public partial class AudioDevices : IAsyncDisposable
{
    [Inject, NotNull]
    private IAudioDevice? AudioDeviceService { get; set; }

    [Inject, NotNull]
    private DownloadService? DownloadService { get; set; }

    private readonly List<IMediaDeviceInfo> _devices = [];

    private List<SelectedItem> _items = [];

    private string? _deviceId;

    private bool _isOpen = false;

    private bool _isDownload = false;

    private async Task OnRequestDevice()
    {
        var devices = await AudioDeviceService.GetDevices();
        if (devices != null)
        {
            _devices.Clear();
            _devices.AddRange(devices);
            _items = [.. _devices.Select(i => new SelectedItem(i.DeviceId, i.Label))];

            _deviceId = _items.FirstOrDefault()?.Value;
        }
    }

    private async Task OnOpen()
    {
        if (!string.IsNullOrEmpty(_deviceId))
        {
            var constraints = new MediaTrackConstraints
            {
                DeviceId = _deviceId,
                Selector = ".bb-audio"
            };
            _isOpen = await AudioDeviceService.Open(constraints);
        }
    }

    private async Task OnClose()
    {
        await AudioDeviceService.Close(".bb-audio");
        _isOpen = false;
        _isDownload = true;
    }

    private async Task OnDownload()
    {
        var stream = await AudioDeviceService.GetData();
        if (stream != null)
        {
            await DownloadService.DownloadFromStreamAsync($"data_{DateTime.Now:HHmmss}.wav", stream);
        }
    }

    private async Task DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            await OnClose();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
