// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">JSModuleAutoLoaderAttribute 类</para>
/// <para lang="en">JSModuleAutoLoaderAttribute class</para>
/// </summary>
/// <param name="path"><para lang="zh">JavaScript 模块的路径</para><para lang="en">The path to the JavaScript module</para></param>
[AttributeUsage(AttributeTargets.Class)]
public class JSModuleAutoLoaderAttribute(string? path = null) : Attribute
{
    /// <summary>
    /// <para lang="zh">获得 路径属性</para>
    /// <para lang="en">Gets the path property</para>
    /// </summary>
    public string? Path { get; } = path;

    /// <summary>
    /// <para lang="zh">表示对 JavaScript 对象的引用。默认值为 false。</para>
    /// <para lang="en">Represents a reference to a JavaScript object. Default value is false.</para>
    /// </summary>
    public bool JSObjectReference { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动调用 init。默认值为 true。</para>
    /// <para lang="en">Gets or sets whether to automatically invoke init. Default is true.</para>
    /// </summary>
    public bool AutoInvokeInit { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否自动调用 dispose。默认值为 true。</para>
    /// <para lang="en">Gets or sets whether to automatically invoke dispose. Default is true.</para>
    /// </summary>
    public bool AutoInvokeDispose { get; set; } = true;
}
