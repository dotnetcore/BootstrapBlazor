// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// JSModuleAutoLoaderAttribute class
/// </summary>
/// <param name="path">The path to the JavaScript module</param>
[AttributeUsage(AttributeTargets.Class)]
public class JSModuleAutoLoaderAttribute(string? path = null) : Attribute
{
    /// <summary>
    /// Gets the path property
    /// </summary>
    public string? Path { get; } = path;

    /// <summary>
    /// Represents a reference to a JavaScript object. Default value is false.
    /// </summary>
    public bool JSObjectReference { get; set; }

    /// <summary>
    /// Gets or sets whether to automatically invoke init. Default is true.
    /// </summary>
    public bool AutoInvokeInit { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to automatically invoke dispose. Default is true.
    /// </summary>
    public bool AutoInvokeDispose { get; set; } = true;
}
