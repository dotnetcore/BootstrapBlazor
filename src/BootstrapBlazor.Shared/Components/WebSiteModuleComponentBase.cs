// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// WebSiteModuleComponentBase 组件
/// </summary>
public abstract class WebSiteModuleComponentBase : BootstrapModuleComponentBase
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnLoadJSModule()
    {
        base.OnLoadJSModule();

        ModulePath = $"./_content/BootstrapBlazor.Shared/{ModulePath}";
    }
}
