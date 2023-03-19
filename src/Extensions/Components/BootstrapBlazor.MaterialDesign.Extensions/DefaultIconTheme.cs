// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

internal class DefaultIconTheme : IIconTheme
{
    private Dictionary<ComponentIcons, string> Icons { get; }

    public DefaultIconTheme()
    {
        Icons = new Dictionary<ComponentIcons, string>()
        {
            { ComponentIcons.AnchorLinkIcon, "mdi mdi-link-variant" },
            { ComponentIcons.TableSortIconAsc, "mdi mdi-sort-ascending" },
            { ComponentIcons.TableSortDesc, "mdi mdi-sort-descending" },
            { ComponentIcons.TableSortIcon, "mdi mdi-sort" },
            { ComponentIcons.TableFilterIcon, "mdi mdi-filter" },
            { ComponentIcons.TableExportButtonIcon, "mdi mdi-download" }
        };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Dictionary<ComponentIcons, string> GetIcons() => Icons;
}
