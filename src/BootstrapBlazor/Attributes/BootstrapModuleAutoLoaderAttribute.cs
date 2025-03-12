// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Constructor
/// </summary>
/// <param name="path"></param>
class BootstrapModuleAutoLoaderAttribute(string? path = null) : JSModuleAutoLoaderAttribute(path)
{
    /// <summary>
    /// Gets or sets the module name. Automatically uses scripts from the modules folder.
    /// </summary>
    public string? ModuleName { get; set; }
}
