// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class IconMapperOptionsExtensions
{
    /// <summary>
    /// 添加 Meterial 图标到系统
    /// </summary>
    /// <param name="options"></param>
    public static void AddMaterialIconMapper(this IconMapperOptions options)
    {
        options.Items = new()
        {
            { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" },
            { ComponentIcons.TableSortIconAsc, "mdi mdi-sort-ascending" },
            { ComponentIcons.TableSortDesc, "mdi mdi-sort-descending" },
            { ComponentIcons.TableSortIcon, "mdi mdi-sort" },
            { ComponentIcons.TableFilterIcon, "mdi mdi-filter" },
            { ComponentIcons.TableExportButtonIcon, "mdi mdi-download" }
        };
    }
}
