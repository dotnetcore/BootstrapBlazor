// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.IO.Compression;

/// <summary>
/// 归档项实体类
/// </summary>
public readonly record struct ArchiveEntry
{
    /// <summary>
    /// 获得 物理文件
    /// </summary>
    public string SourceFileName { get; init; }

    /// <summary>
    /// 获得 归档项
    /// </summary>
    public string EntryName { get; init; }

    /// <summary>
    /// 获得 压缩配置
    /// </summary>
    public CompressionLevel? CompressionLevel { get; init; }
}
