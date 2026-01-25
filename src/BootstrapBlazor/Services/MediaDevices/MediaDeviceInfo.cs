// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <inheritdoc/>
/// <para lang="en"><inheritdoc/></para>
/// </summary>
public class MediaDeviceInfo : IMediaDeviceInfo
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string DeviceId { get; set; } = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string GroupId { get; set; } = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Kind { get; set; } = "";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string Label { get; set; } = "";
}
