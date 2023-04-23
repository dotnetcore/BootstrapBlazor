// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModuleAutoLoaderAttribute class
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class JSModuleAutoLoaderAttribute : Attribute
{
    /// <summary>
    /// 获得 Name 属性
    /// </summary>
    public string? Path { get; }

    /// <summary>
    /// 获得 模块名称
    /// </summary>
    public string? ModuleName { get; set; }

    /// <summary>
    /// Represents a reference to a JavaScript object Default value false
    /// </summary>
    public bool JSObjectReference { get; set; }

    /// <summary>
    /// 获得/设置 脚本路径是否为相对路径 默认 true
    /// </summary>
    public bool Relative { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动调用 init 默认 true
    /// </summary>
    public bool AutoInvokeInit { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动调用 dispose 默认 true
    /// </summary>
    public bool AutoInvokeDispose { get; set; } = true;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path"></param>
    public JSModuleAutoLoaderAttribute(string? path = null)
    {
        Path = path;
    }
}
