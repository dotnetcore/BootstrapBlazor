// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

static class BootstrapModuleAutoLoaderAttributeExtensions
{
    public static string LoadModulePath(this BootstrapModuleAutoLoaderAttribute loader, Type type)
    {
        var path = string.IsNullOrEmpty(loader.ModuleName) ? LoadPath() : $"modules/{loader.ModuleName}.js";
        return $"./_content/BootstrapBlazor/{path}";

        string LoadPath()
        {
            string? path;

            // 通过类名设置路径
            if (string.IsNullOrEmpty(loader.Path))
            {
                var moduleName = type.GetTypeModuleName();
                path = $"Components/{moduleName}/{moduleName}.razor.js";
            }
            else
            {
                path = $"Components/{loader.Path}";
            }
            return path;
        }
    }
}
