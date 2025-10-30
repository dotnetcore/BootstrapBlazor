// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 页面边距选项
/// </summary>
public record MarginOptions
{
    /// <summary>
    /// Top margin, accepts values labeled with units.
    /// </summary>
    public object? Top { get; set; }

    /// <summary>
    /// Left margin, accepts values labeled with units.
    /// </summary>
    public object? Left { get; set; }

    /// <summary>
    /// Bottom margin, accepts values labeled with units.
    /// </summary>
    public object? Bottom { get; set; }

    /// <summary>
    /// Right margin, accepts values labeled with units.
    /// </summary>
    public object? Right { get; set; }
}
