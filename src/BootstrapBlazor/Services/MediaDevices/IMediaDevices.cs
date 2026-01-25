// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">MediaDevices interface of the Media Capture and Streams API provides access to connected media input devices like cameras and microphones, as well as screen sharing. In essence, it lets you obtain access to any hardware source of media 数据.</para>
/// <para lang="en">The MediaDevices interface of the Media Capture and Streams API provides access to connected media input devices like cameras and microphones, as well as screen sharing. In essence, it lets you obtain access to any hardware source of media data.</para>
/// </summary>
public interface IMediaDevices
{
    /// <summary>
    /// <para lang="zh">An array of MediaDeviceInfo objects. Each object in the array describes one of the available media input and output devices.</para>
    /// <para lang="en">An array of MediaDeviceInfo objects. Each object in the array describes one of the available media input and output devices.</para>
    /// </summary>
    Task<IEnumerable<IMediaDeviceInfo>?> EnumerateDevices();

    /// <summary>
    /// <para lang="zh">open() method of the MediaDevices interface creates a new MediaStream object and starts capturing media from the specified device.</para>
    /// <para lang="en">The open() method of the MediaDevices interface creates a new MediaStream object and starts capturing media from the specified device.</para>
    /// </summary>
    /// <param name="type">video or audio</param>
    /// <param name="constraints"></param>
    Task<bool> Open(string type, MediaTrackConstraints constraints);

    /// <summary>
    /// <para lang="zh">close() method of the MediaDevices interface stops capturing media from the specified device and closes the MediaStream object.</para>
    /// <para lang="en">The close() method of the MediaDevices interface stops capturing media from the specified device and closes the MediaStream object.</para>
    /// </summary>
    /// <param name="selector"></param>
    Task<bool> Close(string? selector);

    /// <summary>
    /// <para lang="zh">capture() method of the MediaDevices interface captures a still image from the specified video stream and saves it to the specified location.</para>
    /// <para lang="en">The capture() method of the MediaDevices interface captures a still image from the specified video stream and saves it to the specified location.</para>
    /// </summary>
    Task Capture();

    /// <summary>
    /// <para lang="zh">获得 the preview URL of the captured image.</para>
    /// <para lang="en">Gets the preview URL of the captured image.</para>
    /// </summary>
    Task<string?> GetPreviewUrl();

    /// <summary>
    /// <para lang="zh">获得 the stream of the captured image.</para>
    /// <para lang="en">Gets the stream of the captured image.</para>
    /// </summary>
    Task<Stream?> GetPreviewData();

    /// <summary>
    /// <para lang="zh">Apply the media track constraints.</para>
    /// <para lang="en">Apply the media track constraints.</para>
    /// </summary>
    /// <param name="constraints"></param>
    Task<bool> Apply(MediaTrackConstraints constraints);

    /// <summary>
    /// <para lang="zh">获得 the stream of the audio.</para>
    /// <para lang="en">Gets the stream of the audio.</para>
    /// </summary>
    Task<Stream?> GetAudioData();
}
