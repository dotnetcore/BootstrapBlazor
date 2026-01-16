// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">节日接口</para>
///  <para lang="en">Festivals Interface</para>
/// </summary>
public interface ICalendarFestivals
{
    /// <summary>
    ///  <para lang="zh">获得 节日键值对</para>
    ///  <para lang="en">Get Festival Key-Value Pair</para>
    /// </summary>
    /// <returns></returns>
    string? GetFestival(DateTime dt);
}
