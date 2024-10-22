// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 构造函数
/// </summary>
/// <param name="path"></param>
class BootstrapModuleAutoLoaderAttribute(string? path = null) : JSModuleAutoLoaderAttribute(path)
{
    /// <summary>
    /// 获得/设置 模块名称 自动使用 modules 文件夹下脚本
    /// </summary>
    public string? ModuleName { get; set; }
}
