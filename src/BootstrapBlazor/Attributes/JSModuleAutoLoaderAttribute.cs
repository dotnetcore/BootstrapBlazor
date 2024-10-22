// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModuleAutoLoaderAttribute class
/// </summary>
/// <param name="path"></param>
[AttributeUsage(AttributeTargets.Class)]
public class JSModuleAutoLoaderAttribute(string? path = null) : Attribute
{
    /// <summary>
    /// 获得 Name 属性
    /// </summary>
    public string? Path { get; } = path;

    /// <summary>
    /// Represents a reference to a JavaScript object Default value false
    /// </summary>
    public bool JSObjectReference { get; set; }

    /// <summary>
    /// 获得/设置 是否自动调用 init 默认 true
    /// </summary>
    public bool AutoInvokeInit { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否自动调用 dispose 默认 true
    /// </summary>
    public bool AutoInvokeDispose { get; set; } = true;
}
