// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Video Media Device Interface
/// </summary>
public interface IVideoDevice
{
    /// <summary>
    /// Gets the list of video devices.
    /// </summary>
    /// <returns></returns>
    Task<List<IMediaDeviceInfo>?> GetDevices();

    /// <summary>
    /// Opens the video device with the specified constraints.
    /// </summary>
    /// <param name="constraints"></param>
    /// <returns></returns>
    Task Open(MediaTrackConstraints constraints);

    /// <summary>
    /// Close the video device with the specified selector.
    /// </summary>
    /// <param name="videoSelector"></param>
    /// <returns></returns>
    Task Close(string? videoSelector);

    /// <summary>
    /// Capture a still image from the video stream.
    /// </summary>
    /// <returns></returns>
    Task Capture();

    ///// <summary>
    ///// Preview a still image from the video stream.
    ///// </summary>
    ///// <returns></returns>
    //Task Preview();

    ///// <summary>
    ///// Gets the stream of the captured image.
    ///// </summary>
    ///// <returns></returns>
    //Task<Stream?> GetPreviewImage();

    /// <summary>
    /// Gets the preview URL of the captured image.
    /// </summary>
    /// <returns></returns>
    Task<string?> GetPreviewUrl();

    /// <summary>
    /// Flip the video device.
    /// </summary>
    /// <returns></returns>
    Task Flip();
}
