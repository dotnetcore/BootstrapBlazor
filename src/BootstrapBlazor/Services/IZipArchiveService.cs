// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Zip 归档服务
/// </summary>
public interface IZipArchiveService
{
    /// <summary>
    /// 归档方法
    /// </summary>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    /// <returns>归档数据流</returns>
    Task<Stream> ArchiveAsync(IEnumerable<string> files, ArchiveOptions? options = null);

    /// <summary>
    /// 归档方法
    /// </summary>
    /// <param name="archiveFileName">归档文件</param>
    /// <param name="files">要归档的文件集合</param>
    /// <param name="options">归档配置</param>
    Task ArchiveAsync(string archiveFileName, IEnumerable<string> files, ArchiveOptions? options = null);
}
