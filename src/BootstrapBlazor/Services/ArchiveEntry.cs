// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;

/// <summary>
///  <para lang="zh">归档项实体类</para>
///  <para lang="en">Archive Entry Entity Class</para>
/// </summary>
public readonly record struct ArchiveEntry
{
    /// <summary>
    ///  <para lang="zh">获得 物理文件</para>
    ///  <para lang="en">Get Physical File</para>
    /// </summary>
    public string SourceFileName { get; init; }

    /// <summary>
    ///  <para lang="zh">获得 归档项</para>
    ///  <para lang="en">Get Archive Entry</para>
    /// </summary>
    public string EntryName { get; init; }

    /// <summary>
    ///  <para lang="zh">获得 压缩配置</para>
    ///  <para lang="en">Get Compression Configuration</para>
    /// </summary>
    public CompressionLevel? CompressionLevel { get; init; }
}
