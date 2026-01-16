// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">MediaDeviceInfo interface of the Media Capture and Streams API contains information that describes a single media input or output device.</para>
///  <para lang="en">The MediaDeviceInfo interface of the Media Capture and Streams API contains information that describes a single media input or output device.</para>
/// </summary>
public interface IMediaDeviceInfo
{
    /// <summary>
    ///  <para lang="zh">deviceId read-only 属性 of the MediaDeviceInfo interface returns a string that is an identifier for the represented device and is persisted across sessions.</para>
    ///  <para lang="en">The deviceId read-only property of the MediaDeviceInfo interface returns a string that is an identifier for the represented device and is persisted across sessions.</para>
    /// </summary>
    public string DeviceId { get; }

    /// <summary>
    ///  <para lang="zh">groupId read-only 属性 of the MediaDeviceInfo interface returns a string that is a group identifier.</para>
    ///  <para lang="en">The groupId read-only property of the MediaDeviceInfo interface returns a string that is a group identifier.</para>
    /// </summary>
    public string GroupId { get; }

    /// <summary>
    ///  <para lang="zh">kind read-only 属性 of the MediaDeviceInfo interface returns an 枚举erated value, that is either "videoinput", "audioinput" or "audiooutput".</para>
    ///  <para lang="en">The kind read-only property of the MediaDeviceInfo interface returns an enumerated value, that is either "videoinput", "audioinput" or "audiooutput".</para>
    /// </summary>
    public string Kind { get; }

    /// <summary>
    ///  <para lang="zh">label read-only 属性 of the MediaDeviceInfo interface returns a string describing this device (for example "External USB Webcam").</para>
    ///  <para lang="en">The label read-only property of the MediaDeviceInfo interface returns a string describing this device (for example "External USB Webcam").</para>
    /// </summary>
    public string Label { get; }
}
