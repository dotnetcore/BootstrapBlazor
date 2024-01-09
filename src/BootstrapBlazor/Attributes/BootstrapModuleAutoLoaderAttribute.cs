// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class BootstrapModuleAutoLoaderAttribute : JSModuleAutoLoaderAttribute
{
    /// <summary>
    /// 获得/设置 模块名称 自动使用 modules 文件夹下脚本
    /// </summary>
    public string? ModuleName { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path"></param>
    public BootstrapModuleAutoLoaderAttribute(string? path = null) : base(path)
    {

    }
}
