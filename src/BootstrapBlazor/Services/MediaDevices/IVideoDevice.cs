// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Video Media Device Interface</para>
/// <para lang="en">Video Media Device Interface</para>
/// </summary>
public interface IVideoDevice
{
    /// <summary>
    /// <para lang="zh">获得 the list of video devices</para>
    /// <para lang="en">Gets the list of video devices</para>
    /// </summary>
    Task<List<IMediaDeviceInfo>?> GetDevices();

    /// <summary>
    /// <para lang="zh">Opens the video device with the specified constraints</para>
    /// <para lang="en">Opens the video device with the specified constraints</para>
    /// </summary>
    /// <param name="constraints"></param>
    Task<bool> Open(MediaTrackConstraints constraints);

    /// <summary>
    /// <para lang="zh">Close the video device with the specified selector</para>
    /// <para lang="en">Close the video device with the specified selector</para>
    /// </summary>
    /// <param name="selector"></param>
    Task<bool> Close(string? selector);

    /// <summary>
    /// <para lang="zh">Capture a still image from the video stream</para>
    /// <para lang="en">Capture a still image from the video stream</para>
    /// </summary>
    Task Capture();

    /// <summary>
    /// <para lang="zh">获得 the preview URL of the captured image</para>
    /// <para lang="en">Gets the preview URL of the captured image</para>
    /// </summary>
    Task<string?> GetPreviewUrl();

    /// <summary>
    /// <para lang="zh">获得 the stream of the captured image</para>
    /// <para lang="en">Gets the stream of the captured image</para>
    /// </summary>
    Task<Stream?> GetPreviewData();

    /// <summary>
    /// <para lang="zh">Apply the media track constraints</para>
    /// <para lang="en">Apply the media track constraints</para>
    /// </summary>
    /// <param name="constraints"></param>
    Task<bool> Apply(MediaTrackConstraints constraints);
}
