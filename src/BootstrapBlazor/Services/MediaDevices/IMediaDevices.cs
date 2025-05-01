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
    Task<List<MediaDeviceInfo>?> EnumerateDevices();

    /// <summary>
    /// The getDisplayMedia() method of the MediaDevices interface prompts the user to select and grant permission to capture the contents of a display or portion thereof (such as a window) as a MediaStream.
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<MediaStream> GetDisplayMedia(DisplayMediaOptions? options = null);
}
