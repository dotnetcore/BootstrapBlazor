// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Components;

/// <summary>
/// Loads collocated modules from the shared static web assets.
/// </summary>
public abstract class SharedModuleComponentBase : BootstrapModuleComponentBase
{
    /// <inheritdoc />
    protected override void OnLoadJSModule()
    {
        base.OnLoadJSModule();

        if (!string.IsNullOrEmpty(ModulePath))
        {
            ModulePath = $"./_content/BootstrapBlazor.Shared/Components/{ModulePath}";
        }
    }
}
