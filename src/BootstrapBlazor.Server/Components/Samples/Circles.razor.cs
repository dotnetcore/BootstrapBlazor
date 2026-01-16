// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Circles
/// </summary>
public sealed partial class Circles
{
    private int _circleValue = 0;

    private void Add(int interval)
    {
        _circleValue += interval;
        _circleValue = Math.Min(100, Math.Max(0, _circleValue));
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
}

