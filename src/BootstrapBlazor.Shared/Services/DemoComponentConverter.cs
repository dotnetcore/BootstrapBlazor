// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Services;

internal class DemoComponentConverter
{
    private static Dictionary<string, Type>? Cache { get; set; }

    public bool TryParse(string? name, [NotNullWhen(true)] out Type? type)
    {
        type = null;
        name = name?.Split('.').LastOrDefault();
        if (!string.IsNullOrEmpty(name))
        {
            Cache ??= BuildDemoTable();
            if (!string.IsNullOrEmpty(name) && Cache.ContainsKey(name))
            {
                type = Cache[name];
            }
        }
        return type != null;
    }

    private Dictionary<string, Type> BuildDemoTable() => GetType().Assembly.GetExportedTypes().Where(i => i.Namespace?.Contains(".Demos.") ?? false).ToDictionary(i => i.Name);
}
