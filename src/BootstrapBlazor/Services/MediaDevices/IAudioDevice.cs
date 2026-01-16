// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Audio Media Device Interface
///</para>
/// <para lang="en">Audio Media Device Interface
///</para>
/// </summary>
public interface IAudioDevice
{
    /// <summary>
    /// <para lang="zh">获得 the list of audio devices.
    ///</para>
    /// <para lang="en">Gets the list of audio devices.
    ///</para>
    /// </summary>
    /// <returns></returns>
    Task<List<IMediaDeviceInfo>?> GetDevices();

    /// <summary>
    /// <para lang="zh">Opens the audio device with the specified constraints.
    ///</para>
    /// <para lang="en">Opens the audio device with the specified constraints.
    ///</para>
    /// </summary>
    /// <param name="constraints"></param>
    /// <returns></returns>
    Task<bool> Open(MediaTrackConstraints constraints);

    /// <summary>
    /// <para lang="zh">Close the audio device with the specified selector.
    ///</para>
    /// <para lang="en">Close the audio device with the specified selector.
    ///</para>
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task<bool> Close(string? selector);

    /// <summary>
    /// <para lang="zh">获得 the stream of the audio.
    ///</para>
    /// <para lang="en">Gets the stream of the audio.
    ///</para>
    /// </summary>
    /// <returns></returns>
    Task<Stream?> GetData();
}
