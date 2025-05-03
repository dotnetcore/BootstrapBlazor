// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// The MediaDevices interface of the Media Capture and Streams API provides access to connected media input devices like cameras and microphones, as well as screen sharing. In essence, it lets you obtain access to any hardware source of media data.
/// </summary>
public interface IMediaDevices
{
    /// <summary>
    /// An array of MediaDeviceInfo objects. Each object in the array describes one of the available media input and output devices.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<IMediaDeviceInfo>?> EnumerateDevices();

    /// <summary>
    /// The open() method of the MediaDevices interface creates a new MediaStream object and starts capturing media from the specified device.
    /// </summary>
    /// <param name="constraints"></param>
    /// <returns></returns>
    Task<bool> Open(MediaTrackConstraints constraints);

    /// <summary>
    /// The close() method of the MediaDevices interface stops capturing media from the specified device and closes the MediaStream object.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<bool> Close(string? selector);

    /// <summary>
    /// The capture() method of the MediaDevices interface captures a still image from the specified video stream and saves it to the specified location.
    /// </summary>
    /// <returns></returns>
    Task Capture();

    /// <summary>
    /// Gets the preview URL of the captured image.
    /// </summary>
    /// <returns></returns>
    Task<string?> GetPreviewUrl();

    /// <summary>
    /// Apply the media track constraints.
    /// </summary>
    /// <param name="constraints"></param>
    /// <returns></returns>
    Task<bool> Apply(MediaTrackConstraints constraints);
}
