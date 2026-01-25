// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">构造函数</para>
/// <para lang="en">Constructor</para>
/// </summary>
/// <param name="path"></param>
class BootstrapModuleAutoLoaderAttribute(string? path = null) : JSModuleAutoLoaderAttribute(path)
{
    /// <summary>
    /// <para lang="zh">获得/设置 模块名称。自动使用 modules 文件夹中的脚本</para>
    /// <para lang="en">Gets or sets the module name. Automatically uses scripts from the modules folder</para>
    /// </summary>
    public string? ModuleName { get; set; }
}
