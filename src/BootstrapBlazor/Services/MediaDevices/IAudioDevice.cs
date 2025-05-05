// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Audio Media Device Interface
/// </summary>
public interface IAudioDevice
{
    /// <summary>
    /// Gets the list of audio devices.
    /// </summary>
    /// <returns></returns>
    Task<List<IMediaDeviceInfo>?> GetDevices();

    /// <summary>
    /// Opens the audio device with the specified constraints.
    /// </summary>
    /// <param name="constraints"></param>
    /// <returns></returns>
    Task<bool> Open(MediaTrackConstraints constraints);

    /// <summary>
    /// Close the audio device with the specified selector.
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<bool> Close(string? selector);

    /// <summary>
    /// Gets the stream of the audio.
    /// </summary>
    /// <returns></returns>
    Task<Stream?> GetData();
}
