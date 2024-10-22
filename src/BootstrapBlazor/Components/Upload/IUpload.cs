// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IUpload 接口
/// </summary>
public interface IUpload
{
    /// <summary>
    /// 获得/设置 上传文件实例集合
    /// </summary>
    List<UploadFile> UploadFiles { get; }
}
