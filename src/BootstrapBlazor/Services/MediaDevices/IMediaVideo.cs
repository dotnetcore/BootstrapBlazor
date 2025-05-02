// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public interface IMediaVideo
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<List<IMediaDeviceInfo>?> GetDevices();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="constraints"></param>
    /// <returns></returns>
    Task Open(MediaTrackConstraints constraints);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task Close(string selector);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selector"></param>
    /// <returns></returns>
    Task Capture(string selector);
}
