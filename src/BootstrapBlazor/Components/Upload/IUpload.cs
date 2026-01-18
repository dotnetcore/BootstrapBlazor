// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IUpload 接口</para>
/// <para lang="en">IUpload Interface</para>
/// </summary>
public interface IUpload
{
    /// <summary>
    /// <para lang="zh">获得/设置 上传文件实例集合</para>
    /// <para lang="en">Gets or sets the upload file collection</para>
    /// </summary>
    List<UploadFile> UploadFiles { get; }
}
