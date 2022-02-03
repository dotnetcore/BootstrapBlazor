// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
