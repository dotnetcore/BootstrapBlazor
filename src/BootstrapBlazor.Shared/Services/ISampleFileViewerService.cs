// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">文件预览示例服务，转换始终在服务器执行。</para>
/// <para lang="en">File preview sample service; conversion always runs on the server.</para>
/// </summary>
public interface ISampleFileViewerService
{
    /// <summary>
    /// <para lang="zh">转换指定的站点示例文件。</para>
    /// <para lang="en">Converts the specified site sample file.</para>
    /// </summary>
    Task<string> PreviewAsync(string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">获取 FileViewer 组件参数文档。</para>
    /// <para lang="en">Gets FileViewer component parameter documentation.</para>
    /// </summary>
    Task<List<AttributeItem>> GetAttributesAsync(CancellationToken cancellationToken = default);
}
