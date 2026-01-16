// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">页面边距选项</para>
///  <para lang="en">Page margin options</para>
/// </summary>
[ExcludeFromCodeCoverage]
public record MarginOptions
{
    /// <summary>
    ///  <para lang="zh">Top margin, accepts values labeled with units.</para>
    ///  <para lang="en">Top margin, accepts values labeled with units.</para>
    /// </summary>
    public object? Top { get; set; }

    /// <summary>
    ///  <para lang="zh">Left margin, accepts values labeled with units.</para>
    ///  <para lang="en">Left margin, accepts values labeled with units.</para>
    /// </summary>
    public object? Left { get; set; }

    /// <summary>
    ///  <para lang="zh">Bottom margin, accepts values labeled with units.</para>
    ///  <para lang="en">Bottom margin, accepts values labeled with units.</para>
    /// </summary>
    public object? Bottom { get; set; }

    /// <summary>
    ///  <para lang="zh">Right margin, accepts values labeled with units.</para>
    ///  <para lang="en">Right margin, accepts values labeled with units.</para>
    /// </summary>
    public object? Right { get; set; }
}
