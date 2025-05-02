// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// The MediaTrackConstraints interface of the Media Capture and Streams API is used to specify constraints on the media tracks that are requested from a media device. It is used in conjunction with the getUserMedia() method to specify the desired properties of the media tracks, such as resolution, frame rate, and aspect ratio.
/// </summary>
public class MediaTrackConstraints
{
    /// <summary>
    /// 
    /// </summary>
    public string DeviceId { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string? VideoSelector { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int? Height { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string? FacingMode { get; set; }
}
