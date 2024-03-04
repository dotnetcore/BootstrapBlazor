// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
