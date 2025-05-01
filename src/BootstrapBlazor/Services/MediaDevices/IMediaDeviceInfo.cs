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
    /// Returns a string that is an identifier for the represented device that is persisted across sessions. It is un-guessable by other applications and unique to the origin of the calling application. It is reset when the user clears cookies (for Private Browsing, a different identifier is used that is not persisted across sessions).
    /// </summary>
    public string? DeviceId { get; }

    /// <summary>
    /// Returns a string that is a group identifier. Two devices have the same group identifier if they belong to the same physical device — for example a monitor with both a built-in camera and a microphone.
    /// </summary>
    public string? GroupId { get; }

    /// <summary>
    /// Returns a string that is a group identifier. Two devices have the same group identifier if they belong to the same physical device — for example a monitor with both a built-in camera and a microphone.
    /// </summary>
    public string? Kind { get; }

    /// <summary>
    /// Returns a string that is a group identifier. Two devices have the same group identifier if they belong to the same physical device — for example a monitor with both a built-in camera and a microphone.
    /// </summary>
    public string? Label { get; }
}
