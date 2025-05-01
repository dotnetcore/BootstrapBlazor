// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// The MediaDeviceInfo interface of the Media Capture and Streams API contains information that describes a single media input or output device.
/// </summary>
public interface IMediaDeviceInfo
{
    /// <summary>
    /// The deviceId read-only property of the MediaDeviceInfo interface returns a string that is an identifier for the represented device and is persisted across sessions.
    /// </summary>
    public string DeviceId { get; }

    /// <summary>
    /// The groupId read-only property of the MediaDeviceInfo interface returns a string that is a group identifier.
    /// </summary>
    public string GroupId { get; }

    /// <summary>
    /// The kind read-only property of the MediaDeviceInfo interface returns an enumerated value, that is either "videoinput", "audioinput" or "audiooutput".
    /// </summary>
    public string Kind { get; }

    /// <summary>
    /// The label read-only property of the MediaDeviceInfo interface returns a string describing this device (for example "External USB Webcam").
    /// </summary>
    public string Label { get; }
}
